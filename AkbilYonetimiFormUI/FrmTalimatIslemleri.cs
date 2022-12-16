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
    public partial class FrmTalimatIslemleri : Form
    {
        public decimal YuklenecekMiktar { get; private set; }

        public FrmTalimatIslemleri()
        {
            InitializeComponent();
        }

        

        private void ComboBoxAkbilleriGetir()
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
                    cmbBoxAkbiller.DataSource = dt;
                    cmbBoxAkbiller.DisplayMember = "AkbilNo";
                    cmbBoxAkbiller.ValueMember = "AkbilNo";

                    baglantiNesnesi.Close();
                }
                catch (Exception hata)
                {

                    MessageBox.Show("Beklenmedik bir hata oluştu!"+hata.Message);
                    //TODO: loglama txt dosyasına yazdır
                }
            
            
        }

        private void FrmTalimatIslemleri_Load(object sender, EventArgs e)
        {
            groupBoxBakiye.Enabled = false;
            ComboBoxAkbilleriGetir();
            cmbBoxAkbiller.SelectedIndex = -1;
            cmbBoxAkbiller.Text = "Akbil Seçiniz";
            dataGridViewTalimatlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            timerBekleyenTalimat.Interval = 1000;
            timerBekleyenTalimat.Enabled = true;
            //metodu tekrar inceleyeceğiz
            //BekleyenTalimatSayisiniGetir(); // hata verdiği için yorum satırı yaptık
        }

        private void cmbBoxAkbiller_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxAkbiller.SelectedIndex>=0)
            {
                groupBoxBakiye.Enabled = true;
                txtBakiye.Clear();
            }
        }

        private void btnYukle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBakiye == null)
                    throw new Exception("Yüklenecek tutar boş geçilmez!");
                Talimatlar yeniTalimat = new Talimat()
                {
                    AkbilID = cmbBoxAkbiller.SelectedItem.ToString(),
                    OlusturulmaTarihi = DateTime.Now,
                    YuklendiMi = false,
                    YuklenecekMiktar=Convert.ToDecimal(txtBakiye.Text),
                };
                #region TalimatiKaydetKodlari


                string connectionString = @"Server=DESKTOP-OFVK2FD\MSSQLSERVER01;Database=AKBİLYONETİMİDB;Trusted_Connection=True;";
                SqlConnection baglantiNesnesi = new SqlConnection();
                baglantiNesnesi.ConnectionString = connectionString;
                SqlCommand komutNesnesi = new SqlCommand();
                komutNesnesi.Connection = baglantiNesnesi;
                komutNesnesi.CommandText = $"inser into Talimatlar (OlustulmaTarihi,AkbilID,YuklenecekTutar,YuklendiMi,YuklendigiTarih) values (@olusTrh, @akbilid, @tutar, @yukMi, @yukTrh)";
                komutNesnesi.Parameters.AddWithValue("@olusTrh",yeniTalimat.OlusturulmaTarihi);
                komutNesnesi.Parameters.AddWithValue("@akbilid",yeniTalimat.AkbilID);
                komutNesnesi.Parameters.AddWithValue("@tutar",yeniTalimat.YuklenecekMiktar);
                komutNesnesi.Parameters.AddWithValue("@yukMi",yeniTalimat.YuklendiMi);
                komutNesnesi.Parameters.AddWithValue("@yukTrh",yeniTalimat.YuklendigiTarihi);
                    
                
                baglantiNesnesi.Open();
                if (komutNesnesi.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Talimat oluşturuldu!");
                }
                baglantiNesnesi.Close();
                cmbBoxAkbiller.SelectedIndex = -1;
                cmbBoxAkbiller.Text = "Akbil .seçiniz..";
                txtBakiye.Clear();
                groupBoxBakiye.Enabled = false;

                if (checkBoxBekleyenTalimatlar.Checked)
                {
                    GrideTalimatlariGetir(true);
                }
                else
                {
                    GrideTalimatlariGetir();
                }


                #endregion
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik bir hata oluştu" + hata.Message);
            }
        }
        private void GrideTalimatlariGetir (bool sadeceBekleyenleriGoster = false)
        {
            string connectionString = @"Server=DESKTOP-OFVK2FD\MSSQLSERVER01;Database=AKBİLYONETİMİDB;Trusted_Connection=True;";
            SqlConnection baglantiNesnesi = new SqlConnection();
            baglantiNesnesi.ConnectionString = connectionString;
            SqlCommand komutNesnesi = new SqlCommand();
            komutNesnesi.Connection = baglantiNesnesi;
            komutNesnesi.CommandText = $"select * KullanicininTalimatlari where k.Id =@kullanici";
            komutNesnesi.Parameters.AddWithValue("@kullanici", GenelIslemler.GirisYapmisKullaniciID);
            if (sadeceBekleyenleriGoster)
            {
                komutNesnesi.CommandText += "  and t.YuklendiMi=0";
                
            }
           

            SqlDataAdapter adaptor = new SqlDataAdapter(); //adaptor
            adaptor.SelectCommand = komutNesnesi;
            DataTable dt = new DataTable();
            baglantiNesnesi.Open();
            adaptor.Fill(dt);
            dataGridViewTalimatlar.DataSource = dt;

            //dataGridViewAkbiller.Columns["AkbilSahibiID"].Visible = false;
            //dataGridViewAkbiller.Columns["KayitTarihi"].Width = 200;
            //dataGridViewAkbiller.Columns["SonKullanimTarihi"].Width = 200;
            //dataGridViewAkbiller.Columns["AkbilNo"].Width = 200;

            
            //dataGridViewAkbiller.Columns["AkbilTipi"].HeaderText =
            //   "Akbil Tipi";

            baglantiNesnesi.Close();
        }

        private void checkBoxBekleyenTalimatlar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBekleyenTalimatlar.Checked)
            {
                GrideTalimatlariGetir(true);
            }
            else
            {
                GrideTalimatlariGetir(false);
            }
        }

        private void BekleyenTalimatSayisiniGetir()
        {
            try
            {
                string connectionString =
                   @"Server=DESKTOP-OFVK2FD\MSSQLSERVER01;Database=AKBILYONETIMIDB;Trusted_Connection=True;";
                SqlConnection baglantiNesnesi = new SqlConnection();
                baglantiNesnesi.ConnectionString = connectionString;
                //SqlCommand komutNesnesi = new SqlCommand();
                //komutNesnesi.Connection = baglantiNesnesi;
                //komutNesnesi.CommandText= "SP_BekleyenTalimatSayisi";

                //kısa yoldan ctor'un 2 parametreli olanını kullan
                SqlCommand komutNesnesi = new SqlCommand("SP_BekleyenTalimatSayisi", baglantiNesnesi);
                komutNesnesi.CommandType = CommandType.StoredProcedure;
                komutNesnesi.Parameters.AddWithValue("@kullaniciId", GenelIslemler.GirisYapmisKullaniciID);
                // devam edeceğiz...
                SqlParameter RuturnValue = new SqlParameter("@return_value", SqlDbType.Int);
                RuturnValue.Direction = ParameterDirection.Output;
                komutNesnesi.Parameters.Add(RuturnValue);
                baglantiNesnesi.Open();
                komutNesnesi.ExecuteNonQuery();
                lblBekleyenTalimat.Text = komutNesnesi.Parameters["@return_value"].Value.ToString();
                baglantiNesnesi.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Beklenmedik bir sorun oluştu!" + hata.Message);
                // hata log
            }

        }

        private void timerBekleyenTalimat_Tick(object sender, EventArgs e)
        {
           // if (lblBekleyenTalimat.Text!) "0")
            //{
                if (DateTime.Now.Second % 2==0)
                {
                    lblBekleyenTalimat.Font = new Font("Microsoft Sans Serif", 40);
                }
                else
                {
                    lblBekleyenTalimat.Font = new Font("Microsoft Sans Serif", 20);

                }
           // }
        }

        private void cikisyapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //sistemden çıkılacak
            GenelIslemler.GirisYapmisKullaniciAdSoyad = string.Empty;
            GenelIslemler.GirisYapmisKullaniciID = 0;
            MessageBox.Show("Güle Güle");
            FrmGiris frmGiris = new FrmGiris();
            frmGiris.Show();
            this.Close(); // deneme için yazdık

        }

        private void anaMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmIslemler frmIslemler = new FrmIslemler();
            frmIslemler.Show();
            this.Close();
        }
    }
}
