using System;
using System.Web;
using GigHub.Controllers;
using GigHub.Core.ViewModels;
using GigHub.FunctionalUITests.PageObjectModels;
using NUnit.Framework;

namespace GigHub.FunctionalUITests
{
    [TestFixture]
    public class UnitTest1
    {

        [Test]
        public void TestMethod1()
        {

            var login = BrowserHost.Instance.NavigateToInitialPage<AccountController, LoginPage>(
                x => x.Login("/Home/Index"));
            
            var loginViewModel = new LoginViewModel()
            {
                Email = "Test@Test.com",
                Password = "Testing1/"
            };

            login.Login<GigsPage>(loginViewModel);
            
            var addGigPage =
                BrowserHost.Instance.NavigateToInitialPage<GigsController, AddGigPage>(
                    x => x.Create());
           Read
            var gig = new GigFormViewModel()
            {
                Date = DateTime.Today.AddMonths(1).ToString("d MMM yyy"),
                Time = "20:00",
                Genre = 2,
                Venue = "Venue"
            };

            var result = addGigPage.AddGig<GigsPage>(gig);
            var v = result.GetName(x => x.UpcomingGigs);
            var t = 1;

        }
    }
}
