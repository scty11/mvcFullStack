using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
           _mockRepository = new Mock<IGigRepository>();

           var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(u => u.Gigs).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUnitOfWork.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "scotty.wright30@gmail.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithIdExists_ReturnsNotFound()
        {
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }
        [TestMethod]
        public void Cancel_GigIsCanceled_ReturnsNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(x => x.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);
            
            result.Should().BeOfType<NotFoundResult>();

        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersGig_ReturnsUnauthorized()
        {
            var gig = new Gig(){ArtistId = _userId + "-"};
            
            _mockRepository.Setup(x => x.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ReturnsOk()
        {
            var gig = new Gig() { ArtistId = _userId };

            _mockRepository.Setup(x => x.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<OkResult>();
        }
        
    }
}
