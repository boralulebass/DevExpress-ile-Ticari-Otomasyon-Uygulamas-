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
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string urunid;
        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            txtUrunID.Text = urunid;
            SqlCommand cmd = new SqlCommand("Select * from TBL_FATURADETAY WHERE FDURUNID= '" + urunid + "'", bgl.connection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
               txtUrunAd.Text = reader[1].ToString();
               txtMiktar.Text = reader[2].ToString();
               txtFiyat.Text = reader[3].ToString();
               txtTutar.Text = reader[4].ToString();
            }
            bgl.connection().Close();   


        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            double miktar, fiyat;
            miktar = double.Parse(txtMiktar.Text);
            fiyat = double.Parse(txtFiyat.Text);
            txtTutar.Text = (fiyat * miktar).ToString();
            SqlCommand cmd = new SqlCommand("Update TBL_FATURADETAY set FDUAD=@p1,FDUMIKTAR=@p2,FDUFIYAT=@p3,FDTUTAR=@p4 where FDURUNID=@p5",bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtUrunAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtMiktar.Text);
            cmd.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));
            cmd.Parameters.AddWithValue("@p5", txtUrunID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Değişiklikler Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_FATURADETAY where FDURUNID=@p1",bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtUrunID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Değişiklikler Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
