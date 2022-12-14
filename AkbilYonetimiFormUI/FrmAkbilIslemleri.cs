using AkbilYonetimiEntityLayer.Entities
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AkbilYonetimiFormUI
{
    public partial class FrmAkbilIslemleri : Form
    {
        public FrmAkbilIslemleri()
        {
            InitializeComponent();
        }

        private void btnAkbilKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                Akbil yeniAkbil = new Akbil
                {


                    AkbilNo = txtAkbilSeriNo.Text,
                    Bakiye = 0,
                    KayitTarihi = DateTime.Now,
                    AkbilTipi = 0 //Düzenlicez

                };

                yeniAkbil.SonKullanimTarihi = yeniAkbil.KayitTarihi.AddYears(5);


                string connectionString = @"Server=DESKTOP-OFVK2FD\MSSQLSERVER01;Database=AKBİLYONETİMİDB;Trusted_Connection=True;";
                SqlConnection baglantiNesnesi = new SqlConnection();
                baglantiNesnesi.ConnectionString = connectionString;
                SqlCommand komutNesnesi = new SqlCommand();
                komutNesnesi.Connection = baglantiNesnesi;
                komutNesnesi.CommandText = $"insert into Akbiller() values()";
                baglantiNesnesi.Open(); //bağlantıyı açar
                int sonuc = komutNesnesi.ExecuteNonQuery(); // ekleme,güncelleme,silme yapar // affected rows sayısı
                if (sonuc > 0)
                {
                    MessageBox.Show("Yeni kullanıcı eklendi");
                }
                baglantiNesnesi.Close();
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik hata oluştu. Mesaj:" + hata.Message);
            }
        }
    }
}
