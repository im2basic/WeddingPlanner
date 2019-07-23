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
    [Route("Wedding")]
    public class WeddingController : Controller
    {
        private HomeContext dbContext;
        //Change this controller to new name
        public WeddingController(HomeContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult DashBoard()
        {
            if(HttpContext.Session.GetInt32("UserId") == null )
            {
                return RedirectToAction ("LogOut","Home");
            }
            else 
            {
                //This code Compares Dates and does not display past dates
                List<Wedding> AllWeddings = dbContext.Weddings.Include(w => w.GuestList)
                .ThenInclude(r => r.Guest).Where(c => c.WeddingDate > DateTime.Now).ToList();
                
                ViewBag.User = dbContext.Users.Include(u => u.CreatedWeddings).Include(u => u.Weddings).ThenInclude( r => r.Attending).FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));

                return View ("Dashboard",AllWeddings);
                
            }
        }
            [HttpGet("new")]
            public IActionResult NewWedding()
            {
                ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
                return View();
            }

            [HttpPost("create")]
            public IActionResult CreateWedding(Wedding newWedding)
            {
                Console.WriteLine("Print 1");
                if(HttpContext.Session.GetInt32("UserId") == null)
                {
                    return RedirectToAction ("LogOut", "Home");
                }
                else
                {
                    if(ModelState.IsValid)
                    {
                        Console.WriteLine("Print 2");
                        dbContext.Weddings.Add(newWedding);
                        dbContext.SaveChanges();
                        return Redirect($"/wedding/show/{newWedding.WeddingId}");
                    }
                    else
                    {
                        Console.WriteLine("Print 3");
                        ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
                        return View("NewWedding");
                    }
                }
            }

            [HttpGet("/wedding/show/{WeddingId}")]
            public IActionResult ShowWedding(int WeddingId)
            {
                if(HttpContext.Session.GetInt32("UserId") == null)
                {
                    return RedirectToAction ("LogOut", "Home");
                }
                else
                {
                    Wedding myWedding = dbContext.Weddings.Include(w => w.GuestList).ThenInclude( r => r.Guest).FirstOrDefault(w => w.WeddingId == WeddingId);

                    ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
                    return View("ShowWedding", myWedding);
                }
            }

            [HttpGet("/destroy/{WeddingId}")]
            public IActionResult Destroy(int WeddingId)
            {
                if(HttpContext.Session.GetInt32("UserId") == null)
                {
                    return RedirectToAction ("LogOut", "Home");
                }
                else
                {
                    Wedding RemoveWedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == WeddingId);
                    if(RemoveWedding == null)
                    {
                        return RedirectToAction("Dashboard");

                    }
                    dbContext.Weddings.Remove(RemoveWedding);
                    dbContext.SaveChanges();
                    return RedirectToAction("Dashboard");
                }
            }
        
    }
}