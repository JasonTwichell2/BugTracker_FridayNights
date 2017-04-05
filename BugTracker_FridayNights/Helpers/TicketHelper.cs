using BugTracker_FridayNights.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BugTracker_FridayNights.Helpers
{
    public class TicketHelper
    {
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private static ApplicationDbContext db = new ApplicationDbContext();

        //Help to abstract away the ticket retrieval process and keep it out of the controller
        public List<Ticket> GetUserTickets(string userId)
        {
            var tickets = new List<Ticket>();
            var myRoles = roleHelper.ListUserRoles(userId).ToList();

            if (myRoles.Any(role => role == "Admin"))
                tickets = db.Tickets.ToList();

            else if (myRoles.Any(role => role == "ProjectManager"))           
                tickets = db.Users.Find(userId).Projects.SelectMany(t => t.Tickets).ToList();

            else if (myRoles.Any(role => role == "Developer"))
                tickets = db.Tickets.Where(t => t.AssignedToUserID == userId).ToList();

            else if (myRoles.Any(role => role == "Submitter"))
                tickets = db.Tickets.Where(t => t.OwnerUserId == userId).ToList();

            return tickets;
        }

        public void AddTicketHistory(Ticket oldTicket, Ticket newTicket, string userId)
        {
            //Grab the email address of the unassigned user from the web.config
            var unassignedUser = WebConfigurationManager.AppSettings["Unassigned_User"];
 
            //Obtain the unassigned user id using the retrieved email address
            var unassignedUserId = db.Users.AsNoTracking().First(u => u.Email == unassignedUser).Id;

            foreach (var prop in typeof(Ticket).GetProperties())
            {
                //I want to fix null OwnerUserId and AssignedToUserId properties on both the new and old Tickets
                foreach(var ticket in new List<Ticket>{oldTicket, newTicket})
                {
                    //I am only interested in null values if the prop name contains userid
                    if (prop.Name.ToLower().Contains("userid") && prop.GetValue(ticket) == null)
                        prop.SetValue(ticket, unassignedUserId, null);
                }

                //If these two property vaues are equivalent then we can proceed to the next property
                if (object.ReferenceEquals(prop.GetValue(oldTicket), prop.GetValue(newTicket))) { continue; }
                if (prop.GetValue(oldTicket).Equals(prop.GetValue(newTicket))) { continue; }

                //Otherwise, create a TicketHistory object to populate a new record
                var ticketHistory = new TicketHistory
                {
                    TicketId = oldTicket.Id,
                    Property = prop.Name.ToString(),
                    OldValue = prop.GetValue(oldTicket).ToString(),
                    NewValue = prop.GetValue(newTicket).ToString(),
                    Changed = DateTimeOffset.Now,
                    UserId = userId
                };
                db.TicketHistorys.Add(ticketHistory);
            }
            db.SaveChanges();
        }
        
        public static string GetTicketPriorityNameById(int Id)
        {
            return db.TicketPriorities.AsNoTracking().FirstOrDefault(tp => tp.Id == Id).Name;
        }
        public static string GetTicketStatusNameById(int Id)
        {
            return db.TicketStatuses.AsNoTracking().FirstOrDefault(tp => tp.Id == Id).Name;
        }

        public static string GetTicketTypeNameById(int Id)
        {
            return db.TicketTypes.AsNoTracking().FirstOrDefault(tp => tp.Id == Id).Name;
        }

    }
}