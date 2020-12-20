using System;
using System.Collections;
using System.Threading;

namespace AsansorManager.Threadler
{
    public class Giris
    {
        Random randomKisi, randomKat; // Rastgele sınıfından kisi ve kat değişkenlerini tanımlıyoruz
        public static ArrayList girisKuyruk = new ArrayList(); //Giriş katındaki kuyruğu tutan ArrayList değişkenini tanımlıyoruz

        public int KisiSayisiUret() //Rastgele kişi sayısı üreten method
        {
            randomKisi = new Random(); //Oluşturduğumuz değişkeni new'liyoruz
            return randomKisi.Next(1, 10); //1-10 arasında rastgele sayı üretip geri döndürür
        }

        public int GidilecekKatUret() //Rastgele gidilecek kat üreten method
        {
            randomKat = new Random(); //Oluşturduğumuz değişkeni new'liyoruz
            return randomKat.Next(1, 5); //1-5 arasında rastgele sayı üretip geri döndürür
        }

        public void KuyrugaEkle() //Giriş kuyruğuna kişiler ve katlarını ekleyen method (Giriş yapacaklar kuyruğu)
        {
            for (int i = 0; i < 100; i++) // 100 setlik döngü oluşturuyoruz (100 adet kişiler ve katlar listesi => [3,6],...)
            {
                int kisiSayisi = KisiSayisiUret(); //Kişi sayısı üretip değişkene aktarıyoruz
                int gidilecekKat = GidilecekKatUret(); //Gidilecek katı üretip değişkene aktarıyoruz
                int[] kisiler = { kisiSayisi, gidilecekKat }; // Oluşturulan değerleri array içine atıyoruz
                girisKuyruk.Add(kisiler); // Giriş kuyruğu listesine, oluşturduğumuz array'i ekliyoruz
                Thread.Sleep(500); // 500ms bekliyoruz
            }
        }
    }
}
