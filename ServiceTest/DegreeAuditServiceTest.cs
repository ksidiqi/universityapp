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
    public class DegreeAuditServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertDegreeAuditErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IDegreeAuditRepository>();
            var degreeAuditService = new DegreeAuditService(mockRepository.Object);
            var degreeAudit = new DegreeAudit { CourseId = string.Empty };

            //// Act
            degreeAuditService.InsertDegreeAudit(degreeAudit, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateDegreeAuditErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IDegreeAuditRepository>();
            var degreeAuditService = new DegreeAuditService(mockRepository.Object);
            var degreeAudit = new DegreeAudit { CourseId = string.Empty };

            //// Act
            degreeAuditService.UpdateDegreeAudit(degreeAudit, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void DeleteDegreeAuditErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IDegreeAuditRepository>();
            var degreeAuditService = new DegreeAuditService(mockRepository.Object);
            var degreeAudit = new DegreeAudit { CourseId = string.Empty, StudentId = string.Empty };
          
            //// Act
            degreeAuditService.DeleteDegreeAudit(degreeAudit, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void DeleteDegreeAuditErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IDegreeAuditRepository>();
            var degreeAuditService = new DegreeAuditService(mockRepository.Object);
            var degreeAudit = new DegreeAudit { CourseId = string.Empty, StudentId = string.Empty };

            //// Act
            degreeAuditService.DeleteDegreeAudit(degreeAudit, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }
    }
}
