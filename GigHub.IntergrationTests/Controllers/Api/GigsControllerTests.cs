using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Tests.Extensions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GigHub.IntergrationTests.Controllers.Api
{
    [TestFixture()]
    public class GigsControllerTests
    {
        private ApplicationDbContext _context;
        private GigsController _controller;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigHub.Controllers.Api.GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test,Isolated]
        public void Cancel_WhenCalled_ShouldCancelGig()
        {
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id,user.Name);

            var genre = _context.Genres.First(g => g.Id == 1);
            var gig = new Gig()
            {
                Artist = user,
                DateTime = DateTime.Now.AddDays(1),
                Genre = genre,
                Venue = "-"
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            var result = _controller.Cancel(gig.Id);

            _context.Entry(gig).Reload();
            gig.IsCanceled.Should().BeTrue();
        }
    }
}
