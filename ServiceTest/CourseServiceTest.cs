namespace ServiceTest
{
    using System;
    using System.Collections.Generic;

    using IRepository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using POCO;
    using Service;

    [TestClass]
    public class CourseServiceTest
    {
        [TestMethod]
        public void InsertCourseErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            var course = new Course { CourseId = string.Empty };

            //// Act
            courseService.InsertCourse(course, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateCourseErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);

            var course = new Course { CourseId = string.Empty };

            //// Act
            courseService.UpdateCourse(course, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void DeleteCourseErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);

            //// Act
            courseService.DeleteCourse(null, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void DeleteCourseErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            var course = new Course { CourseId = "1" };

            //// Act
            courseService.DeleteCourse(course, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertPreReqErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);

            var course = new PreReqCourse { CourseId = 0 };

            //// Act
            courseService.InsertPreReqCourse(course, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertPreReqErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);

            var course = new PreReqCourse { CourseId = 0, PreReqId = 0 };

            //// Act
            courseService.InsertPreReqCourse(course, ref errors);

            //// Assert
            Assert.AreEqual(2, errors.Count);
        }

        [TestMethod]
        public void InsertPreReqErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);

            var course = new PreReqCourse { CourseId = 40, PreReqId = 43 };

            //// Act
            courseService.InsertPreReqCourse(course, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeletePreReqErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);

            //// Act
            courseService.DeletePreReqCourse(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeletePreReqErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            var course = new PreReqCourse { CourseId = 0, PreReqId = 0 };

            //// Act
            courseService.DeletePreReqCourse(course, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeletePreReqErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            var course = new PreReqCourse { CourseId = 0 };

            //// Act
            courseService.DeletePreReqCourse(course, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]     
        public void DeletePreReqErrorTest4()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            var course = new PreReqCourse { CourseId = 41, PreReqId = 40 };

            //// Act
            courseService.DeletePreReqCourse(course, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }
    }
}
