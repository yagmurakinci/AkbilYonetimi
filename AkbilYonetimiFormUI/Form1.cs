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
    public partial class FrmGiris : Form
    {
        public string Email { get; set; }
        public FrmGiris()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlBaglantiCumlesi = @"Server=DESKTOP-OFVK2FD\MSSQLSERVER01;Database=AKBİLYONETİMİDB;Trusted_Connection=True;";
                SqlConnection baglanti = new SqlConnection(sqlBaglantiCumlesi);
                string sorgu = $"Select * from Kullanicilar where Email='{txtEmail.Text}' and Parola='{GenelIslemler.MD5Encryption(txtSifre.Text)}'";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader(); //komutu çalıştırıp sorgu sonucunda gelen satırı okuyucuya al
                if (!okuyucu.HasRows)
                {
                    MessageBox.Show("Kullanıcı adınız ya da şifreniz yanlıştır! Lütfen tekrar deneyiniz!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    while (okuyucu.Read())
                    {
                        MessageBox.Show($"Hoşgeldiniz {okuyucu["Isim"].ToString()}");
                    }
                    this.Hide();
                    FrmIslemler frmIslemler = new FrmIslemler();
                    frmIslemler.Show();
                }
                baglanti.Close();
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik hata oluştu! HATA:" + hata.Message);
            }
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmKayitOl frmKayitOl = new FrmKayitOl();
            frmKayitOl.Show();
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {
            if (Email !=null)
            {
                txtEmail.Text = Email;
            }
        }
    }
}
