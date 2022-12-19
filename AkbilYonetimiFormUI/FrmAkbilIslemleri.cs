using AkbilYonetimBusinessLayer;
using AkbilYonetimiEntityLayer.Entities;
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
                if (txtAkbilSeriNo.Text==null || txtAkbilSeriNo.Text==string.Empty)
                {
                    MessageBox.Show("HATA: Akbil Seri numarası boş geçilemez!");
                    return;
                }
                if (txtAkbilSeriNo.Text.Length!=16)
                {
                    MessageBox.Show("HATA: Akbil Seri numarası 16 haneli olmalıdır!");
                    return;
                }
                foreach (char item in txtAkbilSeriNo.Text)
                {
                    if (!char.IsDigit(item))
                    {
                        throw new Exception("Akbil numarası sadece rakamlardan oluşmalıdır!");
                    }
                }
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
                komutNesnesi.CommandText = $"insert into Akbiller(AkbilNo,KayitTarihi,AkbilTipi,Bakiye,SonKullanimTarihi,AkbilSahibiID) values(@akbilNo,@kayitTarihi,@akbiltipi,@bakiye,@sonkullan,@akbilSahibi)";
                komutNesnesi.Parameters.AddWithValue("@akbilNo",yeniAkbil.AkbilNo);
                komutNesnesi.Parameters.AddWithValue("@kayitTarihi",yeniAkbil.KayitTarihi);
                komutNesnesi.Parameters.AddWithValue("@akbilTipi",yeniAkbil.AkbilTipi);
                komutNesnesi.Parameters.AddWithValue("@bakiye",yeniAkbil.Bakiye);
                komutNesnesi.Parameters.AddWithValue("@sonkullan",yeniAkbil.SonKullanimTarihi);
                komutNesnesi.Parameters.AddWithValue("@akbilsahibi",GenelIslemler.GirisYapmisKullaniciID);
                baglantiNesnesi.Open(); //bağlantıyı açar
                int sonuc = komutNesnesi.ExecuteNonQuery(); // ekleme,güncelleme,silme yapar // affected rows sayısı
                if (sonuc > 0)
                {
                    MessageBox.Show("Yeni kullanıcı eklendi");
                }
                baglantiNesnesi.Close();
                DataGridViewiDoldur();
                txtAkbilSeriNo.Clear();
                cmbBoxAkbilTipleri.SelectedIndex = -1;
                cmbBoxAkbilTipleri.Text = "Akbil Tipi Seçiniz...";
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik hata oluştu. Mesaj:" + hata.Message);
            }
        }

        private void cmbBoxAkbilTipleri_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBoxAkbilProps_Enter(object sender, EventArgs e)
        {

        }

        private void FrmAkbilIslemleri_Load(object sender, EventArgs e)
        {
            AkbilTipiComboyuDoldur();
            DataGridViewiDoldur();
            dataGridViewAkbiller.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void AkbilTipiComboyuDoldur()
        {
            //ComboBox'ın dolması
            cmbBoxAkbilTipleri.DataSource = Enums.AkbilTipleriniGetir();
            //cmbBoxAkbilTipleri.DisplayMember = "";
            //cmbBoxAkbilTipleri.ValueMember = "";
            cmbBoxAkbilTipleri.Text = "Akbil Tipi Seçiniz";
        }
        private void DataGridViewiDoldur()
        {
            try
            {
 
                string connectionString = @"Server=DESKTOP-OFVK2FD\MSSQLSERVER01;Database=AKBİLYONETİMİDB;Trusted_Connection=True;";
                SqlConnection baglantiNesnesi = new SqlConnection();
                baglantiNesnesi.ConnectionString = connectionString;
                SqlCommand komutNesnesi = new SqlCommand();
                komutNesnesi.Connection = baglantiNesnesi;
                komutNesnesi.CommandText = $"select * from Akbiller where AkbilSahibiID=@akbilsahibi";
                komutNesnesi.Parameters.AddWithValue("@akbilsahibi", GenelIslemler.GirisYapmisKullaniciID);

                SqlDataAdapter adaptor = new SqlDataAdapter(); //adaptor
                adaptor.SelectCommand = komutNesnesi;
                DataTable dt = new DataTable();
                baglantiNesnesi.Open();
                adaptor.Fill(dt);
                dataGridViewAkbiller.DataSource = dt;

                dataGridViewAkbiller.Columns["AkbilSahibiID"].Visible = false;
                dataGridViewAkbiller.Columns["KayitTarihi"].Width = 200;
                dataGridViewAkbiller.Columns["SonKullanimTarihi"].Width = 200;
                dataGridViewAkbiller.Columns["AkbilNo"].Width = 200;

                dataGridViewAkbiller.Columns["AkbilNo"].HeaderText = "Akbil No";
                dataGridViewAkbiller.Columns["KayitTarihi"].HeaderText = "Kayıt Tarihi";
                dataGridViewAkbiller.Columns["SonKullanimTarihi"].HeaderText =
                    "Son Kullanım Tarihi";
                dataGridViewAkbiller.Columns["AkbilTipi"].HeaderText =
                   "Akbil Tipi";

                baglantiNesnesi.Close();
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik bir hata oluştu!");
                    //TODO: loglama txt dosyasına yazdır
            }
        }

        private void dataGridViewAkbiller_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cikisyapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenelIslemler.GirisYapmisKullaniciAdSoyad = string.Empty;
            GenelIslemler.GirisYapmisKullaniciID = 0;
            MessageBox.Show("Güle Güle");
            FrmGiris frmGiris = new FrmGiris();
            frmGiris.Show();
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].Name == "FrmGiris") continue;
                Application.OpenForms[i].Close();
            }
        }
    }
}
