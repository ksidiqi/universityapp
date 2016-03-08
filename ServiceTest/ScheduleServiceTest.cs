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
    public class ScheduleServiceTest
    {
        [TestMethod]
        public void InsertScheduleErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);
            var schedule = new Schedule { };

            //// Act
            scheduleService.InsertSchedule(schedule, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void InsertScheduleErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);
            var schedule = new Schedule
            {
                ScheduleId = 0,
                Year = "2011",
                Quarter = string.Empty,
                Session = string.Empty,
                Schedule_day_id = 0,
                Schedule_time_id = 0
            };
            //// Act
            scheduleService.InsertSchedule(schedule, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void InsertScheduleErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);
            var schedule = new Schedule
            {
                ScheduleId = 1,
                Year = "2011",
                Quarter = "FALL",
                Session = "B00",
                Schedule_day_id = 2,
                Schedule_time_id = 3,
                Instructor = new Instructor()
                {
                    Id = 10
                },
            };

            //// Act
            scheduleService.InsertSchedule(schedule, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void UpdateScheduleErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);
            var schedule = new Schedule { };

            //// Act
            scheduleService.UpdateSchedule(schedule, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void UpdateScheduleErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);
            var schedule = new Schedule
            {
                ScheduleId = 0,
                Year = "0",
                Quarter = string.Empty,
                Session = string.Empty,
                Schedule_day_id = 0,
                Schedule_time_id = 0,
                Instructor = new Instructor()
                {
                    Id = 0
                }
            };

            //// Act
            scheduleService.UpdateSchedule(schedule, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void UpdateScheduleErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);
            var schedule = new Schedule
            {
                ScheduleId = 1,
                Year = "2011",
                Quarter = "FALL",
                Session = "B00",
                Schedule_day_id = 2,
                Schedule_time_id = 3,
                Instructor = new Instructor
                {
                    Id = 10
                }
            };

            //// Act
            scheduleService.UpdateSchedule(schedule, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void DeleteScheduleErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);

            //// Act
            scheduleService.DeleteSchedule(0, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteScheduleErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);

            //// Act
            scheduleService.DeleteSchedule(-1, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void DeleteScheduleErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);

            //// Act
            scheduleService.DeleteSchedule(1, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }
    }
}
