using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using UnitTestProject.Controllers;
using UnitTestProject.Interfaces;
using UnitTestProject.Models;

namespace UnitTestProject.UnitTests
{

        [TestFixture]
        public class UsersControllerTests
        {
            private Mock<IUserRepository> _mockRepo;
            private UsersController _controller;

            [SetUp]
            public void Setup()
            {
                _mockRepo = new Mock<IUserRepository>();
                _controller = new UsersController(_mockRepo.Object);
            }

            [Test]
            public void GetUserById_ReturnsOk_WhenUserExists()
            {
                short userId = 1;
                var user = new User { Id = userId, Un = "testuser", Pw = "hashedpassword" };
                _mockRepo.Setup(repo => repo.GetUserByID(userId)).Returns(user);

                var result = _controller.GetUserByID(userId);

                Assert.IsInstanceOf<OkObjectResult>(result);
                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.AreEqual(user, okResult.Value);
            }

            [Test]
            public void GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
            {
                short userId = 99;
                _mockRepo.Setup(repo => repo.GetUserByID(userId)).Returns((User)null);

                var result = _controller.GetUserByID(userId);

                Assert.IsInstanceOf<NotFoundResult>(result);
            }
        }
    
}