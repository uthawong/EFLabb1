using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFLabb1.Data;
using EFLabb1.Models;

namespace EFLabb1.Controllers
{
    public class LeavesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeavesController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Metoden hämtar alla Leaves från databasen (SQL) inkl. info om den tillhörande employee för varje leave-post och sedan skickar listan
        //med leaves till VYN för att visas för användaren.
        public IActionResult Index()
        {
            var Leaves = _context.Leaves.Include(l => l.Employee).ToList();
            return View(Leaves);
        }
        //[HttpGet] är som en skylt som säger att den ska svara när någon frågar efter en sida eller data. Om jag skulle sälja en produkt i min hemsida så skulle
        //    jag sätta HttpGet på den metoden som hämtar produkten från databasen och visar den på hemsidan.
        [HttpGet]
        public IActionResult Apply()
        {
            // Hämta alla anställd för att fylla i en propdown Lista sen
            ViewBag.Employees = _context.Employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = e.EmployeeName
            }).ToList();
            return View();
        }
        [HttpPost]  //Används för att köra en metod när data skickas till serverna via en HTTP-Post-fråga

        public IActionResult Apply(Leave Leave)
        {
            if (ModelState.IsValid)
            {
                _context.Leaves.Add(Leave);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employees = _context.Employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = e.EmployeeName
            }).ToList();
            return View(Leave);
        }
        [HttpGet]  //Hämta
        public IActionResult EditStatus(int id)
        {
            var leave = _context.Leaves.Include(l => l.Employee).FirstOrDefault(l => l.LeaveId == id);
            if (leave == null)
            {
                return NotFound();
            }
            return View(leave);
        }
        [HttpPost] //Post
        public IActionResult EditStatus(Leave model)
        {
            var leave = _context.Leaves.Find(model.LeaveId);
            if (leave == null)
            {
                return NotFound();
            }
            leave.Status = model.Status;
            _context.SaveChanges();
            return RedirectToAction("Index");
            return View(model);
        }
    }
}
