namespace BugTracker_FridayNights.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker_FridayNights.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BugTracker_FridayNights.Models.ApplicationDbContext context)
        {
            #region Role section
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            var role = new IdentityRole();

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                role = new IdentityRole { Name = "Developer" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                role = new IdentityRole { Name = "Submitter" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                role = new IdentityRole { Name = "ProjectManager" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Guest"))
            {
                role = new IdentityRole { Name = "Guest" };
                manager.Create(role);
            }
            #endregion

            #region User section
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(u => u.Email == "jtwichell@coderfoundry.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "jtwichell@coderfoundry.com",
                    Email = "jtwichell@coderfoundry.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "JTWICH"
                };

                userManager.Create(user, "Abc&123!");

                userManager.AddToRoles(user.Id,
                new string[] {
                    "Admin",
                    "ProjectManager",
                    "Developer",
                    "Submitter"
                });
            }

            if (!context.Users.Any(u => u.Email == "AdminDemo@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "AdminDemo@mailinator.com",
                    Email = "AdminDemo@mailinator.com",
                    FirstName = "Admin",
                    LastName = "Demo",
                    DisplayName = "Admin Demo"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "PMDemo@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "PMDemo@mailinator.com",
                    Email = "PMDemo@mailinator.com",
                    FirstName = "PM",
                    LastName = "Demo",
                    DisplayName = "PM Demo"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "DeveloperDemo@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DeveloperDemo@mailinator.com",
                    Email = "DeveloperDemo@mailinator.com",
                    FirstName = "Developer",
                    LastName = "Demo",
                    DisplayName = "Developer Demo"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "SubmitterDemo@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "SubmitterDemo@mailinator.com",
                    Email = "SubmitterDemo@mailinator.com",
                    FirstName = "Submitter",
                    LastName = "Demo",
                    DisplayName = "Submitter Demo"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "unassigned@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "unassigned@mailinator.com",
                    Email = "unassigned@mailinator.com",
                    FirstName = "",
                    LastName = "",
                    DisplayName = ""
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "Developer_1@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Developer_1@mailinator.com",
                    Email = "Developer_1@mailinator.com",
                    FirstName = "David",
                    LastName = "Developer",
                    DisplayName = "David Developer"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "Developer_2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Developer_2@mailinator.com",
                    Email = "Developer_2@mailinator.com",
                    FirstName = "Marvin",
                    LastName = "Developer",
                    DisplayName = "Marvin Developer"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "Submitter_1@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Submitter_1@mailinator.com",
                    Email = "Submitter_1@mailinator.com",
                    FirstName = "Steven",
                    LastName = "Submitter",
                    DisplayName = "Steven Submitter"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "Submitter_2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Submitter_2@mailinator.com",
                    Email = "Submitter_2@mailinator.com",
                    FirstName = "Alex",
                    LastName = "Submitter",
                    DisplayName = "Alex Submitter"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "ProjectManager_1@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ProjectManager_1@Mailinator.com",
                    Email = "ProjectManager_1@Mailinator.com",
                    FirstName = "Paula",
                    LastName = "ProjectManager",
                    DisplayName = "Paula PM"
                }, "Abc&123!");
            }

            if (!context.Users.Any(u => u.Email == "ProjectManager_2@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ProjectManager_2@Mailinator.com",
                    Email = "ProjectManager_2@Mailinator.com",
                    FirstName = "Patricia",
                    LastName = "ProjectManager",
                    DisplayName = "Patricia PM"
                }, "Abc&123!");
            }
            #endregion

            #region User Roles section
            var userId = userManager.FindByEmail("unassigned@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("AdminDemo@Mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("PMDemo@Mailinator.com").Id;
            userManager.AddToRole(userId, "ProjectManager");

            userId = userManager.FindByEmail("DeveloperDemo@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("SubmitterDemo@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("ProjectManager_1@Mailinator.com").Id;
            userManager.AddToRole(userId, "ProjectManager");

            userId = userManager.FindByEmail("ProjectManager_2@Mailinator.com").Id;
            userManager.AddToRole(userId, "ProjectManager");

            userId = userManager.FindByEmail("Developer_1@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("Developer_2@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("Submitter_1@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("Submitter_2@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            #endregion

            #region Ticket Priorities section
            // Ticket Priorities
            if (!context.TicketPriorities.Any(u => u.Name == "High"))
            { context.TicketPriorities.Add(new TicketPriority { Name = "High" }); }

            if (!context.TicketPriorities.Any(u => u.Name == "Medium"))
            { context.TicketPriorities.Add(new TicketPriority { Name = "Medium" }); }

            if (!context.TicketPriorities.Any(u => u.Name == "Low"))
            { context.TicketPriorities.Add(new TicketPriority { Name = "Low" }); }

            if (!context.TicketPriorities.Any(u => u.Name == "Urgent"))
            { context.TicketPriorities.Add(new TicketPriority { Name = "Urgent" }); }
            #endregion

            #region Ticket Types section
            // Ticket Types - Completely Arbitrary and up to the Developer
            if (!context.TicketTypes.Any(u => u.Name == "Production Fix"))
            { context.TicketTypes.Add(new TicketType { Name = "Production Fix" }); }

            if (!context.TicketTypes.Any(u => u.Name == "Project Task"))
            { context.TicketTypes.Add(new TicketType { Name = "Project Task" }); }

            if (!context.TicketTypes.Any(u => u.Name == "Software Update"))
            { context.TicketTypes.Add(new TicketType { Name = "Software Update" }); }
            #endregion

            #region Ticket Statuses section
            // Ticket Types - Completely Arbitrary and up to the Developer
            if (!context.TicketStatuses.Any(u => u.Name == "New"))
            { context.TicketStatuses.Add(new TicketStatus { Name = "New" }); }

            if (!context.TicketStatuses.Any(u => u.Name == "In Development"))
            { context.TicketStatuses.Add(new TicketStatus { Name = "In Development" }); }

            if (!context.TicketStatuses.Any(u => u.Name == "Completed"))
            { context.TicketStatuses.Add(new TicketStatus { Name = "Completed" }); }
            #endregion

            #region Projects section
            // Projects
            if (!context.Projects.Any(u => u.Name == "The Hubble Project"))
            { context.Projects.Add(new Project { Name = "The Hubble Project" }); }

            if (!context.Projects.Any(u => u.Name == "The Artemis Project"))
            { context.Projects.Add(new Project { Name = "The Artemis Project" }); }

            if (!context.Projects.Any(u => u.Name == "The Apollo Project"))
            { context.Projects.Add(new Project { Name = "The Apollo Project" }); }

            if (!context.Projects.Any(u => u.Name == "The Clementine Project"))
            { context.Projects.Add(new Project { Name = "The Clementine Project" }); }
            #endregion
        }
    }
}
