using AsansorManager.Threadler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsansorManager.Asansorler
{
    public class Asansor4:Asansor //Asansör abstract class'ından inheritance alan class
    {
        int toplamKisi = 0; //Asansordeki toplam kisiyi tutan değişken
        List<int[]> binenler, inenler; //Binen ve inen kisilerin tutulduğu listler        

        //Parametreli constructor
        public Asansor4(string asansorAdi)
        {
            asansorAd = asansorAdi; //Parametre ile gelen değeri asansör adına atıyoruz
        }

        public void MusteriAl(ArrayList kuyruk) //Müşterileri kuyruklardan alan method
        {
            if (binenler == null) //Eğer binenler listesi oluşturulmamışsa bu block çalışır
                binenler = new List<int[]>(); //binenler listesini new'liyoruz

            // Kuyruğumuz adındaki array listesi(array of array) tutan değişkenimize, => int[][] kuyrugumuz
            // parametreyle verilen ArrayList tipindeki listeyi Cast (Çevirme) ediyoruz, => (int[][])kuyruk
            // daha sonra array'e çeviriyoruz => kuyruk.ToArray(typeof(int[]))
            int[][] kuyrugumuz = (int[][])kuyruk.ToArray(typeof(int[]));
            for (int i = 0; i < kuyrugumuz.Length; i++) //Kuyruğun uzunluğu kadar bir döngü açıyoruz
            {
                if (toplamKisi == 10) //Eğer asansordeki toplam kişi sayısı 10 ise bu block çalışır
                {
                    count_inside = 10; //Asansor içindeki kişi sayısını tutan property'mize 10'u set ediyoruz
                    break; // Döngüyü bitiriyoruz
                }
                else //Eğer asansordeki toplam kişi sayısı 10 değilse bu block çalışır
                {
                    // Toplam kişiye değişkenine gezdiğimiz kuyruğunun 0. elemanını ekliyoruz çünkü 0. eleman kişi sayısı, 1. elaman gidilecek kat 
                    toplamKisi += kuyrugumuz[i][0];
                    if (toplamKisi > 10) //Eğer toplam kisi sayısı 10'u geçtiyse bu block çalışır
                    {
                        toplamKisi -= kuyrugumuz[i][0]; //Toplam kişi sayısı asansor limitini geçtiği için eklediğimiz kişileri çıkartıyoruz
                        continue; //Döngüyü tekrar arttırıyoruz
                    }
                    else //Eğer toplam kisi sayısı 10'u geçmediyse bu block çalışır
                    {
                        binenler.Add(kuyrugumuz[i]); // Binenler listemize gezdiğimiz seti ekliyoruz (örnek;[3,4])
                    }
                }
                count_inside = toplamKisi; //Asansor içindeki kişi sayısını tutan property'mize toplam kisiyi set ediyoruz çünkü her zaman 10 kişi binemeyebilir
            }
            inside = binenler; // Detayları tutan property'mize binenler list'imizi ekliyoruz
        }

        public void HedefeGotur() //Müşterileri katlara götüren method
        {
            mode = true; // Asansörün modunu true yapıyoruz yani aktif
            if (binenler.Count > 0) //Eğer binenler listemiz boş değilse bu block çalışır
            {
                inenler = new List<int[]>(); //Inenler listemizi new'liyoruz
                for (int i = 0; i < binenler.Count; i++) //Binenlerin uzunluğu kadar bir döngü açıyoruz
                {
                    if (floor < binenler[i][1]) //Eğer bulunduğumuz kat, gezdiğimizi setteki 1.elemandan küçükse (1.eleman gidilecek kat) bu block çalışır
                        direction = "yukarı"; //Asansör yönünü yukarı yapıyoruz
                    else if (floor > binenler[i][1]) //Eğer bulunduğumuz kat, gezdiğimizi setteki 1.elemandan büyükse (1.eleman gidilecek kat) bu block çalışır
                        direction = "aşağı"; //Asansör yönünü aşağı yapıyoruz

                    destination = binenler[i][1];//Hedefi, gezdiğimiz setin 1.elemanına set ediyoruz (1.eleman gidilecek kat)
                    Thread.Sleep(200); //200ms bekliyoruz, katlar arası mesafe
                    inenler.Add(binenler[i]); //Inenler listemize, gezdiğimiz seti ekliyoruz
                    toplamKisi -= binenler[i][0];//Toplam kişi sayısını gezdiğimiz setin 0.elemanı(0.eleman kişi sayısı) kadar azaltıyoruz 
                    floor = destination; //Hedefe ulaşınca şuanki katı hedef katına eşitliyoruz
                    count_inside = toplamKisi; //Asansor içindeki kişi sayısını tutan property'mizi toplam kisiye set ediyoruz                    
                }

                //Bu asansör yolcu bıraktığı yerden yolcu almaz

                MusteriIndir(); //Musteriyi indirmek için methodu çağırıyoruz

                if (Giris.girisKuyruk.Count == 0) //Eğer giriş katı kuyruğu bittiyse bu block çalışır
                {
                    //1,2,3,4. katlarda kuyrukta kişiler kaldıysa oradaki müşterileri alıyoruz
                    if (Cikis.birinciKat.Count != 0)
                        MusteriAl(Cikis.birinciKat);
                    else if (Cikis.ikinciKat.Count != 0)
                        MusteriAl(Cikis.ikinciKat);
                    else if (Cikis.ucuncuKat.Count != 0)
                        MusteriAl(Cikis.ucuncuKat);
                    else
                        MusteriAl(Cikis.dorduncuKat);
                }

            }
        }

        public void MusteriIndir() //Müşterileri indiren method
        {
            foreach (int[] inen in inenler) //Inenler listemizi foreach ile geziyoruz
            {
                if (binenler.Contains(inen)) //Eğer binenler listemizde, gezdiğimiz set var ise bu block çalışır
                    binenler.Remove(inen); //Binenker listemizde gezdiğimiz seti sileriz

                //Eğer kuyruklarda kişi kalmışsa, inen kişileri o kuyruklardanda siliyoruz
                if (Giris.girisKuyruk.Count > 0)
                    Giris.girisKuyruk.Remove(inen);

                if (Cikis.birinciKat.Count > 0)
                    Cikis.birinciKat.Remove(inen);

                if (Cikis.ikinciKat.Count > 0)
                    Cikis.ikinciKat.Remove(inen);

                if (Cikis.ucuncuKat.Count > 0)
                    Cikis.ucuncuKat.Remove(inen);

                if (Cikis.dorduncuKat.Count > 0)
                    Cikis.dorduncuKat.Remove(inen);
            }
        }
    }
}
