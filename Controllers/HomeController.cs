using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.App_Classes;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            ViewBag.AktifKullanici= HttpContext.Application["AktifKullanici"];
            ViewBag.ToplamZiyaretci=HttpContext.Application["ToplamZiyaretci"];

            return View();
        }
        public ActionResult CookieAta()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CookieAta(string CookieAdi,string CookieDegeri)
        {
            HttpCookie ck = new HttpCookie(CookieAdi);
            ck.Value=CookieDegeri;
            ck.Expires=DateTime.Now.AddDays(1);//Cookie ne zamana kadar geçerliliğini koruyacak eğer bu değeri biz vermezsek varsayılan olarak oturum sonlanınca oluşturduğumuz çerez silinir.
            Response.Cookies.Add(ck);
            return View();
            //CookieAdi lerin dezavantajları vardır:
            //kullanıcı istediği zaman zamanlanmış cookie leri silebilir.
            //kullanıcı browser dan isterse hiç cookie tutturmayabilir. bizim yazdığımız cookie boşuna olmuş olur.
        }
        public ActionResult CookieOku()
        {
            string deger = Request.Cookies["Grup Kodu"].Value;
            ViewBag.Cerez=deger;
            return View();
        }
        public ActionResult Sepetim()
        {
            List<Urunler> urunler = new List<Urunler>();
            if (Session["AktifSepet"] !=null)
            {
                Sepet s = (Sepet)Session["AktifSepet"];
                urunler =s.Urunler;

            }
            return View(urunler);
        }
    }
}