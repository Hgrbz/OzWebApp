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
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        
        public ActionResult Index()
        {
            //1) ben tüm kullanıcıları listelemek istiyorum.
            //getAllUsers methodu geriye membershipUserCollection tipini döndürdüğü için biz bu türde bir değişken oluşturabiliriz.
            //getAllUsers, veritabanındaki users tablosunda bulunan tüm kullanıcıları getirir.
            MembershipUserCollection users = Membership.GetAllUsers();
            return View(users);
        }
        [AllowAnonymous]//kullanıcı kendini yeni ekleyeceğinden login şartı olmamalı.
        public ActionResult Ekle()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Ekle(Kullanici k)

        {
            MembershipCreateStatus durum;
            Membership.CreateUser(k.KullaniciAdi, k.Parola, k.Email, k.GizliSoru, k.GizliCevap, true, out durum);
            string mesaj = "";
            switch (durum)//switch durum yazıp enter'a basınca aşağıdaki case'leri otomatik getirir. bunlar enum içindeki değerlerdir.Yani MembershipCreateStatus enumu içindeki.
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj+="geçersiz kullanıcı anahtar hatası";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj+="geçersiz parola";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj+="geçersiz gizli soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj+="geçersiz gizli cevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj+="geçersiz mail adresi";

                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj+="bu Kullanıcı adı önceden alınmış.";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj+="bu mail daha önceden alınmış";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj+="kullanıcı reddedildi";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    mesaj+="geçersiz kullanıcı anahtar hatası";

                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj+="bu mail daha önceden alınmış key hatası";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj+="sağlayıcı hatası";
                    break;
                default:
                    break;
            }
            ViewBag.Mesaj=mesaj;
            if (durum==MembershipCreateStatus.Success)//MembershipCreateStatus.Success yerine o case'e karşılık gelen 0 dayazılabilirdi.
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }


        }
        [Authorize(Roles = "Admin")]// sadece admin bu action'a erişebilsin(sadece admin rol ekleme sayfasına erişsin.)
        public ActionResult RolAta(string id)//burada string id dememize rağmen aslında ismin kendisi gelmekte. Bunun sebebi bu bir get action'ı olduğu için default config'de ilgili parametre adı olarak id verilmiş olmasıdır. Aynı metodun postunda ise normal isim yazılacak.
        {
            ViewBag.Roller=Roles.GetAllRoles().ToList();
            /*ViewBag.Isim=id;*/ //böyle diyerek boş view gönderilebileceği gibi aşağıdaki gibi de yapılabilir.
            return View((object)id);//burada şu yüzden object dedik:normalde default olarak route configde id int olarak tanımlı.ama biz isim göndereceğimiz için (adına id desek de) string olarak parametre verdik. kullanıcı nın index sayfasındaki route'da @mu.UserName string olarak name alıyor. int string çakışmasını önlemek için object tipinde veri gönderdik. )
        }
        [Authorize(Roles = "Admin")]// sadece admin bu action'a erişebilsin(sadece admin rol ekleme sayfasına erişsin.)
        [HttpPost]
        public ActionResult RolAta(string kullaniciAdi, string rolAdi)//buradaki parametre adları view 'daki ilgili yerlerin name özellikleri ile aynı olmalıdır. yoksa view'dan seçilen değerler post edildiğinde post action'ına gelmez.
        {
            Roles.AddUserToRole(kullaniciAdi, rolAdi);
            
            return RedirectToAction("Index");//tekrardan index actionuna gitmesini sağlayarak yeniden kullanıcıların yüklenmesini sağlıyoruz direkt index diyince view'daki Modal null referance hatası veriyor
        }
        [HttpPost]
        public string UyeRolleri(string kullaniciAdi)
        {
            List<string> roller = Roles.GetRolesForUser(kullaniciAdi).ToList();
            string rol = "";


            foreach (string r in roller)
            {
                rol+=r+"-";

            }
            if (rol.Length>0)
            {
                rol=rol.Remove(rol.Length-1, 1);
            }

            return rol;

        }
    }
}