namespace WebApi136.Areas.HelpPage.SampleGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Web.Http.Description;
    using System.Xml.Linq;

    using Newtonsoft.Json;

    public class HelpPageSampleGenerator
    {
        public HelpPageSampleGenerator()
        {
            this.ActualHttpMessageTypes = new Dictionary<HelpPageSampleKey, Type>();
            this.ActionSamples = new Dictionary<HelpPageSampleKey, object>();
            this.SampleObjects = new Dictionary<Type, object>();
        }

        public IDictionary<HelpPageSampleKey, Type> ActualHttpMessageTypes { get; internal set; }

        public IDictionary<HelpPageSampleKey, object> ActionSamples { get; internal set; }

        public IDictionary<Type, object> SampleObjects { get; internal set; }

        public IDictionary<MediaTypeHeaderValue, object> GetSampleRequests(ApiDescription api)
        {
            return this.GetSample(api, SampleDirection.Request);
        }

        public IDictionary<MediaTypeHeaderValue, object> GetSampleResponses(ApiDescription api)
        {
            return this.GetSample(api, SampleDirection.Response);
        }

        public virtual IDictionary<MediaTypeHeaderValue, object> GetSample(ApiDescription api, SampleDirection sampleDirection)
        {
            if (api == null)
            {
                throw new ArgumentNullException("api");
            }

            var controllerName = api.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = api.ActionDescriptor.ActionName;
            var parameterNames = api.ParameterDescriptions.Select(p => p.Name);
            Collection<MediaTypeFormatter> formatters;
            var type = this.ResolveType(api, controllerName, actionName, parameterNames, sampleDirection, out formatters);
            var samples = new Dictionary<MediaTypeHeaderValue, object>();

            // Use the samples provided directly for actions
            var actionSamples = this.GetAllActionSamples(controllerName, actionName, parameterNames, sampleDirection);
            foreach (var actionSample in actionSamples)
            {
                samples.Add(actionSample.Key.MediaType, WrapSampleIfString(actionSample.Value));
            }

            if (type != null && !typeof(HttpResponseMessage).IsAssignableFrom(type))
            {
                var sampleObject = this.GetSampleObject(type);
                foreach (var formatter in formatters)
                {
                    foreach (var mediaType in formatter.SupportedMediaTypes)
                    {
                        if (!samples.ContainsKey(mediaType))
                        {
                            var sample = this.GetActionSample(controllerName, actionName, parameterNames, type, formatter, mediaType, sampleDirection);

                            if (sample == null && sampleObject != null)
                            {
                                sample = this.WriteSampleObjectUsingFormatter(formatter, sampleObject, type, mediaType);
                            }

                            samples.Add(mediaType, WrapSampleIfString(sample));
                        }
                    }
                }
            }

            return samples;
        }

        public virtual object GetActionSample(string controllerName, string actionName, IEnumerable<string> parameterNames, Type type, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, SampleDirection sampleDirection)
        {
            object sample;

            // First, try get sample provided for a specific mediaType, controllerName, actionName and parameterNames.
            // If not found, try get the sample provided for a specific mediaType, controllerName and actionName regardless of the parameterNames
            // If still not found, try get the sample provided for a specific type and mediaType 
            if (this.ActionSamples.TryGetValue(new HelpPageSampleKey(mediaType, sampleDirection, controllerName, actionName, parameterNames), out sample) ||
                this.ActionSamples.TryGetValue(new HelpPageSampleKey(mediaType, sampleDirection, controllerName, actionName, new[] { "*" }), out sample) ||
                this.ActionSamples.TryGetValue(new HelpPageSampleKey(mediaType, type), out sample))
            {
                return sample;
            }

            return null;
        }

        public virtual object GetSampleObject(Type type)
        {
            object sampleObject;

            if (!this.SampleObjects.TryGetValue(type, out sampleObject))
            {
                // Try create a default sample object
                var objectGenerator = new ObjectGenerator();
                sampleObject = objectGenerator.GenerateObject(type);
            }

            return sampleObject;
        }

        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "This is only used in advanced scenarios.")]
        public virtual Type ResolveType(ApiDescription api, string controllerName, string actionName, IEnumerable<string> parameterNames, SampleDirection sampleDirection, out Collection<MediaTypeFormatter> formatters)
        {
            if (!Enum.IsDefined(typeof(SampleDirection), sampleDirection))
            {
                throw new InvalidEnumArgumentException("sampleDirection", (int)sampleDirection, typeof(SampleDirection));
            }

            if (api == null)
            {
                throw new ArgumentNullException("api");
            }

            Type type;
            if (this.ActualHttpMessageTypes.TryGetValue(new HelpPageSampleKey(sampleDirection, controllerName, actionName, parameterNames), out type) ||
                this.ActualHttpMessageTypes.TryGetValue(new HelpPageSampleKey(sampleDirection, controllerName, actionName, new[] { "*" }), out type))
            {
                // Re-compute the supported formatters based on type
                var newFormatters = new Collection<MediaTypeFormatter>();
                foreach (var formatter in api.ActionDescriptor.Configuration.Formatters)
                {
                    if (IsFormatSupported(sampleDirection, formatter, type))
                    {
                        newFormatters.Add(formatter);
                    }
                }

                formatters = newFormatters;
            }
            else
            {
                switch (sampleDirection)
                {
                    case SampleDirection.Request:
                        var requestBodyParameter = api.ParameterDescriptions.FirstOrDefault(p => p.Source == ApiParameterSource.FromBody);
                        type = requestBodyParameter == null ? null : requestBodyParameter.ParameterDescriptor.ParameterType;
                        formatters = api.SupportedRequestBodyFormatters;
                        break;
                    case SampleDirection.Response:
                    default:
                        type = api.ActionDescriptor.ReturnType;
                        formatters = api.SupportedResponseFormatters;
                        break;
                }
            }

            return type;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The exception is recorded as InvalidSample.")]
        public virtual object WriteSampleObjectUsingFormatter(MediaTypeFormatter formatter, object value, Type type, MediaTypeHeaderValue mediaType)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException("formatter");
            }

            if (mediaType == null)
            {
                throw new ArgumentNullException("mediaType");
            }

            object sample;
            MemoryStream ms = null;
            HttpContent content = null;
            try
            {
                if (formatter.CanWriteType(type))
                {
                    ms = new MemoryStream();
                    content = new ObjectContent(type, value, formatter, mediaType);
                    formatter.WriteToStreamAsync(type, value, ms, content, null).Wait();
                    ms.Position = 0;
                    var reader = new StreamReader(ms);
                    var serializedSampleString = reader.ReadToEnd();
                    if (mediaType.MediaType.ToUpperInvariant().Contains("XML"))
                    {
                        serializedSampleString = TryFormatXml(serializedSampleString);
                    }
                    else if (mediaType.MediaType.ToUpperInvariant().Contains("JSON"))
                    {
                        serializedSampleString = TryFormatJson(serializedSampleString);
                    }

                    sample = new TextSample(serializedSampleString);
                }
                else
                {
                    sample = new InvalidSample(string.Format(
                        CultureInfo.CurrentCulture,
                        "Failed to generate the sample for media type '{0}'. Cannot use formatter '{1}' to write type '{2}'.",
                        mediaType,
                        formatter.GetType().Name,
                        type.Name));
                }
            }
            catch (Exception e)
            {
                sample = new InvalidSample(string.Format(
                    CultureInfo.CurrentCulture,
                    "An exception has occurred while using the formatter '{0}' to generate sample for media type '{1}'. Exception message: {2}",
                    formatter.GetType().Name,
                    mediaType.MediaType,
                    e.Message));
            }
            finally
            {
                if (ms != null)
                {
                    ms.Dispose();
                }

                if (content != null)
                {
                    content.Dispose();
                }
            }

            return sample;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Handling the failure by returning the original string.")]
        private static string TryFormatJson(string str)
        {
            try
            {
                var parsedJson = JsonConvert.DeserializeObject(str);
                return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            }
            catch
            {
                // can't parse JSON, return the original string
                return str;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Handling the failure by returning the original string.")]
        private static string TryFormatXml(string str)
        {
            try
            {
                var xml = XDocument.Parse(str);
                return xml.ToString();
            }
            catch
            {
                // can't parse XML, return the original string
                return str;
            }
        }

        private static bool IsFormatSupported(SampleDirection sampleDirection, MediaTypeFormatter formatter, Type type)
        {
            switch (sampleDirection)
            {
                case SampleDirection.Request:
                    return formatter.CanReadType(type);
                case SampleDirection.Response:
                    return formatter.CanWriteType(type);
            }

            return false;
        }

        private static object WrapSampleIfString(object sample)
        {
            var stringSample = sample as string;
            if (stringSample != null)
            {
                return new TextSample(stringSample);
            }

            return sample;
        }

        private IEnumerable<KeyValuePair<HelpPageSampleKey, object>> GetAllActionSamples(string controllerName, string actionName, IEnumerable<string> parameterNames, SampleDirection sampleDirection)
        {
            var parameterNamesSet = new HashSet<string>(parameterNames, StringComparer.OrdinalIgnoreCase);
            foreach (var sample in this.ActionSamples)
            {
                var sampleKey = sample.Key;
                if (string.Equals(controllerName, sampleKey.ControllerName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(actionName, sampleKey.ActionName, StringComparison.OrdinalIgnoreCase) &&
                    (sampleKey.ParameterNames.SetEquals(new[] { "*" }) || parameterNamesSet.SetEquals(sampleKey.ParameterNames)) &&
                    sampleDirection == sampleKey.SampleDirection)
                {
                    yield return sample;
                }
            }
        }
    }
}