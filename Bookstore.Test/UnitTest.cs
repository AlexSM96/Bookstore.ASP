using Bookstore.Test.Interfaces;
using Bookstore.Test.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Bookstore.Test
{
    public class UnitTest
    {
        [Fact]
        public void IndexReturnViewWithUsers()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetUsers()).Returns(GetUsersForTest());
            var controller = new HomeController(mock.Object);
            //Act
            var result = controller.Index();
            //Assert
            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(view.Model);
            Assert.Equal(GetUsersForTest().Count(), model.Count());
        }

        [Fact]
        public void AddUserAndRedirectToIndex()
        {
            //Arrange
            var mock = new Mock<IRepository>();
            var controller = new HomeController(mock.Object);
            var user = GetTestUser();
            //Act
            var result = controller.Create(user);
            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(x => x.Create(user));
        }

        [Fact]
        public void GetUser_MustReturnNotFoundWhenUserNotFound()
        {
            // Arrange
            var user = GetTestUser();
            var mock = new Mock<IRepository>();
            _ = mock.Setup(x => x.GetUser(user.Id)).Returns(null as User);
            var controller = new HomeController(mock.Object);

            // Act
            var result = controller.GetUser(user.Id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


        private IEnumerable<User> GetUsersForTest()
        {
            return new List<User>
            {
                new User { Id = 1, Email = "1", Login = "A" },
                new User { Id = 2, Email = "2", Login = "B" },
                new User { Id = 3, Email = "3", Login = "C" },
                new User { Id = 4, Email = "4", Login = "D" },
                new User { Id = 5, Email = "5", Login = "E" },
            };
        }

        private User GetTestUser()
        {
            return new User
            {
                Id = 6,
                Email = "FFFFFFF",
                Login = "FFFFFFFFAAAAAAAAA"
            };
        }
    }
}