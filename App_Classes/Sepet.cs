using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.App_Classes
{
    using Models;
    public class Sepet
    {
        //kullanmadan önce newliyoruz.
        private static List<Urunler> urunler=new List<Urunler>();//newleme işlemi yapılmazsa ram'de nesne üretilmemiş olur ve sepete ürün eklemeye çalışıldığında null referance hatası verir.

        public  List<Urunler> Urunler
        {
            get { return urunler; }
            set { urunler = value; }
        }

    }
}