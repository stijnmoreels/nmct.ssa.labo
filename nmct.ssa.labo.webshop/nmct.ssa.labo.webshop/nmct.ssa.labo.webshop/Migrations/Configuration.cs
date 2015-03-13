namespace nmct.ssa.labo.webshop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using nmct.ssa.labo.webshop.businesslayer.Context;
    using nmct.ssa.labo.webshop.DatabaseHelper;
    using nmct.ssa.labo.webshop.models;
    using nmct.ssa.labo.webshop.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            DatabaseSeed seed = new DatabaseSeed();

            seed.InsertOs(context);
            seed.InsertFrameWork(context);
            seed.InsertDevice(context);

            InsertIdentity(context);

        }

        private static void InsertIdentity(ApplicationDbContext context)
        {
            string admin = "Admin";
            IdentityResult result;

            var manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!manager.RoleExists(admin))
                result = manager.Create(new IdentityRole(admin));

            if (!context.Users.Any(u => u.Email.Equals("stijn.moreels@student.howest.be")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Moreels",
                    Fristname = "Stijn",
                    Email = "stijn.moreels@student.howest.be",
                    UserName = "stijn.moreels@student.howest.be",
                    Address = "Grensstraat 89",
                    City = "Menen",
                    Zipcode = "8930"
                };
                userManager.Create(user, "-Password1");
                userManager.AddToRole(user.Id, admin);
            }
        }
    }
}
