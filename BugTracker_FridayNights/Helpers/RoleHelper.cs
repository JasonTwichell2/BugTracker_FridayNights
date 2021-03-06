﻿using BugTracker_FridayNights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker_FridayNights.Helpers
{
    public class RoleHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static string GetRoleNameById(string Id)
        {
            return db.Roles.FirstOrDefault(r => r.Id == Id).Name;
        }
    }
}