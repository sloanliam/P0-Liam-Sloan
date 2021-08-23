using System;
using Xunit;
using Model;
using Model.Entities;
using View;
using Controller;

namespace AppTesting
{
    public class UnitTest1
    {

        [Fact]
        public void TestIfAdmin()
        {
            MainMenu menu = new MainMenu(new AppController(new Model.AppRepo(new Model.Entities.revtrainingdbContext())));
            var adminStatus = menu.GetAdminStatus("admin");

            bool expectedResult = true;

            Assert.Equal(expectedResult, adminStatus);
        }

        [Fact]
        public void CheckExceptionHandlingAdminTest(){
            MainMenu menu = new MainMenu(new AppController(new Model.AppRepo(new Model.Entities.revtrainingdbContext())));
            var adminStatus = menu.GetAdminStatus("none");

            bool expectedResult = false;

            Assert.Equal(expectedResult, adminStatus);
        }

        [Fact]
        public void CouldNotLocateRestaurant(){
            AppController appController = new AppController(new Model.AppRepo(new Model.Entities.revtrainingdbContext()));
            var findRestaurant = appController.RecommendRestaurant(00000);

            string expectedResult = "There aren't any highly reviews restaurants in your area.";

            Assert.Equal(expectedResult, findRestaurant);
        }

        [Fact]
        public void FindUser(){
            AppController appController = new AppController(new Model.AppRepo(new Model.Entities.revtrainingdbContext()));
            var user = appController.FindUser(99);

            string? expectedResult = "This user cannot be found.";

            Assert.Equal(expectedResult, user);
        }

        [Fact]
        public void LogInError(){
            AppController appController = new AppController(new Model.AppRepo(new Model.Entities.revtrainingdbContext()));
            var loginAttempt = appController.LogIn("none", "none");

            bool expectedResult = false;

            Assert.Equal(expectedResult, loginAttempt);
        }
    }
}
