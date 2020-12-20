using AsansorManager.Asansorler;
using AsansorManager.Utilities;
using System.Threading;
using System.Windows.Forms;

namespace AsansorManager.Threadler
{
    public class Kontrol
    {
        public Thread asansor1, asansor2, asansor3, asansor4; // 4 asansörümüzün thread'lerini tanımlıyoruz
        GroupBox asansor1Group,asansor2Group, asansor3Group, asansor4Group; // 4 asansörümüzün form üzerindeki groupBox'larını tutacak değişkenleri tanımlıyoruz
        static readonly object _lockObject = new object(); // Listelerimizin thread safe olması bir lock objesi oluşturuyoruz (bir thread listemizle işlem yaparken, başka bir thread işlem yapamasın diye o listesi kitlemek için kullanırız)        

        //Parametreli constructor
        //Asansörlerin groupBox'larını parametre ile alıyoruz
        public Kontrol(GroupBox asansor1Box, GroupBox asansor2Box, GroupBox asansor3Box, GroupBox asansor4Box)
        {
            //En yukarıda tanımladığımız değişkenleri, verilen parametrelerden gelen değerlere set ediyoruz
            asansor1Group = asansor1Box;
            asansor2Group = asansor2Box;
            asansor3Group = asansor3Box;
            asansor4Group = asansor4Box;

            //En yukarıda tanımladığımız thread'leri new'liyoruz ve start işlemi verildiğinde çalışmasını istediğimiz metodları veriyoruz
            asansor1 = new Thread(new ThreadStart(asansor_1));
            asansor2 = new Thread(new ThreadStart(asansor_2));
            asansor3 = new Thread(new ThreadStart(asansor_3));
            asansor4 = new Thread(new ThreadStart(asansor_4));

            //Asansör threadlerimizide uygulama kapandığında, durdurmak için background özelliklerini set ediyoruz
            asansor1.IsBackground = true;
            asansor2.IsBackground = true;
            asansor3.IsBackground = true;
            asansor4.IsBackground = true;            
        }

        public void kuyruguKontrolEt() //Kuyruk sayısını kontrol edip diğer asansörleri devreye sokan method
        {
            Thread.Sleep(1000);//Bir saniyelik bekleme ayarlıyoruz. (Bu method bir döngü içinde kullanıldığında her 1 saniyede tekrar çalışır)

            if (Yazdirici.toplamKisi >= 20 && !asansor1.IsAlive)//Eğer kuyruktaki toplam bekleyen sayısı 20 ve üzeriyse ve asansor1 threadi kapalıysa (start edilmediyse) bu block çalışır
                asansor1.Start();//Asansör1 thread'ini devreye sokuyoruz
            else if (Yazdirici.toplamKisi < 20 && asansor1.IsAlive)//Eğer kuyruktaki toplam kişi sayısı 20'den düşükse ve asansor1 threadi açıksa (start edildiyse) bu block çalışır
            {
                asansor1.Abort();//Asansör1 thread'ini iptal ediyoruz (durduruyoruz)

                //Asansör1'in groupbox'ındaki değişiklikleri ekrana yazdırıyoruz
                asansor1Group.Controls.Find("lblMode1", true)[0].Text = "Çalışmıyor";
                asansor1Group.Controls.Find("lblDetay1", true)[0].Text = "-";
                asansor1Group.Controls.Find("lblTasinan1", true)[0].Text = "0";                
            }

            if (Yazdirici.toplamKisi >= 40 && !asansor2.IsAlive)//Eğer kuyruktaki toplam bekleyen sayısı 40 ve üzeriyse ve asansor2 threadi kapalıysa (start edilmediyse) bu block çalışır
                asansor2.Start();//Asansör1 thread'ini devreye sokuyoruz
            else if (Yazdirici.toplamKisi < 40 && asansor2.IsAlive)//Eğer kuyruktaki toplam kişi sayısı 40'den düşükse ve asansor2 threadi açıksa (start edildiyse) bu block çalışır
            {
                asansor2.Abort();//Asansör2 thread'ini iptal ediyoruz (durduruyoruz)

                //Asansör2'in groupbox'ındaki değişiklikleri ekrana yazdırıyoruz
                asansor2Group.Controls.Find("lblMode2", true)[0].Text = "Çalışmıyor";
                asansor2Group.Controls.Find("lblDetay2", true)[0].Text = "-";
                asansor2Group.Controls.Find("lblTasinan2", true)[0].Text = "0";
            }
                

            if (Yazdirici.toplamKisi >= 80 && !asansor3.IsAlive)//Eğer kuyruktaki toplam bekleyen sayısı 80 ve üzeriyse ve asansor3 threadi kapalıysa (start edilmediyse) bu block çalışır
                asansor3.Start();//Asansör3 thread'ini devreye sokuyoruz
            else if (Yazdirici.toplamKisi < 80 && asansor3.IsAlive)//Eğer kuyruktaki toplam kişi sayısı 80'den düşükse ve asansor3 threadi açıksa (start edildiyse) bu block çalışır
            {
                asansor3.Abort();//Asansör3 thread'ini iptal ediyoruz (durduruyoruz)

                //Asansör3'in groupbox'ındaki değişiklikleri ekrana yazdırıyoruz
                asansor3Group.Controls.Find("lblMode3", true)[0].Text = "Çalışmıyor";
                asansor3Group.Controls.Find("lblDetay3", true)[0].Text = "-";
                asansor3Group.Controls.Find("lblTasinan3", true)[0].Text = "0";
            }                

            if (Yazdirici.toplamKisi >= 160 && !asansor4.IsAlive)//Eğer kuyruktaki toplam bekleyen sayısı 160 ve üzeriyse ve asansor4 threadi kapalıysa (start edilmediyse) bu block çalışır
                asansor4.Start();//Asansör4 thread'ini devreye sokuyoruz
            else if (Yazdirici.toplamKisi < 160 && asansor4.IsAlive)//Eğer kuyruktaki toplam kişi sayısı 160'den düşükse ve asansor4 threadi açıksa (start edildiyse) bu block çalışır
            {
                asansor4.Abort();//Asansör4 thread'ini iptal ediyoruz (durduruyoruz)

                //Asansör4'in groupbox'ındaki değişiklikleri ekrana yazdırıyoruz
                asansor4Group.Controls.Find("lblMode4", true)[0].Text = "Çalışmıyor";
                asansor4Group.Controls.Find("lblDetay4", true)[0].Text = "-";
                asansor4Group.Controls.Find("lblTasinan4", true)[0].Text = "0";
            }                
        }

        public void asansor_1() //Asansör1 thread'i start edildiğinde çalışacak method
        {
            Asansor1 asansor1 = new Asansor1("Asansör - 1");//Asansor1 class'ından yeni bir asansör oluşturuyoruz (Parametre ile ismini veriyoruz)
            while (true) //Sonsuz bir döngü açıyoruz
            {
                lock (_lockObject) //Oluşturuğumuz lock objesiyle bu block içindeki kodları block bitene kadar kilitliyoruz
                {
                    //Herhangibir kuyruktaki set sayısı (kişiler ve katsayıları) giriş kuyruğundan fazlaysa oradan müşteri alır.
                    if (Giris.girisKuyruk.Count < Cikis.birinciKat.Count)
                        asansor1.MusteriAl(Cikis.birinciKat);
                    else if (Giris.girisKuyruk.Count < Cikis.ikinciKat.Count)
                        asansor1.MusteriAl(Cikis.ikinciKat);
                    else if (Giris.girisKuyruk.Count < Cikis.ucuncuKat.Count)
                        asansor1.MusteriAl(Cikis.ucuncuKat);
                    else if (Giris.girisKuyruk.Count < Cikis.dorduncuKat.Count)
                        asansor1.MusteriAl(Cikis.dorduncuKat);
                    else //Eğer hiçbiri fazla değilse giriş kuyruğundan müşteri alır
                        asansor1.MusteriAl(Giris.girisKuyruk);

                    Thread.Sleep(1000); //1 saniyeliğine bekliyoruz (Opsiyonel)
                    asansor1Yazdir(asansor1); //Asansör1'in değerlerini ekrana yazdırıyoruz (sonsuz döngü olduğu için her çalıştığında güncel değerleri yazar)
                    asansor1.HedefeGotur();//Asansör1'in müşterilerini istenilen katlara götürür
                }
                
                //Eğer tüm kuyruklardaki müşteriler bittiyse (herkes çıkış yaptıysa) bu block çalışır
                if (Giris.girisKuyruk.Count == 0 && Cikis.birinciKat.Count == 0 && Cikis.ikinciKat.Count == 0 && Cikis.ucuncuKat.Count == 0 && Cikis.dorduncuKat.Count == 0)
                {
                    asansor1.floor = 0; //Asansörün kat değerini 0 a çekiyoruz
                    asansor1.mode = false; //Asansörün modunu false yapıyoruz (yani çalışmıyor)
                    break;//Sonsuz döngüyü bitiriyoruz
                }                
            }
            asansor1Yazdir(asansor1);//Asansörün son değerlerini yazdırıyoruz
        }

        public void asansor_2() //Asansör2 thread'i start edildiğinde çalışacak method
        {
            Asansor2 asansor2 = new Asansor2("Asansör - 2");//Asansor2 class'ından yeni bir asansör oluşturuyoruz (Parametre ile ismini veriyoruz)
            while (true)//Sonsuz bir döngü açıyoruz
            {
                lock (_lockObject)//Oluşturuğumuz lock objesiyle bu block içindeki kodları block bitene kadar kilitliyoruz
                {
                    //Herhangibir kuyruktaki set sayısı (kişiler ve katsayıları) giriş kuyruğundan fazlaysa oradan müşteri alır.
                    if (Giris.girisKuyruk.Count < Cikis.birinciKat.Count)
                        asansor2.MusteriAl(Cikis.birinciKat);
                    else if (Giris.girisKuyruk.Count < Cikis.ikinciKat.Count)
                        asansor2.MusteriAl(Cikis.ikinciKat);
                    else if (Giris.girisKuyruk.Count < Cikis.ucuncuKat.Count)
                        asansor2.MusteriAl(Cikis.ucuncuKat);
                    else if (Giris.girisKuyruk.Count < Cikis.dorduncuKat.Count)
                        asansor2.MusteriAl(Cikis.dorduncuKat);
                    else //Eğer hiçbiri fazla değilse giriş kuyruğundan müşteri alır
                        asansor2.MusteriAl(Giris.girisKuyruk);

                    Thread.Sleep(1000);//1 saniyeliğine bekliyoruz (Opsiyonel)
                    asansor2Yazdir(asansor2);//Asansör2'in değerlerini ekrana yazdırıyoruz (sonsuz döngü olduğu için her çalıştığında güncel değerleri yazar)
                    asansor2.HedefeGotur();//Asansör2'in müşterilerini istenilen katlara götürür
                }

                //Eğer tüm kuyruklardaki müşteriler bittiyse (herkes çıkış yaptıysa) bu block çalışır
                if (Giris.girisKuyruk.Count == 0 && Cikis.birinciKat.Count == 0 && Cikis.ikinciKat.Count == 0 && Cikis.ucuncuKat.Count == 0 && Cikis.dorduncuKat.Count == 0)
                {
                    asansor2.floor = 0;//Asansörün kat değerini 0 a çekiyoruz
                    asansor2.mode = false;//Asansörün modunu false yapıyoruz (yani çalışmıyor)
                    break;//Sonsuz döngüyü bitiriyoruz
                }
            }
            asansor2Yazdir(asansor2);//Asansörün son değerlerini yazdırıyoruz
        }
        public void asansor_3()//Asansör3 thread'i start edildiğinde çalışacak method
        {
            Asansor3 asansor3 = new Asansor3("Asansör - 3");//Asansor3 class'ından yeni bir asansör oluşturuyoruz (Parametre ile ismini veriyoruz)
            while (true)//Sonsuz bir döngü açıyoruz
            {
                lock (_lockObject)//Oluşturuğumuz lock objesiyle bu block içindeki kodları block bitene kadar kilitliyoruz
                {
                    //Herhangibir kuyruktaki set sayısı (kişiler ve katsayıları) giriş kuyruğundan fazlaysa oradan müşteri alır.
                    if (Giris.girisKuyruk.Count < Cikis.birinciKat.Count)
                        asansor3.MusteriAl(Cikis.birinciKat);
                    else if (Giris.girisKuyruk.Count < Cikis.ikinciKat.Count)
                        asansor3.MusteriAl(Cikis.ikinciKat);
                    else if (Giris.girisKuyruk.Count < Cikis.ucuncuKat.Count)
                        asansor3.MusteriAl(Cikis.ucuncuKat);
                    else if (Giris.girisKuyruk.Count < Cikis.dorduncuKat.Count)
                        asansor3.MusteriAl(Cikis.dorduncuKat);
                    else//Eğer hiçbiri fazla değilse giriş kuyruğundan müşteri alır
                        asansor3.MusteriAl(Giris.girisKuyruk);

                    Thread.Sleep(1000);//1 saniyeliğine bekliyoruz (Opsiyonel)
                    asansor3Yazdir(asansor3);//Asansör3'ün değerlerini ekrana yazdırıyoruz (sonsuz döngü olduğu için her çalıştığında güncel değerleri yazar)
                    asansor3.HedefeGotur();//Asansör3'ün müşterilerini istenilen katlara götürür
                }

                //Eğer tüm kuyruklardaki müşteriler bittiyse (herkes çıkış yaptıysa) bu block çalışır
                if (Giris.girisKuyruk.Count == 0 && Cikis.birinciKat.Count == 0 && Cikis.ikinciKat.Count == 0 && Cikis.ucuncuKat.Count == 0 && Cikis.dorduncuKat.Count == 0)
                {
                    asansor3.floor = 0;//Asansörün kat değerini 0 a çekiyoruz
                    asansor3.mode = false;//Asansörün modunu false yapıyoruz (yani çalışmıyor)
                    break;//Sonsuz döngüyü bitiriyoruz
                }
            }
            asansor3Yazdir(asansor3);//Asansörün son değerlerini yazdırıyoruz
        }
        public void asansor_4()//Asansör4 thread'i start edildiğinde çalışacak method
        {
            Asansor4 asansor4 = new Asansor4("Asansör - 4");//Asansor4 class'ından yeni bir asansör oluşturuyoruz (Parametre ile ismini veriyoruz)
            while (true)//Sonsuz bir döngü açıyoruz
            {
                lock (_lockObject)//Oluşturuğumuz lock objesiyle bu block içindeki kodları block bitene kadar kilitliyoruz
                {
                    //Herhangibir kuyruktaki set sayısı (kişiler ve katsayıları) giriş kuyruğundan fazlaysa oradan müşteri alır.
                    if (Giris.girisKuyruk.Count < Cikis.birinciKat.Count)
                        asansor4.MusteriAl(Cikis.birinciKat);
                    else if (Giris.girisKuyruk.Count < Cikis.ikinciKat.Count)
                        asansor4.MusteriAl(Cikis.ikinciKat);
                    else if (Giris.girisKuyruk.Count < Cikis.ucuncuKat.Count)
                        asansor4.MusteriAl(Cikis.ucuncuKat);
                    else if (Giris.girisKuyruk.Count < Cikis.dorduncuKat.Count)
                        asansor4.MusteriAl(Cikis.dorduncuKat);
                    else//Eğer hiçbiri fazla değilse giriş kuyruğundan müşteri alır
                        asansor4.MusteriAl(Giris.girisKuyruk);

                    Thread.Sleep(1000);//1 saniyeliğine bekliyoruz (Opsiyonel)
                    asansor4Yazdir(asansor4);//Asansör4'ün değerlerini ekrana yazdırıyoruz (sonsuz döngü olduğu için her çalıştığında güncel değerleri yazar)
                    asansor4.HedefeGotur();//Asansör4'ün müşterilerini istenilen katlara götürür
                }
                //Eğer tüm kuyruklardaki müşteriler bittiyse (herkes çıkış yaptıysa) bu block çalışır
                if (Giris.girisKuyruk.Count == 0 && Cikis.birinciKat.Count == 0 && Cikis.ikinciKat.Count == 0 && Cikis.ucuncuKat.Count == 0 && Cikis.dorduncuKat.Count == 0)
                {
                    asansor4.floor = 0;//Asansörün kat değerini 0 a çekiyoruz
                    asansor4.mode = false;//Asansörün modunu false yapıyoruz (yani çalışmıyor)
                    break;//Sonsuz döngüyü bitiriyoruz
                }
            }
            asansor4Yazdir(asansor4);//Asansörün son değerlerini yazdırıyoruz
        }

        public void asansor1Yazdir(Asansor asansor)//Asansör1'in değerlerini ekrana yazan method
        {
            string detay = "";
            foreach (var item in asansor.inside) //Asansör içindeki kişiler ve katları köşeli parantez içine alıp detay değişkenine aktarıyoruz (örnek => [2,4],[1,0])
            {
                detay += "[" + string.Join(",", item) + "]";
            }
            //AsansorGroupBox'ın içindeki labelları bulup asansör değerlerini yazdırıyoruz
            asansor1Group.Controls.Find("lblMode1", true)[0].Text = asansor.mode ? "Çalışıyor" : "Çalışmıyor"; //Eğer asansör modu true ise çalışıyor, false ise çalışmıyor yazar
            asansor1Group.Controls.Find("lblKat1", true)[0].Text = asansor.floor.ToString();
            asansor1Group.Controls.Find("lblHedef1", true)[0].Text = asansor.destination.ToString();
            asansor1Group.Controls.Find("lblYon1", true)[0].Text = asansor.direction;
            asansor1Group.Controls.Find("lblKapasite1", true)[0].Text = asansor.capacity.ToString();
            asansor1Group.Controls.Find("lblTasinan1", true)[0].Text = asansor.count_inside.ToString();
            asansor1Group.Controls.Find("lblDetay1", true)[0].Text = detay;
        }

        public void asansor2Yazdir(Asansor asansor)//Asansör2'in değerlerini ekrana yazan method
        {
            string detay = "";
            foreach (var item in asansor.inside)//Asansör içindeki kişiler ve katları köşeli parantez içine alıp detay değişkenine aktarıyoruz (örnek => [2,4],[1,0])
            {
                detay += "[" + string.Join(",", item) + "]";
            }
            //AsansorGroupBox'ın içindeki labelları bulup asansör değerlerini yazdırıyoruz
            asansor2Group.Controls.Find("lblMode2", true)[0].Text = asansor.mode ? "Çalışıyor" : "Çalışmıyor";//Eğer asansör modu true ise çalışıyor, false ise çalışmıyor yazar
            asansor2Group.Controls.Find("lblKat2", true)[0].Text = asansor.floor.ToString();
            asansor2Group.Controls.Find("lblHedef2", true)[0].Text = asansor.destination.ToString();
            asansor2Group.Controls.Find("lblYon2", true)[0].Text = asansor.direction;
            asansor2Group.Controls.Find("lblKapasite2", true)[0].Text = asansor.capacity.ToString();
            asansor2Group.Controls.Find("lblTasinan2", true)[0].Text = asansor.count_inside.ToString();
            asansor2Group.Controls.Find("lblDetay2", true)[0].Text = detay;
        }
        public void asansor3Yazdir(Asansor asansor)//Asansör3'in değerlerini ekrana yazan method
        {
            string detay = "";
            foreach (var item in asansor.inside)//Asansör içindeki kişiler ve katları köşeli parantez içine alıp detay değişkenine aktarıyoruz (örnek => [2,4],[1,0])
            {
                detay += "[" + string.Join(",", item) + "]";
            }
            //AsansorGroupBox'ın içindeki labelları bulup asansör değerlerini yazdırıyoruz
            asansor3Group.Controls.Find("lblMode3", true)[0].Text = asansor.mode ? "Çalışıyor" : "Çalışmıyor";//Eğer asansör modu true ise çalışıyor, false ise çalışmıyor yazar
            asansor3Group.Controls.Find("lblKat3", true)[0].Text = asansor.floor.ToString();
            asansor3Group.Controls.Find("lblHedef3", true)[0].Text = asansor.destination.ToString();
            asansor3Group.Controls.Find("lblYon3", true)[0].Text = asansor.direction;
            asansor3Group.Controls.Find("lblKapasite3", true)[0].Text = asansor.capacity.ToString();
            asansor3Group.Controls.Find("lblTasinan3", true)[0].Text = asansor.count_inside.ToString();
            asansor3Group.Controls.Find("lblDetay3", true)[0].Text = detay;
        }
        public void asansor4Yazdir(Asansor asansor)//Asansör4'in değerlerini ekrana yazan method
        {
            string detay = "";
            foreach (var item in asansor.inside)//Asansör içindeki kişiler ve katları köşeli parantez içine alıp detay değişkenine aktarıyoruz (örnek => [2,4],[1,0])
            {
                detay += "[" + string.Join(",", item) + "]";
            }
            //AsansorGroupBox'ın içindeki labelları bulup asansör değerlerini yazdırıyoruz
            asansor4Group.Controls.Find("lblMode4", true)[0].Text = asansor.mode ? "Çalışıyor" : "Çalışmıyor";//Eğer asansör modu true ise çalışıyor, false ise çalışmıyor yazar
            asansor4Group.Controls.Find("lblKat4", true)[0].Text = asansor.floor.ToString();
            asansor4Group.Controls.Find("lblHedef4", true)[0].Text = asansor.destination.ToString();
            asansor4Group.Controls.Find("lblYon4", true)[0].Text = asansor.direction;
            asansor4Group.Controls.Find("lblKapasite4", true)[0].Text = asansor.capacity.ToString();
            asansor4Group.Controls.Find("lblTasinan4", true)[0].Text = asansor.count_inside.ToString();
            asansor4Group.Controls.Find("lblDetay4", true)[0].Text = detay;
        }
    }
}
