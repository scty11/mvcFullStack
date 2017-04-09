﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.IntergrationTests.Extensions;
using GigHub.Persistence;
using NUnit.Framework;

namespace GigHub.IntergrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_shouldReturnUpComingGigs()
        {
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var gig = new Gig()
            {
                Artist = user, DateTime = DateTime.Now.AddDays(1),
                Genre = genre,
                Venue = "-"
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            var result = (ViewResult) _controller.Mine();

            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Update_WhenCalled_shouldUpdateTheGivenGig()
        {
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First(g => g.Id ==1);
            var gig = new Gig()
            {
                Artist = user,
                DateTime = DateTime.Now.AddDays(1),
                Genre = genre,
                Venue = "-"
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            var result = _controller.Update(new GigFormViewModel()
            {
                Id = gig.Id,
                Date = DateTime.Today.AddMonths(1).ToString("d MMM yyy"),
                Time = "20:00",
                Genre = 2,
                Venue = "Venue"
            });

            _context.Entry(gig).Reload();
            gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            gig.Venue.Should().Be("Venue");
            gig.GenreId.Should().Be(2);
        }
    }
}
