namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;

    public class SchedulleController : ApiController
    {
        [HttpGet]
        public List<Schedule> GetScheduleList()
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleList(ref errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleByCourseId(int course_id)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleByCourseId(course_id, ref errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleByDay()
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleDay(ref errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleTime()
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleTime(ref errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleByScheduleId(int schedule_id)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleByScheduleId(schedule_id, ref errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleByYear(string year)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleByYear(year, ref errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleBySessionId(string sessionId)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleBySessionId(sessionId, ref errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleByScheduleDayId(int day_id)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleByScheduleDayId(day_id, ref errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleByScheduleTimeId(int time_id)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleByScheduleTimeId(time_id, ref errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleByInstructorId(int id)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleByInstructorId(id, ref errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleByQuarter(string quarter)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleByQuarter(quarter, ref errors);
        }

        [HttpPost]
        public string InsertSchedule(Schedule schedule)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            service.InsertSchedule(schedule, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string UpdateSchedule(Schedule schedule)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            service.UpdateSchedule(schedule, ref errors);

            if (errors.Count == 0)
            {
                return "Update Sucessfull Following Schedule has been updated <br>" + schedule.ScheduleId;
            }

            return "error";
        }

        [HttpPost]
        public string DeleteSchedule(int id)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            service.DeleteSchedule(id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}