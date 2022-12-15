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
    public partial class FrmTalimatIslemleri : Form
    {
        public FrmTalimatIslemleri()
        {
            InitializeComponent();
        }

        private void groupBoxBakiye_Enter(object sender, EventArgs e)
        {
            groupBoxBakiye.Enabled = false;
            ComboBoxAkbilleriGetir();
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
    }
}
