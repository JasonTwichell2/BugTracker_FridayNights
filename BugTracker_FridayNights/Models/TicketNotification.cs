using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker_FridayNights.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }

        [AllowHtml]
        public string Detail { get; set; }

        public bool IsRead { get; set; }

        public DateTime? GeneratedDt { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}