using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MysqlEntities;
using System.Configuration;
using Parinay.Models;
using System.Threading.Tasks;

namespace Parinay.Controllers
{

    public class HomeController : Controller
    {
        IDataContext _context = new DataContext(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public async Task<ActionResult> Index()
        {
            ViewBag.queryResult = null;
            var studs = await _context.SelectAsync<student>();
            return View(studs);
        }

        [HttpPost]
        public async Task<ActionResult> Index(FormCollection studForm)
        {
            //Save into DB
            var result = await _context.AddAsync(new student
            {
                Name = studForm["txtName"].ToString(),
                Email = studForm["txtEmail"].ToString(),
                DOB = Convert.ToDateTime(studForm["txtDOB"]),
                isActive = 1
            });

            //Get From DB
            ViewBag.queryResult = result;
            var studs = (await _context.SelectAsync<student>());

            //Sending to controller
            return View(studs);
        }

        // /Home/About/{id}
        public async Task<ActionResult> About(int Id)
        {
            var student = (await _context.SelectAsync<student>()).SingleOrDefault(stud => stud.Id == Id);
            return View(student);
        }

        [HttpPost]
        public async Task<ActionResult> About(student s)
        {
            var result = await _context.UpdateAsync(s, "Id={0}", s.Id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Contact(long Id)
        {
            var result = await _context.DeleteAsync<student>("Id={0}", Id);
            return RedirectToAction("Index");
        }
    }
}