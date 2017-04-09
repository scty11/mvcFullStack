﻿using System.Data.Entity.Migrations;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Persistence;
using NUnit.Framework;

namespace GigHub.IntergrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            MigrateDbToLatestVersion();

            Seed();
        }

        private void MigrateDbToLatestVersion()
        {
            var config = new GigHub.Migrations.Configuration();
            var migrator = new DbMigrator(config);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if(context.Users.Any())
                return;
            context.Users.Add(new ApplicationUser()
            {
                UserName = "user1",
                Name = "user1",
                Email = "-",
                PasswordHash = "-"
            });
            
            context.Users.Add(new ApplicationUser()
            {
                UserName = "user2",
                Name = "user2",
                Email = "-",
                PasswordHash = "-"
            });

            context.SaveChanges();
        }
    }
}
