using System;
using System.Windows.Forms;

namespace AsansorManager
{
    public partial class frmMain : Form
    {        
        AnaClass anaClass; //AnaClass'ımızı tutacak değişkeni tanımlıyoruz
        public frmMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //Birden fazla thread kullanıdığımız için çakışmaları önleyecek satır
        }

        private void btnBaslat_Click(object sender, EventArgs e)//Başlat butonuna tıklanıldığında çalışacak event
        {            
            anaClass = new AnaClass();//AnaClass'ımızı new'liyoruz
            
            //AnaClass'ımızdaki baslat methodunu groupBox parametrelerini göndererek çağırıyoruz
            anaClass.baslat(grbKuyruklar,grbAsansor0,grbAsansor1,grbAsansor2,grbAsansor3,grbAsansor4);

            btnBaslat.Enabled = false;//Başlat tuşunu tekrar basılmasını engellemek için pasif yapıyoruz
        }
    }
}
