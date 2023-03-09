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

namespace TicariOtomasyon
{
    public partial class FrmAnasayfa : Form
    {
        public FrmAnasayfa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void azalantok()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select UAD as 'Ürün Adı', sum(UADET) as 'Adet' from TBL_URUNLER group by UAD \r\nhaving Sum(UADET) <=20 order by Sum(UADET)", bgl.connection());
            adapter.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void ajanda()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Top 5 TARIH,SAAT,BASLIK from TBL_NOTLAR order by ID desc", bgl.connection());
            da.Fill(dataTable);
            gridControl2.DataSource = dataTable;
        }

        void sonhareketler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select UAD as 'Ürün Adı',ADET as 'Adet',FAD as 'Firma',(Select USATISFIYAT from TBL_URUNLER where UID=TBL_HAREKETLER.URUNID) as 'Satış Fiyatı',TOPLAM as 'Toplam',TARIH as 'Tarih' from TBL_HAREKETLER\r\ninner join TBL_URUNLER\r\non\r\nTBL_URUNLER.UID=TBL_HAREKETLER.URUNID\r\ninner join TBL_FIRMALAR\r\non\r\nTBL_FIRMALAR.FID=TBL_HAREKETLER.FIRMA", bgl.connection());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        void firmalar()
        {
            DataTable da = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select FAD as 'Firma Adı',FYADSOYAD as 'Yetkili Adı Soyadı', FTELEFON1 as 'Telefon' from TBL_FIRMALAR", bgl.connection());
            sqlDataAdapter.Fill(da);
            gridControl4.DataSource = da;
        }
        private void FrmAnasayfa_Load(object sender, EventArgs e)
        {
            azalantok();
            ajanda();
            sonhareketler();
            firmalar();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
        }
    }
}
