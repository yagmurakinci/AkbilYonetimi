using AkbilYonetimBusinessLayer;
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
    public partial class FrmKayitOl : Form
    {
        public FrmKayitOl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Md5sifre = GenelIslemler.MD5Encryption(txtSifre.Text);
                //bağlantı cümlesine --> connection string
                //bağlantı nesnesine --> SQLConnection
                //komut nesnesine -----> SQLCommand
                string connectionString = @"Server=DESKTOP-OFVK2FD\MSSQLSERVER01;Database=AKBİLYONETİMİDB;Trusted_Connection=True;";
                SqlConnection baglantiNesnesi = new SqlConnection();
                baglantiNesnesi.ConnectionString = connectionString;
                SqlCommand komutNesnesi = new SqlCommand();
                komutNesnesi.Connection = baglantiNesnesi;
                komutNesnesi.CommandText = $"insert into Kullanicilar (KayitTarihi,Isim,Soyisim,Email,Parola,DogumTarihi) " +
                    $"values ('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{txtIsim.Text}','{txtSoyisim.Text}','{txtEmail.Text}','{Md5sifre}','{dtpDogumTarihi.Value.ToString("yyyy.MM.dd")}')";
                baglantiNesnesi.Open(); //bağlantıyı açar
                int sonuc = komutNesnesi.ExecuteNonQuery(); // ekleme,güncelleme,silme yapar // affected rows sayısı
                if (sonuc>0)
                {
                    MessageBox.Show("Yeni kullanıcı eklendi");
                }
                baglantiNesnesi.Close();
                ////temizlik
                foreach (var item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        ((TextBox)item).Enabled = false;
                    }
                    if (item is DateTimePicker)
                    {
                        ((DateTimePicker)item).Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Beklenmedik hata oluştu! \n Hata: {ex.Message}","HATA BİLDİRİMİ",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
        }

        private void FrmKayitOl_Load(object sender, EventArgs e)
        {
            txtSifre.PasswordChar = '*';
            dtpDogumTarihi.MaxDate = new DateTime(2015, 12, 31);
            dtpDogumTarihi.Value = new DateTime(2015, 12, 31);
            
            
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmGiris frmGiris = new FrmGiris();
            frmGiris.Email = txtEmail.Text;
            frmGiris.Show();
        }

        private void FrmKayitOl_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            FrmGiris frmGiris = new FrmGiris();
            frmGiris.Email = txtEmail.Text;
            frmGiris.Show();
        }
    }
}
