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
    public class ReviewServiceTest
    {
        ////Insert
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertReviewErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            reviewService.InsertReview(null, ref errors);

            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertReviewErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);
            var review1 = new Review { Student = string.Empty, Course = string.Empty };

            //// Act
            reviewService.InsertReview(review1, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertReviewErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);
            var review1 = new Review { Student = "1000001", Course = "A123" };

            reviewService.InsertReview(review1, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        ////update 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateReviewErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            reviewService.UpdateReview(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateReviewErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);
            var review1 = new Review { Student = string.Empty, Course = string.Empty };

            //// Act
            reviewService.UpdateReview(review1, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateReviewErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);
            var review1 = new Review { Student = "1234567", Course = "09876A" };

            //// Act
            reviewService.UpdateReview(review1, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        ////delete
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteReviewErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            reviewService.DeleteReview(null, null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteReviewErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);
            var review1 = new Review { Student = string.Empty, Course = string.Empty };

            //// Act
            reviewService.DeleteReview(string.Empty, string.Empty, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void DeleteReviewErrorTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);
            var review1 = new Review { Student = string.Empty, Course = string.Empty };

            //// Act
            reviewService.DeleteReview("1234567", "AAA4", ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        ////GET all REview 
        [TestMethod]
        public void GetAllReviewTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetAllReview(ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(list.Count, 0);
        }

        ////get student review 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetStudentReviewErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetStudentReview(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetStudentReviewErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetStudentReview(string.Empty, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetStudentReviewTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetStudentReview("AA00001", ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        ////Get instructor review 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInstructorReviewErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetInstructorReview(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInstructorReviewErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetInstructorReview(string.Empty, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetInstructorReviewTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetInstructorReview("A123456", ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        ////Get course review 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCourseReviewErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetCourseReview(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCourseReviewErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetCourseReview(string.Empty, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetCourseReviewTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetCourseReview("A123456", ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }
        ////get average course revewi 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAverageCourseReviewErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetAverageClassReview(null, null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAverageCourseReviewErrorTest2()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetAverageClassReview(string.Empty, null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAverageCourseReviewTest3()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IReviewRepository>();
            var reviewService = new ReviewService(mockRepository.Object);

            //// Act
            var list = reviewService.GetAverageClassReview("A123456", null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }
    }
}
