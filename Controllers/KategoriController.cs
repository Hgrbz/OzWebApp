using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class KategoriController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET: Kategori
        public ActionResult Index()
        {
            List<Kategoriler> kat = ctx.Kategoriler.ToList();
            return View(kat);
        }
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Kategoriler k)
        {
            ctx.Kategoriler.Add(k);
            ctx.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpPost]
        public void Sil(int id)
        {
            Kategoriler k = ctx.Kategoriler.FirstOrDefault(x => x.KategoriID==id);
            ctx.Kategoriler.Remove(k);
            ctx.SaveChanges();

        }
    }
}