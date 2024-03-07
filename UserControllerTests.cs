using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Web.Mvc;
using System.Linq;

namespace Usercontrollertests
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController _userController;

        [TestInitialize]
        public void Setup()
        {
            _userController = new UserController();
        }

        [TestMethod]
        public void Index_ReturnsCorrectView()
        {
            // Act
            var result = _userController.Index(null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details_ValidId_ReturnsUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "Test User" };
            UserController.userlist.Add(user);

            // Act
            var result = _userController.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user, result.Model);
        }

        [TestMethod]
        public void Create_ValidUser_AddsUserToList()
        {
            // Arrange
            var user = new User { Name = "Test User" };

            // Act
            var result = _userController.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(UserController.userlist.Contains(user));
        }

        [TestMethod]
        public void Edit_ValidUser_UpdatesUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "Test User" };
            UserController.userlist.Add(user);
            var updatedUser = new User { Id = 1, Name = "Updated User" };

            // Act
            var result = _userController.Edit(1, updatedUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(updatedUser.Name, UserController.userlist.First(u => u.Id == 1).Name);
        }

        [TestMethod]
        public void Delete_ValidId_RemovesUserFromList()
        {
            // Arrange
            var user = new User { Id = 1, Name = "Test User" };
            UserController.userlist.Add(user);

            // Act
            var result = _userController.Delete(1, null) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsFalse(UserController.userlist.Any(u => u.Id == 1));
        }
    }
}
