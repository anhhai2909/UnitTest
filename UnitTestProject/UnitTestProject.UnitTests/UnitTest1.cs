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

        // Tests for GetUserList
        [Test]
        public void GetUserList_ReturnsOk_WhenUsersExist()
        {
            var users = new List<User> { new User { Id = 1, Un = "testuser" } };
            _mockRepo.Setup(repo => repo.GetUserList()).Returns(users);

            var result = _controller.GetUserList() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(users, result.Value);
        }

        [Test]
        public void GetUserList_ReturnsOk_WhenNoUsers()
        {
            _mockRepo.Setup(repo => repo.GetUserList()).Returns(new List<User>());

            var result = _controller.GetUserList() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsEmpty((List<User>)result.Value);
        }

        // Tests for GetUserByID
        [Test]
        public void GetUserById_ReturnsOk_WhenUserExists()
        {
            short userId = 1;
            var user = new User { Id = userId, Un = "testuser", Pw = "hashedpassword" };
            _mockRepo.Setup(repo => repo.GetUserByID(userId)).Returns(user);

            var result = _controller.GetUserByID(userId) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(user, result.Value);
        }

        [Test]
        public void GetUserById_ReturnsBadRequest_WhenUserDoesNotExist()
        {
            short userId = 99;
            _mockRepo.Setup(repo => repo.GetUserByID(userId)).Returns((User)null);

            var result = _controller.GetUserByID(userId) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            
        }

        // Tests for UpdateUser
        [Test]
        public void UpdateUser_ReturnsOk_WhenUserExistsAndUpdateSucceeds()
        {
            var user = new User { Id = 1, Un = "updateduser", PhoneNum = "1234567890", Email = "test@example.com", Pw = "newpassword" };
            _mockRepo.Setup(repo => repo.GetUserByID(user.Id)).Returns(new User { Id = 1 });
            _mockRepo.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns(true);

            var result = _controller.UpdateUser(user) as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void UpdateUser_ReturnsBadRequest_WhenUserDoesNotExist()
        {
            var user = new User { Id = 99, Un = "updateduser" };
            _mockRepo.Setup(repo => repo.GetUserByID(user.Id)).Returns((User)null);

            var result = _controller.UpdateUser(user) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void UpdateUser_ReturnsBadRequest_WhenUpdateFails()
        {
            var user = new User { Id = 1, Un = "updateduser" };
            _mockRepo.Setup(repo => repo.GetUserByID(user.Id)).Returns(new User { Id = 1 });
            _mockRepo.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns(false);

            var result = _controller.UpdateUser(user) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        // Tests for CreateUser
        [Test]
        public void CreateUser_ReturnsOk_WhenUserDoesNotExistAndCreationSucceeds()
        {
            var user = new User { Id = 1, Un = "newuser", PhoneNum = "1234567890", Email = "new@example.com", Pw = "password" };
            _mockRepo.Setup(repo => repo.GetUserByID(user.Id)).Returns((User)null);
            _mockRepo.Setup(repo => repo.CreateUser(user)).Returns(true);

            var result = _controller.CreateUser(user) as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void CreateUser_ReturnsBadRequest_WhenUserAlreadyExists()
        {
            var user = new User { Id = 1, Un = "newuser" };
            _mockRepo.Setup(repo => repo.GetUserByID(user.Id)).Returns(new User { Id = 1 });

            var result = _controller.CreateUser(user) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void CreateUser_ReturnsBadRequest_WhenCreationFails()
        {
            var user = new User { Id = 1, Un = "newuser" };
            _mockRepo.Setup(repo => repo.GetUserByID(user.Id)).Returns((User)null);
            _mockRepo.Setup(repo => repo.CreateUser(user)).Returns(false);

            var result = _controller.CreateUser(user) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        // Tests for DeleteUser
        [Test]
        public void DeleteUser_ReturnsOk_WhenUserExistsAndDeletionSucceeds()
        {
            int userId = 1;
            _mockRepo.Setup(repo => repo.GetUserByID((short)userId)).Returns(new User { Id = (short)userId });
            _mockRepo.Setup(repo => repo.DeleteUser(userId)).Returns(true);

            var result = _controller.DeleteUser(userId) as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void DeleteUser_ReturnsBadRequest_WhenUserDoesNotExist()
        {
            int userId = 99;
            _mockRepo.Setup(repo => repo.GetUserByID((short)userId)).Returns((User)null);

            var result = _controller.DeleteUser(userId) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void DeleteUser_ReturnsBadRequest_WhenDeletionFails()
        {
            int userId = 1;
            _mockRepo.Setup(repo => repo.GetUserByID((short)userId)).Returns(new User { Id = (short)userId });
            _mockRepo.Setup(repo => repo.DeleteUser(userId)).Returns(false);

            var result = _controller.DeleteUser(userId) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}