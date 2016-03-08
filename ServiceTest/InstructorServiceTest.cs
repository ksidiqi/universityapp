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
    public class InstructorServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertInstructor()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IInstructorRepository>();
            var courseService = new InstructorService(mockRepository.Object);
            var instructor = new Instructor { Id = 0 };

            //// Act
            courseService.InsertInstructor(instructor, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void InsertInstructor2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IInstructorRepository>();
            var courseService = new InstructorService(mockRepository.Object);
            var instructor = new Instructor { Id = 5, FirstName = "foo", LastName = "bar", Title = "test" };

            //// Act
            courseService.InsertInstructor(instructor, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateInstructor()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IInstructorRepository>();
            var courseService = new InstructorService(mockRepository.Object);
            var instructor = new Instructor { Id = 0 };

            //// Act
            courseService.UpdateInstructor(instructor, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void DeleteInstructor()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IInstructorRepository>();
            var courseService = new InstructorService(mockRepository.Object);
            var instructor = new Instructor { Id = 1 };
            //// Act
            courseService.DeleteInstructor(instructor, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }
    }
}
