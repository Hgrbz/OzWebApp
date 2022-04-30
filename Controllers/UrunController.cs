using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using WebApplication4.App_Classes;

namespace WebApplication4.Controllers
{
    [Authorize]
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
        public ActionResult UrunDetay()
        {   //Client side ->querystring yöntemi ile state management işlemi
            //queystring yöntemi olmadan da varsayılan olarak routeconfig sayesinde query string'e'1 adet id parametresi koyabiliriz. ama querystring sayesinde birden fazla parametre & işareti ile yan yana yazılabilir.
            //yine de yüvenlik ve SEO sorunları sebebiyle çok tavsiye edilen bir yöntem değildir.
            int id = Convert.ToInt32(Request.QueryString["id"].ToString());
            string adi = Request.QueryString["adi"].ToString();//bu kodun çalışması için bir view olmalıdır.
            Urunler u = con.Urunler.FirstOrDefault(x => x.UrunID==id);
            return View(u);
        }

        [HttpPost]
        public void SepeteAt(int id)//bize sepete eklemek istediğimiz ürünün id si geliyor.
        { 
            Sepet s;
            if (Session["AktifSepet"]==null)
            {
                 s=new Sepet();//sepet oluşturulmadıysa oluştur.
                

            }
            else
            {
                s = (Sepet)Session["AktifSepet"];
            }
            //önce bize parametre olarak gelen eklenecek ürün id'sine göre veritabanından o ürünü bulmamız gerekiyor.
                Urunler u = con.Urunler.FirstOrDefault(x => x.UrunID==id);
                //sonra da ekliyoruz sepete ürünü.
                s.Urunler.Add(u);
                Session["AktifSepet"]=s;//session propertysi geriye object nesnesi döndürür.bu işlem yapılmazsa else durumunda aktif sepet session'ında sepet nesnesi cast edilemeyeceği için(sepet nesnesi daha önce tanımlanmadığından) hata verir. bu işlem sayesinde aktif sepet içine sepet nesnesini ekledik bundan sonra else durumunda aktif nesne içine sepet nesnesi eklenmiş olacak.
        }
    }
}