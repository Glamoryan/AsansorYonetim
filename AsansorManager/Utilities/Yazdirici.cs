using AsansorManager.Threadler;

namespace AsansorManager.Utilities
{
    public static class Yazdirici
    {
        public static int toplamKisi; //Toplam kişi sayısını tutan değişkeni public olarak tanımlıyoruz çünkü başka classlarda erişeceğiz
        public static int[] toplamKuyruk() //Kuyruklarda bekleyenlerin sayısı ve tüm kuyruklarda bekleyenlerin toplamlarını veren method
        {
            toplamKisi = 0; //Toplam kişi sayısı başta sıfırlıyoruz
            int girisToplamKisi = 0, birinciToplamKisi = 0, ikinciToplamKisi = 0, ucuncuToplamKisi = 0, dorduncuToplamKisi = 0;//Diğer kuyraklardaki kişi sayılarını tutan değişkenleri tanımlıyoruz ve sıfırlıyoruz

            //ArrayList tipindeki tüm kuyrukları ikili int array'ine çevirip değişkenlere atıyoruz
            int[][] girisKuyrugu = (int[][])Giris.girisKuyruk.ToArray(typeof(int[]));
            int[][] birinciKatKuyrugu = (int[][])Cikis.birinciKat.ToArray(typeof(int[]));
            int[][] ikinciKatKuyrugu = (int[][])Cikis.ikinciKat.ToArray(typeof(int[]));
            int[][] ucuncuKatKuyrugu = (int[][])Cikis.ucuncuKat.ToArray(typeof(int[]));
            int[][] dorduncuKatKuyrugu = (int[][])Cikis.dorduncuKat.ToArray(typeof(int[]));

            //Çevirdiğimiz tüm kuyrukların içinde geziyoruz
            for (int i = 0; i < girisKuyrugu.Length; i++)
            {
                girisToplamKisi += girisKuyrugu[i][0];//Gezdiğimiz setin 0.elemanını (0.eleman kişi sayısı,1.eleman gidilecek kat) değişkene ekliyoruz
            }
            for (int i = 0; i < birinciKatKuyrugu.Length; i++)
            {
                birinciToplamKisi += birinciKatKuyrugu[i][0];//Gezdiğimiz setin 0.elemanını (0.eleman kişi sayısı,1.eleman gidilecek kat) değişkene ekliyoruz
            }
            for (int i = 0; i < ikinciKatKuyrugu.Length; i++)
            {
                ikinciToplamKisi += ikinciKatKuyrugu[i][0];//Gezdiğimiz setin 0.elemanını (0.eleman kişi sayısı,1.eleman gidilecek kat) değişkene ekliyoruz
            }
            for (int i = 0; i < ucuncuKatKuyrugu.Length; i++)
            {
                ucuncuToplamKisi += ucuncuKatKuyrugu[i][0];//Gezdiğimiz setin 0.elemanını (0.eleman kişi sayısı,1.eleman gidilecek kat) değişkene ekliyoruz
            }
            for (int i = 0; i < dorduncuKatKuyrugu.Length; i++)
            {
                dorduncuToplamKisi += dorduncuKatKuyrugu[i][0];//Gezdiğimiz setin 0.elemanını (0.eleman kişi sayısı,1.eleman gidilecek kat) değişkene ekliyoruz
            }
            //Tüm kuyrukları toplayıp toplam kişi değişkenine atıyoruz
            toplamKisi = girisToplamKisi + birinciToplamKisi + ikinciToplamKisi + ucuncuToplamKisi + dorduncuToplamKisi;

            //toplam kisi ve diğer tüm kuyrukların bekleyen sayılarını int array ile geri döndürüyoruz
            return new int[]{ toplamKisi,girisToplamKisi, birinciToplamKisi, ikinciToplamKisi, ucuncuToplamKisi, dorduncuToplamKisi };
        }                
    }
}
