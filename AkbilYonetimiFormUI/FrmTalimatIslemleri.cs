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

                    MessageBox.Show("Beklenmedik bir hata oluştu! "+hata.Message);
                    //TODO: loglama txt dosyasına yazdır
                }
            
            
        }

        private void FrmTalimatIslemleri_Load(object sender, EventArgs e)
        {
            
            ComboBoxAkbilleriGetir();
            cmbBoxAkbiller.SelectedIndex = -1;
            cmbBoxAkbiller.Text = "Akbil Seçiniz";
            dataGridViewTalimatlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            timerBekleyenTalimat.Interval = 1000;
            timerBekleyenTalimat.Enabled = true;
            //metodu tekrar inceleyeceğiz
            GrideTalimatlariGetir();
            BekleyenTalimatSayisiniGetir(); // hata verdiği için yorum satırı yaptık
            dataGridViewTalimatlar.ContextMenuStrip = contextMenuStripTalimatGrid;
            groupBoxBakiye.Enabled = false;
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
                if (cmbBoxAkbiller.SelectedIndex < 0)
                    throw new Exception("Talimat yüklemek için akbil seçiniz");
                if (txtBakiye.Text == null || txtBakiye.Text == string.Empty)
                    throw new Exception("Yüklenecek tutar boş geçilemez");
                Talimat yeniTalimat = new Talimat()
                {
                    AkbilID = cmbBoxAkbiller.SelectedValue.ToString(),
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
                komutNesnesi.CommandText = $"insert into Talimatlar (OlustulmaTarihi,AkbilID,YuklenecekTutar,YuklendiMi,YuklendigiTarih) values (@olusTrh, @akbilid, @tutar,0, null)";

                komutNesnesi.Parameters.AddWithValue("@olusTrh",yeniTalimat.OlusturulmaTarihi);
                komutNesnesi.Parameters.AddWithValue("@akbilid",yeniTalimat.AkbilID);
                komutNesnesi.Parameters.AddWithValue("@tutar",yeniTalimat.YuklenecekMiktar);
                
                
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
                BekleyenTalimatSayisiniGetir();


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
            komutNesnesi.CommandText = $"select * from KullanicininTalimatlari where KullaniciId =@kullanici";
            komutNesnesi.Parameters.AddWithValue("@kullanici", GenelIslemler.GirisYapmisKullaniciID);
            if (sadeceBekleyenleriGoster)
            {
                komutNesnesi.CommandText += "  and YuklendiMi=0";
                
            }
           

            SqlDataAdapter adaptor = new SqlDataAdapter(); //adaptor
            adaptor.SelectCommand = komutNesnesi;
            DataTable dt = new DataTable();
            baglantiNesnesi.Open();
            adaptor.Fill(dt);
            dataGridViewTalimatlar.DataSource = dt;

            dataGridViewTalimatlar.Columns["Id"].Visible = false;
            dataGridViewTalimatlar.Columns["KullaniciId"].Visible = false;
            dataGridViewTalimatlar.Columns["OlustulmaTarihi"].Width = 150;
            dataGridViewTalimatlar.Columns["YuklendigiTarih"].Width = 200;
            dataGridViewTalimatlar.Columns["AkbilID"].Width = 200;


            dataGridViewTalimatlar.Columns["YuklendigiTarih"].HeaderText =
               "Yüklendiği Tarih";

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
                   @"Server=DESKTOP-OFVK2FD\MSSQLSERVER01;Database=AKBİLYONETİMİDB;Trusted_Connection=True;";
                SqlConnection baglantiNesnesi = new SqlConnection();
                baglantiNesnesi.ConnectionString = connectionString;
                //SqlCommand komutNesnesi = new SqlCommand();
                //komutNesnesi.Connection = baglantiNesnesi;
                //komutNesnesi.CommandText= "SP_BekleyenTalimatSayisi";

                //kısa yoldan ctor'un 2 parametreli olanını kullan
                SqlCommand komutNesnesi = new SqlCommand("SP_BekleyenTalimatSayisi", baglantiNesnesi);
                komutNesnesi.CommandType = CommandType.StoredProcedure;

                SqlParameter kullaniciId = komutNesnesi.Parameters.AddWithValue("@kullaniciId", GenelIslemler.GirisYapmisKullaniciID);
                kullaniciId.Direction = ParameterDirection.Input;
                SqlParameter returnValue = new SqlParameter("@return_value", SqlDbType.Int);
                returnValue.Direction = ParameterDirection.ReturnValue;
                komutNesnesi.Parameters.Add(returnValue);
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
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].Name == "FrmGiris") continue;
                Application.OpenForms[i].Close(); 
            }


        }

        private void anaMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmIslemler frmIslemler = new FrmIslemler();
            frmIslemler.Show();
            this.Close();
        }

        private void groupBoxBakiye_Enter(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void talimatigerceklestirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int sayac = 0;
                foreach (DataGridViewRow item in dataGridViewTalimatlar.SelectedRows)
                {
                    string connectionString = @"Server=DESKTOP-OFVK2FD\MSSQLSERVER01;Database=AKBİLYONETİMİDB;Trusted_Connection=True;";
                    SqlConnection baglantiNesnesi = new SqlConnection();
                    baglantiNesnesi.ConnectionString = connectionString;
                    SqlCommand komutNesnesi = new SqlCommand();
                    komutNesnesi.Connection = baglantiNesnesi;
                    komutNesnesi.CommandText = $"update Talimatlar set YuklendiMi=1 , YuklendigiTarih=yukTrh where Id= @id";
                    var talimatId = item.Cells["Id"].Value;
                    komutNesnesi.Parameters.AddWithValue("@id",talimatId);
                    komutNesnesi.Parameters.AddWithValue("@yukTrh",DateTime.Now);


                    baglantiNesnesi.Open();
                    sayac += komutNesnesi.ExecuteNonQuery();
                    
                    baglantiNesnesi.Close(); 
                }
                MessageBox.Show($"Gerçekleşen talimat sayısı {sayac}");
                GrideTalimatlariGetir();
                BekleyenTalimatSayisiniGetir();
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik bir hata oluştu! " + hata.Message);
            }
        }

        private void talimatiSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int sayac = 0;
                foreach (DataGridViewRow item in dataGridViewTalimatlar.SelectedRows)
                {
                    string connectionString = @"Server=DESKTOP-OFVK2FD\MSSQLSERVER01;Database=AKBİLYONETİMİDB;Trusted_Connection=True;";
                    SqlConnection baglantiNesnesi = new SqlConnection();
                    baglantiNesnesi.ConnectionString = connectionString;
                    SqlCommand komutNesnesi = new SqlCommand();
                    komutNesnesi.Connection = baglantiNesnesi;
                    komutNesnesi.CommandText = $"delete from Talimatlar where Id=@id";
                    var talimatId = item.Cells["Id"].Value;
                    komutNesnesi.Parameters.AddWithValue("@id", talimatId);
                    baglantiNesnesi.Open();
                    sayac += komutNesnesi.ExecuteNonQuery();
                    baglantiNesnesi.Close();
                }
                MessageBox.Show($"Silinen/iptal edilen talimat sayısı ={sayac}");
                GrideTalimatlariGetir();
                BekleyenTalimatSayisiniGetir();
            }
            catch (Exception hata)
            {

                MessageBox.Show("Beklenmedik bir hata oluştu! " + hata.Message);
            }
        }
    }
}
