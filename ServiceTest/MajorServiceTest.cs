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
    public class MajorServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertMajorErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IMajorRepository>();
            var majorService = new MajorService(mockRepository.Object);
            var major = new Major { MajorCode = string.Empty };

            //// Act
            majorService.InsertMajor(major, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertMajorErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IMajorRepository>();
            var majorService = new MajorService(mockRepository.Object);
            var major = new Major { MajorCode = string.Empty, MajorDescription = string.Empty };

            //// Act
            majorService.InsertMajor(major, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertMajorErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IMajorRepository>();
            var majorService = new MajorService(mockRepository.Object);
            var major = new Major { };

            //// Act
            majorService.InsertMajor(major, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void InsertMajorErrorTest4()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IMajorRepository>();
            var majorService = new MajorService(mockRepository.Object);
            var major = new Major { MajorCode = "CSE", MajorDescription = "Computer Science" };

            //// Act
            majorService.InsertMajor(major, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateMajorErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IMajorRepository>();
            var majorService = new MajorService(mockRepository.Object);
            var major = new Major { MajorCode = string.Empty };

            //// Act
            majorService.UpdateMajor(major, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatetMajorErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IMajorRepository>();
            var majorService = new MajorService(mockRepository.Object);
            var major = new Major { MajorCode = string.Empty, MajorDescription = string.Empty };

            //// Act
            majorService.UpdateMajor(major, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateMajorErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IMajorRepository>();
            var majorService = new MajorService(mockRepository.Object);
            var major = new Major { };

            //// Act
            majorService.UpdateMajor(major, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void UpdateMajorErrorTest4()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IMajorRepository>();
            var majorService = new MajorService(mockRepository.Object);
            var major = new Major { MajorCode = "CSE", MajorDescription = "Computer Science" };

            //// Act
            majorService.UpdateMajor(major, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteMajorErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IMajorRepository>();
            var majorService = new MajorService(mockRepository.Object);

            //// Act
            majorService.DeleteMajor(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteMajorErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IMajorRepository>();
            var majorService = new MajorService(mockRepository.Object);

            //// Act
            majorService.DeleteMajor(string.Empty, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void DeleteMajorErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IMajorRepository>();
            var majorService = new MajorService(mockRepository.Object);

            //// Act
            majorService.DeleteMajor("ECON", ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }
    }
}
