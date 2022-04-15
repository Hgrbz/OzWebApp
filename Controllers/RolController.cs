using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {
            List<string> roller= Roles.GetAllRoles().ToList();
            return View(roller);
        }
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(string RolAdi)
        {
            Roles.CreateRole(RolAdi);
            return RedirectToAction("index");
            
               
        }
    }
}