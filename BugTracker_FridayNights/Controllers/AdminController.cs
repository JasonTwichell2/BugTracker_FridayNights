using BugTracker_FridayNights.Helpers;
using BugTracker_FridayNights.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker_FridayNights.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ProjectsHelper ph = new ProjectsHelper();
        public UserRolesHelper urh = new UserRolesHelper();

        //TODO: If Adding & Removing users be sure to create an AddUser Action (Get & Post)
        public ActionResult AddUser(int ProjectId)
        {
            //Users not already on the project
            var userList = ph.UsersNotOnProject(ProjectId);
            ViewBag.UnAssignedUsers = new MultiSelectList(userList, "Id", "DisplayName");

            return View(db.Projects.FirstOrDefault(p => p.Id == ProjectId));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(int ProjectId, List<string> UnassignedUsers)
        {
            if (UnassignedUsers == null)
                return RedirectToAction("Index", "Projects");

            foreach (var userId in UnassignedUsers)
                ph.AddUserToProject(userId, ProjectId);

            return RedirectToAction("Index", "Projects");
        }

        //TODO: If Adding & Removing users be sure to create an RemoveUser Action (Get & Post)
        public ActionResult RemoveUser(int ProjectId)
        {
            //Users currently on the project
            var userList = ph.UsersOnProject(ProjectId);
            ViewBag.AssignedUsers = new MultiSelectList(userList, "Id", "DisplayName");
            return View(db.Projects.FirstOrDefault(p => p.Id == ProjectId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUser(int ProjectId, List<string> AssignedUsers)
        {
            if (AssignedUsers == null)
                return RedirectToAction("Index", "Projects");

            foreach (var userId in AssignedUsers)
                ph.RemoveUserFromProject(userId, ProjectId);

            return RedirectToAction("Index", "Projects");
        }


        //TODO: If Reassigning users be sure to create a ReassignUser Action (Get & Post)
        public ActionResult ReassignUser(int ProjectId)
        {
            //User Id's of those currently on the project
            var assignedUserIds = ph.UserIdsOnProject(ProjectId);
            ViewBag.ReassignUsers = new MultiSelectList(db.Users, "Id", "DisplayName", assignedUserIds);
            return View(db.Projects.FirstOrDefault(p => p.Id == ProjectId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReassignUser(int ProjectId, List<string> ReassignUsers)
        {
            //if (ReassignUsers == null)
            //    return RedirectToAction("Index", "Projects");

            foreach (var userId in ph.UserIdsOnProject(ProjectId))
                ph.RemoveUserFromProject(userId, ProjectId);

            if(ReassignUsers != null)
            {
                foreach(var userId in ReassignUsers)
                    ph.AddUserToProject(userId, ProjectId);
            }

            return RedirectToAction("Index", "Projects");
        }

        public ActionResult ReassignUserByRole(int ProjectId)
        {
            //Users and Users Id's of those currently on the project
            var users = db.Users;

            //Create two lists of users
            var alldevs = new List<ApplicationUser>();
            var allpms = new List<ApplicationUser>();

            //Group them by Role
            foreach (var user in users)
            {
                if (urh.IsUserInRole(user.Id, "Developer"))
                    alldevs.Add(user);
                if (urh.IsUserInRole(user.Id, "Manager"))
                    allpms.Add(user);
            }

            //Create two lists of Assigned Ids
            var assignedDevIds = new List<string>();
            var assignedPMIds = new List<string>();

            //Whom among these are already assigned
            foreach (var user in alldevs)
            {
                if (ph.IsUserOnProject(user.Id, ProjectId))
                    assignedDevIds.Add(user.Id);
            }

            foreach(var pm in allpms)
            {
                if (ph.IsUserOnProject(pm.Id, ProjectId))
                    assignedPMIds.Add(pm.Id);
            }

            ViewBag.ReassignDevs = new MultiSelectList(alldevs, "Id", "DisplayName", assignedDevIds);
            ViewBag.ReassignPMs = new MultiSelectList(allpms, "Id", "DisplayName", assignedPMIds);

            return View(db.Projects.FirstOrDefault(p => p.Id == ProjectId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReassignUserByRole(int ProjectId, List<string> ReassignDevs, List<string> ReassignPMs)
        {
            //Unassign all devs assigned to this project
            foreach (var user in db.Users)
            {
                if (ph.IsUserOnProject(user.Id, ProjectId))
                {
                    if (urh.IsUserInRole(user.Id, "Developer") || urh.IsUserInRole(user.Id, "Manager"))
                        ph.RemoveUserFromProject(user.Id, ProjectId);
                }
            }

            if (ReassignDevs != null)
            {
                foreach (var userId in ReassignDevs)
                    ph.AddUserToProject(userId, ProjectId);
            }

            if (ReassignPMs != null)
            {
                foreach (var userId in ReassignPMs)
                    ph.AddUserToProject(userId, ProjectId);
            }

            return RedirectToAction("Index", "Projects");
        }
    }
}