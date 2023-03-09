using DevExpress.Utils.CodedUISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.Charts;

namespace TicariOtomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string ad;

        void giderlistele()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from TBL_GIDERLER", bgl.connection());
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl2.DataSource = dataTable;
        }
        void mlistele()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Execute MusteriHareketler",bgl.connection());
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }
        void flistele()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Execute FirmaHareketler", bgl.connection());
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl3.DataSource = dataTable;
        }
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            mlistele();
            flistele();
            giderlistele();
            lblAktifK.Text = ad;
            
            //TOPLAM TUTARI HESAPLAMA
            SqlCommand cmd = new SqlCommand("Select Sum(FDTUTAR) from TBL_FATURADETAY", bgl.connection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
              lblTutar.Text = reader[0].ToString() + " TL";
            }
            bgl.connection().Close();
            
            //SON AYIN FATURALARI
            SqlCommand cmd1 = new SqlCommand("Select (GIELEKTRIK+GISU+GIDOGALGAZ+GIINTERNET+GIEXTRA) from TBL_GIDERLER order by GIDERID asc", bgl.connection());
            SqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                lblOdeme.Text = reader1[0].ToString() + " TL";
            }
            bgl.connection().Close();

            //SON AYIN PERS MAAŞLARI
            SqlCommand cmd2 = new SqlCommand("Select GIMAASLAR from TBL_GIDERLER order by GIDERID asc",bgl.connection());
            SqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                lblPermaas.Text = reader2[0].ToString() + " TL";
            }
            bgl.connection().Close();


            //TOPLAM MÜŞTERİ SAYISI
            SqlCommand cmd3 = new SqlCommand("Select Count(*) From TBL_MUSTERILER", bgl.connection());
            SqlDataReader reader3 = cmd3.ExecuteReader();
            while (reader3.Read())
            {
                lblMus.Text = reader3[0].ToString();
            }
            bgl.connection().Close();


            //TOPLAM FİRMA SAYISI
            SqlCommand cmd4 = new SqlCommand("Select Count(*) from TBL_FIRMALAR", bgl.connection());
            SqlDataReader reader4 = cmd4.ExecuteReader();
            while (reader4.Read())
            {
                lblFirma.Text = reader4[0].ToString(); 
            }
            bgl.connection().Close();



            //TOPLAM FİRMA ŞEHİR SAYISI
            SqlCommand cmd5 = new SqlCommand("Select Count(Distinct(FIL)) from TBL_FIRMALAR", bgl.connection());
            SqlDataReader reader5 = cmd5.ExecuteReader();
            while (reader5.Read())
            {
                lblSehir.Text = reader5[0].ToString();
            }
            bgl.connection().Close();


            //TOPLAM PERSONEL SAYISI
            SqlCommand cmd6 = new SqlCommand("Select Count(*) from TBL_PERSONELLER", bgl.connection());
            SqlDataReader reader6 = cmd6.ExecuteReader();
            while (reader6.Read())
            {
                lblPersonel.Text = reader6[0].ToString();
            }
            bgl.connection().Close();

            //TOPLAM MÜŞTERİ ŞEHİR SAYISI
            SqlCommand cmd7 = new SqlCommand("Select Count(Distinct(MIL)) from TBL_MUSTERILER", bgl.connection());
            SqlDataReader reader7 = cmd7.ExecuteReader();
            while (reader7.Read())
            {
                lblMusSehir.Text = reader7[0].ToString();
            }
            bgl.connection().Close();

            //TOPLAM MÜŞTERİ ŞEHİR SAYISI
            SqlCommand cmd8 = new SqlCommand("Select sum(UADET) from TBL_URUNLER", bgl.connection());
            SqlDataReader reader8 = cmd8.ExecuteReader();
            while (reader8.Read())
            {
                lblStok.Text = reader8[0].ToString();
            }
            bgl.connection().Close();

            //2.CHART KONTROL SU FATURASI SON4 AY LİSTELEME
            SqlCommand cmd10 = new SqlCommand("Select top 4 GIAY, GISU from TBL_GIDERLER order by GIDERID DESC", bgl.connection());
            SqlDataReader reader10 = cmd10.ExecuteReader();
            while (reader10.Read())
            {
                chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader10[0], reader10[1]));
            }
            bgl.connection().Close();
            
            
        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if(sayac>0 && sayac <=5)
            {
                groupControl9.Text = "Elektrik Faturası";
                chartControl1.Series["Aylar"].Points.Clear();
                //1.CHART KONTROL ELEKTRIK FATURASI SON4 AY LİSTELEME
                SqlCommand cmd9 = new SqlCommand("Select top 4 GIAY, GIELEKTRIK from TBL_GIDERLER order by GIDERID DESC", bgl.connection());
                SqlDataReader reader9 = cmd9.ExecuteReader();
                while (reader9.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader9[0], reader9[1]));
                }
                bgl.connection().Close();
            }

            if (sayac > 5 && sayac <= 10)
            {
                groupControl9.Text = "Su Faturası";
                chartControl1.Series["Aylar"].Points.Clear();
                //2.CHART KONTROL SU FATURASI SON4 AY LİSTELEME
                SqlCommand cmd10 = new SqlCommand("Select top 4 GIAY, GISU from TBL_GIDERLER order by GIDERID DESC", bgl.connection());
                SqlDataReader reader10 = cmd10.ExecuteReader();
                while (reader10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader10[0], reader10[1]));
                }
                bgl.connection().Close();
            }

            if (sayac > 10 && sayac <= 15)
            {
                groupControl9.Text = "Doğalgaz Faturası";
                chartControl1.Series["Aylar"].Points.Clear();
                //2.CHART KONTROL SU FATURASI SON4 AY LİSTELEME
                SqlCommand cmd11 = new SqlCommand("Select top 4 GIAY, GIDOGALGAZ from TBL_GIDERLER order by GIDERID DESC", bgl.connection());
                SqlDataReader reader11 = cmd11.ExecuteReader();
                while (reader11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader11[0], reader11[1]));
                }
                bgl.connection().Close();
            }
            
            if (sayac > 15 && sayac <= 20)
            {
                groupControl9.Text = "İnternet Faturası";
                chartControl1.Series["Aylar"].Points.Clear();
                //2.CHART KONTROL SU FATURASI SON4 AY LİSTELEME
                SqlCommand cmd12 = new SqlCommand("Select top 4 GIAY, GIINTERNET from TBL_GIDERLER order by GIDERID DESC", bgl.connection());
                SqlDataReader reader12 = cmd12.ExecuteReader();
                while (reader12.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader12[0], reader12[1]));
                }
                bgl.connection().Close();
            }

            if (sayac > 20 && sayac <= 25)
            {
                groupControl9.Text = "Extra Ödemeler";
                chartControl1.Series["Aylar"].Points.Clear();
                //2.CHART KONTROL SU FATURASI SON4 AY LİSTELEME
                SqlCommand cmd13 = new SqlCommand("Select top 4 GIAY, GIINTERNET from TBL_GIDERLER order by GIDERID DESC", bgl.connection());
                SqlDataReader reader13 = cmd13.ExecuteReader();
                while (reader13.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader13[0], reader13[1]));
                }
                bgl.connection().Close();
            }

            if (sayac == 26)
            {
                sayac = 0;
            }
        }
        public int sayac2;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl10.Text = "Elektrik Faturası";
                chartControl2.Series["Aylar"].Points.Clear();
                //1.CHART KONTROL ELEKTRIK FATURASI SON4 AY LİSTELEME
                SqlCommand cmd9 = new SqlCommand("Select top 4 GIAY, GIELEKTRIK from TBL_GIDERLER order by GIDERID DESC", bgl.connection());
                SqlDataReader reader9 = cmd9.ExecuteReader();
                while (reader9.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader9[0], reader9[1]));
                }
                bgl.connection().Close();
            }

            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl10.Text = "Su Faturası";
                chartControl2.Series["Aylar"].Points.Clear();
                //2.CHART KONTROL SU FATURASI SON4 AY LİSTELEME
                SqlCommand cmd10 = new SqlCommand("Select top 4 GIAY, GISU from TBL_GIDERLER order by GIDERID DESC", bgl.connection());
                SqlDataReader reader10 = cmd10.ExecuteReader();
                while (reader10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader10[0], reader10[1]));
                }
                bgl.connection().Close();
            }

            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl10.Text = "Doğalgaz Faturası";
                chartControl2.Series["Aylar"].Points.Clear();
                //2.CHART KONTROL DOĞALGAZ FATURASI SON4 AY LİSTELEME
                SqlCommand cmd11 = new SqlCommand("Select top 4 GIAY, GIDOGALGAZ from TBL_GIDERLER order by GIDERID DESC", bgl.connection());
                SqlDataReader reader11 = cmd11.ExecuteReader();
                while (reader11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader11[0], reader11[1]));
                }
                bgl.connection().Close();
            }

            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl10.Text = "İnternet Faturası";
                chartControl2.Series["Aylar"].Points.Clear();
                //2.CHART KONTROL İNTERNET FATURASI SON4 AY LİSTELEME
                SqlCommand cmd12 = new SqlCommand("Select top 4 GIAY, GIINTERNET from TBL_GIDERLER order by GIDERID DESC", bgl.connection());
                SqlDataReader reader12 = cmd12.ExecuteReader();
                while (reader12.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader12[0], reader12[1]));
                }
                bgl.connection().Close();
            }

            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl10.Text = "Extra Ödemeler";
                chartControl2.Series["Aylar"].Points.Clear();
                //2.CHART KONTROL EXTRA SON4 AY LİSTELEME
                SqlCommand cmd13 = new SqlCommand("Select top 4 GIAY, GIINTERNET from TBL_GIDERLER order by GIDERID DESC", bgl.connection());
                SqlDataReader reader13 = cmd13.ExecuteReader();
                while (reader13.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader13[0], reader13[1]));
                }
                bgl.connection().Close();
            }

            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
    
}
