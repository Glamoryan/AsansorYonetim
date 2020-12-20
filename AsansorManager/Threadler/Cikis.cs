using System;
using System.Collections;
using System.Threading;

namespace AsansorManager.Threadler
{
    public class Cikis
    {
        Random randomKisi, randomKat;// Rastgele sınıfından kisi ve kat değişkenlerini tanımlıyoruz

        //Zemin kat dışındaki kuyrukları burada public olarak tanımlıyoruz çünkü başka classlardan erişeceğiz
        public static ArrayList birinciKat = new ArrayList();
        public static ArrayList ikinciKat = new ArrayList();
        public static ArrayList ucuncuKat = new ArrayList();
        public static ArrayList dorduncuKat = new ArrayList();

        public int KisiSayisiUret() //Rastgele kişi sayısı üreten method
        {
            randomKisi = new Random(); //Oluşturduğumuz değişkeni new'liyoruz
            return randomKisi.Next(1, 5); //1-5 arasında rastgele sayı üretip geri döndürür
        }

        public int GidilecekKatUret() //Rastgele gidilecek kat üreten method
        {
            randomKat = new Random(); //Oluşturduğumuz değişkeni new'liyoruz
            return randomKat.Next(1, 5); //1-5 arasında rastgele sayı üretip geri döndürür
        }

        public void KuyrugaEkle() //Diğer kuyruklara kişiler ve katlarını ekleyen method (Çıkış yapacaklar kuyruğu)
        {
            for (int i = 0; i < 100; i++) // 100 setlik döngü oluşturuyoruz (100 adet kişiler ve katlar listesi => [3,6],...)
            {
                int kisiSayisi = KisiSayisiUret(); //Kişi sayısı üretip değişkene atıyoruz
                int beklenilenKat = GidilecekKatUret(); //Rastgele kat üretip değişkene atıyoruz (Çıkış yapacakların bekleyecekleri kat)
                int[] kisiler = { kisiSayisi, 0 };  //Kişi sayısı ve gidilecek katı (0.kat çünkü çıkış yapacak) array'e atıyoruz

                if (beklenilenKat == 1) //Eğer beklenilen kat 1.kat ise bu block çalışır
                    birinciKat.Add(kisiler); //1.Katın kuyruğuna kişilerimizi ekleriz
                else if (beklenilenKat == 2)//Eğer beklenilen kat 2.kat ise bu block çalışır
                    ikinciKat.Add(kisiler); //2.Katın kuyruğuna kişilerimizi ekleriz
                else if (beklenilenKat == 3)//Eğer beklenilen kat 3.kat ise bu block çalışır
                    ucuncuKat.Add(kisiler);//3.Katın kuyruğuna kişilerimizi ekleriz
                else if (beklenilenKat == 4)//Eğer beklenilen kat 4.kat ise bu block çalışır
                    dorduncuKat.Add(kisiler);//4.Katın kuyruğuna kişilerimizi ekleriz

                Thread.Sleep(1000); //Bir saniye bekliyoruz
            }
        }
    }
}
