using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class TedarikciController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET: Tedarikci
        public ActionResult Index()
        {
            List<Tedarikciler> ted=ctx.Tedarikciler.ToList();
            return View(ted);
        }
    }
}