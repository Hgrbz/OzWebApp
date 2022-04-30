using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication4.App_Classes;

namespace WebApplication4.Controllers

{
    [Authorize]
    public class UyeController : Controller
    {
        [AllowAnonymous]//Authorise'ı ezerek login olmadan da action'a erişebilmeyi sağlar.
        // GET: Uye
        public ActionResult GirisYap()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GirisYap(Kullanici k, string Hatirla)
        {
            bool girisYap = Membership.ValidateUser(k.KullaniciAdi, k.Parola);

            if (girisYap==true)
            {
                if (Hatirla=="on")//yani beni hatırla checkbox işaretlendiyse
                {
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, true); /*bu methodun ikinci parametresi,ilk parametrede aldığı kullanıcının cookie sini oluşturup oluşturmamaya yarar.*/
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, false);
                }
                return RedirectToAction("index", "Home");
            }
            else
            {
                ViewBag.Mesaj="Kullanıcı adı veya parola hatalı.";
                return View();
            }

        }

        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap");
        }
        [AllowAnonymous]
        public ActionResult ParolamiUnuttum()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ParolamiUnuttum(Kullanici k)//parolami unuttum formundan gelen bilgileri parametre olarak alıyoruz.
        {
            MembershipUser mu = Membership.GetUser(k.KullaniciAdi);
            if (mu.PasswordQuestion==k.GizliSoru)
            {
                
                string pwd = mu.ResetPassword(k.GizliCevap);//cevabı gizlicevap olan parolayı getir.
                mu.ChangePassword(pwd, k.Parola);
                return RedirectToAction("GirisYap");

            }
            else
            {
                ViewBag.Mesaj="Girilen bilgiler doğru değil.";
                return View();
            }
        }
    }
}