using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class UrunController : Controller
    {
        NorthwindEntities con = new NorthwindEntities();
        // GET: Urun
        public ActionResult Index()
        {
            List<Urunler> urunler = con.Urunler.ToList();
            return View(urunler);
        }
        public ActionResult UrunEkle()
        {
            ViewBag.kategori=con.Kategoriler.ToList();
            ViewBag.tedarikci=con.Tedarikciler.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urunler u)
        {
            con.Urunler.Add(u);
            con.SaveChanges();
            return RedirectToAction("index", "urun");
        }
        public ActionResult UrunSil(int id)//bu int id şuradan geliyor: index cshtml içindeki sil butonunun u.urunid.
        {
            Urunler u = con.Urunler.FirstOrDefault(i => i.UrunID==id);

            return View(u);

        }

        [HttpPost]
        public ActionResult UrunSil(Urunler u) // burada urunler tipinde bir değişken var ürünler sınıfında ise UrunID diye bir property var onu view daki name özelliğine yazarak eşleştiriyoruz.
        {
            Urunler urn = con.Urunler.FirstOrDefault(x => x.UrunID==u.UrunID);
            con.Urunler.Remove(urn);
            con.SaveChanges();
            return RedirectToAction("index");
        }
    }
}