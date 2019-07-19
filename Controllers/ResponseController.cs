using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class ResponseController : Controller
    {
        private HomeContext dbContext;
        public ResponseController(HomeContext context)
        {
            dbContext = context;
        }

        [HttpGet("/rsvp/{WeddingId}/{Status}")]
        public IActionResult RSVP(int WeddingId, String Status)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction ("LogOut", "Home");
            }
            else
            {
                if(Status == "add")
                {
                    Reservation NewRsvp = new Reservation();
                    NewRsvp.UserId = (int)HttpContext.Session.GetInt32("UserId");
                    NewRsvp.WeddingId = WeddingId;
                    dbContext.Rsvps.Add(NewRsvp);
                    dbContext.SaveChanges();
                    return RedirectToAction("Dashboard", "Wedding");
                }
                if(Status == "remove")
                {
                    Reservation remove = dbContext.Rsvps.FirstOrDefault(r => r.WeddingId == WeddingId && r.UserId == HttpContext.Session.GetInt32("UserId"));

                    dbContext.Rsvps.Remove(remove);
                    dbContext.SaveChanges();
                    return RedirectToAction("Dashboard", "Wedding");

                }
                return View("Dashboard");
            }
        }

    }
}