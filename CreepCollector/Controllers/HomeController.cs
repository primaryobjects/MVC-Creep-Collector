using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreepCollector.Models;

namespace CreepCollector.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Creep creep)
        {
            // Validate the model server-side.
            if (ModelState.IsValid)
            {
                // Do something with the submitted data ..

                // Redirect to confirmation page.
                return RedirectToAction("Confirm", new { name = creep.Name });
            }
            else
            {
                // Let the user correct any errors.
                return View(creep);
            }
        }

        public ActionResult Confirm(string name)
        {
            TempData["Name"] = name;

            return View();
        }
    }
}
