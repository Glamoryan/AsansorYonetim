using AsansorManager.Asansorler;
using AsansorManager.Threadler;
using AsansorManager.Utilities;
using System.Threading;
using System.Windows.Forms;

namespace AsansorManager
{
    public class AnaClass
    {
        Thread giris, cikis, asansor0; // Giriş, Çıkış ve Asansor0 threadlerimizi tanımlıyoruz
        GroupBox kuyrukGrup,asansor0Grup, asansor1Grup, asansor2Grup, asansor3Grup, asansor4Grup; //Form ekranındaki groupBox'larımızı tutacak değişkenlerimizi tanımlıyoruz
        Kontrol kontrol; //Kontrol class'ımızı tutacak değişkeni tanımlıyoruz
        static readonly object _lockObject = new object();// Listelerimizin thread safe olması bir lock objesi oluşturuyoruz (bir thread listemizle işlem yaparken, başka bir thread işlem yapamasın diye o listesi kitlemek için kullanırız)        

        //Ana thread'lerimizi başlatan method (giris,cikis,asansor0)
        //Parametre olarak Form class'ından gelen groupbox'ları alır
        public void baslat(GroupBox kuyrukBox,GroupBox asansor0Box,GroupBox asansor1Box, GroupBox asansor2Box, GroupBox asansor3Box, GroupBox asansor4Box)
        {
            //Parametre olarak gelen groupBox'ları tanımladığımız değişkenlere atıyoruz
            kuyrukGrup = kuyrukBox;
            asansor0Grup = asansor0Box;
            asansor1Grup = asansor1Box;
            asansor2Grup = asansor2Box;
            asansor3Grup = asansor3Box;
            asansor4Grup = asansor4Box;

            // Threadlerimizin start olaylarını methodlarına bağlıyoruz. Threadlerimizi start olduğunda vermiş olduğumuz methodlar çalışacak
            giris = new Thread(new ThreadStart(giris_t));
            cikis = new Thread(new ThreadStart(cikis_t));
            asansor0 = new Thread(new ThreadStart(asansor0_t));

            // Threadlerimizin, uygulama kapandıktan sonrada kapanması için background özelliğini set ediyoruz
            giris.IsBackground = true;
            cikis.IsBackground = true;
            asansor0.IsBackground = true;

            giris.Start(); //Giriş thread'ini başlatıyoruz (Avm girişi için rastgele kişi ve gidecekleri katları üretir)
            cikis.Start(); //Çıkış thread'ini başlatıyoruz (Avm çıkışı için rastgele katlardan kişiler üretir)
            Thread.Sleep(1000); // Kişiler oluşmasını 1 saniye bekliyoruz (opsiyonel)
            asansor0.Start(); // Asansor0 thread'ini başlatıyoruz
            
            //Kontrol class'ımızı new'leyip parametre olarak asansör groupBox'larımızı veriyoruz
            kontrol = new Kontrol(asansor1Grup,asansor2Grup,asansor3Grup,asansor4Grup);
        }

        public void giris_t()//Giriş threadimize bağladığımız start methodu
        {
            Giris girisThread = new Giris(); // Giris thread'mizin instance'ını alıyoruz
            girisThread.KuyrugaEkle(); //Giris thread'imizdeki kuyruga ekle methodunu çağırıyoruz
        }
        public void cikis_t()//Çıkış threadimize bağladığımız start methodu
        {
            Cikis cikisThread = new Cikis(); // Cikis thread'mizin instance'ını alıyoruz
            cikisThread.KuyrugaEkle(); //Cikis thread'imizdeki kuyruga ekle methodunu çağırıyoruz
        }
        public void asansor0_t()//Asansor0 threadimize bağladığımız start methodu
        {
            Asansor0 asansor = new Asansor0("Asansör - 0"); //Asansor0'ı new'liyoruz (Parametre olarak asansor adını gönderiyoruz)                        
            while (true) //Sonsuz döngü açıyoruz
            {
                lock (_lockObject)//Oluşturuğumuz lock objesiyle bu block içindeki kodları block bitene kadar kilitliyoruz
                {
                    asansor.MusteriAl(Giris.girisKuyruk);//Oluşturduğumuz asansörün musteri al methodunu çağırıyoruz (Parametre olarak giriş kuyruğunu gönderiyoruz)
                    Thread.Sleep(1000); // 1 saniye bekliyoruz (opsiyonel)
                    kuyrukYazdir(); //Kuyruk bilgilerimizi ekrana yazdıran methodumuzu çağırıyoruz
                    asansor0Yazdir(asansor); //Asansör0 bilgilerini ekrana yazdırab methodumuzu çağırıyoruz
                    asansor.HedefeGotur(); //Oluşturduğumuz asansörün hedefe götür methodunu çağırıyoruz
                }                  
                //Eğer hiçbir katta kuyruk yoksa bu block çalışır
                if (Giris.girisKuyruk.Count == 0 && Cikis.birinciKat.Count == 0 && Cikis.ikinciKat.Count == 0 && Cikis.ucuncuKat.Count == 0 && Cikis.dorduncuKat.Count == 0)
                {
                    asansor.floor = 0; //Asansör katını 0 a çekiyoruz
                    asansor.mode = false; //Asansör modunu false yapıyorz (yani çalışmayacak)
                    break;//Sonsuz döngüyü bitiriyoruz
                }
                kontrol.kuyruguKontrolEt(); //Kontrol class'ındaki kontrol et metodunu çağırıyoruz
            }
            kuyrukYazdir();//Kuyruk bilgilerinin son halini ekrana yazdırıyoruz
            asansor0Yazdir(asansor); //Asansör0 değerlerinin son halini ekrana yazdırıyoruz

            //Form'daki başlat tuşunu arayıp bulduktan sonra tekrar aktif hale getiriyoruz
            asansor0Grup.Parent.Controls.Find("btnBaslat", true)[0].Enabled = true;
        }       
        
        public void kuyrukYazdir()//Kuyruk bilgilerini ekrana yazan method
        {            
            int[] degerler = Yazdirici.toplamKuyruk(); //ToplamKuyruk methodundan dönen değerleri değişkene atıyoruz

            //Kuyruk değerlerini formdaki uygun yerlere yazdırıyoruz
            kuyrukGrup.Controls.Find("lblToplam", true)[0].Text = degerler[0].ToString();
            kuyrukGrup.Controls.Find("lblZemin", true)[0].Text = degerler[1].ToString();
            kuyrukGrup.Controls.Find("lblBirinci", true)[0].Text = degerler[2].ToString();
            kuyrukGrup.Controls.Find("lblIkinci", true)[0].Text = degerler[3].ToString();
            kuyrukGrup.Controls.Find("lblUcuncu", true)[0].Text = degerler[4].ToString();
            kuyrukGrup.Controls.Find("lblDorduncu", true)[0].Text = degerler[5].ToString();
        }

        public void asansor0Yazdir(Asansor0 asansor) //Asansör0 bilgilerini ekrana yazan method
        {         
            string detay = "";
            foreach (var item in asansor.inside)//Asansör içindeki kişiler ve katları köşeli parantez içine alıp detay değişkenine aktarıyoruz (örnek => [2,4],[1,0])
            {
                detay += "[" + string.Join(",", item) + "]";
            }
            //AsansorGroupBox'ın içindeki labelları bulup asansör değerlerini yazdırıyoruz
            asansor0Grup.Controls.Find("lblMod", true)[0].Text = asansor.mode ? "Çalışıyor":"Çalışmıyor";//Eğer asansör modu true ise çalışıyor, false ise çalışmıyor yazar
            asansor0Grup.Controls.Find("lblKat", true)[0].Text = asansor.floor.ToString();
            asansor0Grup.Controls.Find("lblHedef", true)[0].Text = asansor.destination.ToString();
            asansor0Grup.Controls.Find("lblYon", true)[0].Text = asansor.direction;
            asansor0Grup.Controls.Find("lblKapasite", true)[0].Text = asansor.capacity.ToString();
            asansor0Grup.Controls.Find("lblTasinan", true)[0].Text = asansor.count_inside.ToString();
            asansor0Grup.Controls.Find("lblDetay", true)[0].Text = detay;            
        }
    }
}
