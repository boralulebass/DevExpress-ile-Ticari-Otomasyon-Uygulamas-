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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select FBILGIID as 'Bilgi ID', FSERI as 'Seri', FSIRANO as 'Sıra No',FTARIH as 'Tarih',FSAAT as 'Saat',FVERGIDAIRE as 'Vergi Dairesi', FALICI as 'Müşteri', FTESLIMEDEN as 'Faturayı Teslim Eden', FTESLIMALAN as 'Faturayı Teslim Alan' from TBL_FATURABILGI",bgl.connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void Temizle()
        {
           txtID.Text = string.Empty;
            txtAlıcı.Text = string.Empty;
            txtFaturaID.Text = string.Empty;
            txtFiyat.Text = string.Empty;
            txtMiktar.Text = string.Empty;
            txtSeri.Text = string.Empty;
            txtSira.Text = string.Empty;
            txtTeslimA.Text = string.Empty;
            txtTeslime.Text = string.Empty;
            txtTutar.Text = string.Empty;
            txtUrunAd.Text = string.Empty;
            txtUrunID.Text = string.Empty;
            txtVergi.Text = string.Empty;
            mskSaat.Text = string.Empty;
            mskTarih.Text = string.Empty;
        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            if (txtFaturaID.Text == "")
            {
                SqlCommand cmd = new SqlCommand("Insert into TBL_FATURABILGI (FSERI,FSIRANO,FTARIH,FSAAT,FVERGIDAIRE,FALICI,FTESLIMEDEN,FTESLIMALAN) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.connection());
                cmd.Parameters.AddWithValue("@p1", txtSeri.Text);
                cmd.Parameters.AddWithValue("@p2", txtSira.Text);
                cmd.Parameters.AddWithValue("@p3", mskTarih.Text);
                cmd.Parameters.AddWithValue("@p4", mskSaat.Text);
                cmd.Parameters.AddWithValue("@p5", txtVergi.Text);
                cmd.Parameters.AddWithValue("@p6", txtAlıcı.Text);
                cmd.Parameters.AddWithValue("@p7", txtTeslime.Text);
                cmd.Parameters.AddWithValue("@p8", txtTeslimA.Text);
                cmd.ExecuteNonQuery();
                bgl.connection().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
            if (txtFaturaID.Text != "" && comboBox1.Text == "Firma")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(txtFiyat.Text);
                miktar = Convert.ToDouble(txtMiktar.Text);
                tutar = miktar * fiyat;
                txtTutar.Text = tutar.ToString();
                SqlCommand cmd2 = new SqlCommand("Insert into TBL_FATURADETAY (FDUAD,FDUMIKTAR,FDUFIYAT,FDTUTAR,FDFATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.connection());
                cmd2.Parameters.AddWithValue("@p1", txtUrunAd.Text);
                cmd2.Parameters.AddWithValue("@p2", txtMiktar.Text);
                cmd2.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
                cmd2.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));
                cmd2.Parameters.AddWithValue("@p5", txtFaturaID.Text);
                cmd2.ExecuteNonQuery();
                bgl.connection().Close();

                SqlCommand cmd3 = new SqlCommand("update TBL_URUNLER set UADET=UADET-@s1 where UID=@s2", bgl.connection());
                cmd3.Parameters.AddWithValue("@s1", txtMiktar.Text);
                cmd3.Parameters.AddWithValue("@s2", txtUrunID.Text);
                cmd3.ExecuteNonQuery();
                bgl.connection().Close();

                SqlCommand cmd4 = new SqlCommand("Insert into TBL_HAREKETLER (URUNID,ADET,PERSONEL,FIRMA,FIYAT,TOPLAM,FATURAID,TARIH) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.connection());
                cmd4.Parameters.AddWithValue("@h1", txtUrunID.Text);
                cmd4.Parameters.AddWithValue("@h2", txtMiktar.Text);
                cmd4.Parameters.AddWithValue("@h3", txtPerso.Text);
                cmd4.Parameters.AddWithValue("@h4", txtFirma.Text);
                cmd4.Parameters.AddWithValue("@h5", decimal.Parse(txtFiyat.Text));
                cmd4.Parameters.AddWithValue("@h6", decimal.Parse(txtTutar.Text));
                cmd4.Parameters.AddWithValue("@h7", txtFaturaID.Text);
                cmd4.Parameters.AddWithValue("@h8", mskTarih.Text);
                cmd4.ExecuteNonQuery();
                bgl.connection().Close();
                MessageBox.Show("Fatura Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();

                if (txtFaturaID.Text != "" && comboBox1.Text == "Müşteri")
                {
                    double miktar1, tutar1, fiyat1;
                    fiyat1 = Convert.ToDouble(txtFiyat.Text);
                    miktar1 = Convert.ToDouble(txtMiktar.Text);
                    tutar1 = miktar1 * fiyat1;
                    txtTutar.Text = tutar1.ToString();
                    SqlCommand cmd2m = new SqlCommand("Insert into TBL_FATURADETAY (FDUAD,FDUMIKTAR,FDUFIYAT,FDTUTAR,FDFATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.connection());
                    cmd2m.Parameters.AddWithValue("@p1", txtUrunAd.Text);
                    cmd2m.Parameters.AddWithValue("@p2", txtMiktar.Text);
                    cmd2m.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
                    cmd2m.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));
                    cmd2m.Parameters.AddWithValue("@p5", txtFaturaID.Text);
                    cmd2m.ExecuteNonQuery();
                    bgl.connection().Close();

                    SqlCommand cmd3m = new SqlCommand("update TBL_URUNLER set UADET=UADET-@s1 where UID=@s2", bgl.connection());
                    cmd3m.Parameters.AddWithValue("@s1", txtMiktar.Text);
                    cmd3m.Parameters.AddWithValue("@s2", txtUrunID.Text);
                    cmd3m.ExecuteNonQuery();
                    bgl.connection().Close();

                    SqlCommand cmd4m = new SqlCommand("Insert into TBL_MHAREKETLER (URUNID,ADET,PERSONEL,MUSTERI,FIYAT,TOPLAM,FATURAID,TARIH) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.connection());
                    cmd4m.Parameters.AddWithValue("@h1", txtUrunID.Text);
                    cmd4m.Parameters.AddWithValue("@h2", txtMiktar.Text);
                    cmd4m.Parameters.AddWithValue("@h3", txtPerso.Text);
                    cmd4m.Parameters.AddWithValue("@h4", txtFirma.Text);
                    cmd4m.Parameters.AddWithValue("@h5", decimal.Parse(txtFiyat.Text));
                    cmd4m.Parameters.AddWithValue("@h6", decimal.Parse(txtTutar.Text));
                    cmd4m.Parameters.AddWithValue("@h7", txtFaturaID.Text);
                    cmd4m.Parameters.AddWithValue("@h8", mskTarih.Text);
                    cmd4m.ExecuteNonQuery();
                    bgl.connection().Close();
                    MessageBox.Show("Fatura Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele();
                    Temizle();
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null) 
            {
                txtSeri.Text = dr["Seri"].ToString();
                txtSira.Text = dr["Sıra No"].ToString();
                mskTarih.Text = dr["Tarih"].ToString();
                mskSaat.Text = dr["Saat"].ToString() ;
                txtVergi.Text = dr["Vergi Dairesi"].ToString();
                txtAlıcı.Text = dr["Müşteri"].ToString();
                txtTeslime.Text = dr["Faturayı Teslim Eden"].ToString();
                txtTeslimA.Text = dr["Faturayı Teslim Alan"].ToString();
                txtID.Text = dr["Bilgi ID"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_FATURABILGI where FBILGIID=@p1", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Fatura Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Listele();
            Temizle();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_FATURABILGI set FSERI=@p1,FSIRANO=@p2,FTARIH=@p3,FSAAT=@p4,FVERGIDAIRE=@p5,FALICI=@p6,FTESLIMEDEN=@p7,FTESLIMALAN=@p8 where FBILGIID=@p9",bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtSeri.Text);
            cmd.Parameters.AddWithValue("@p2", txtSira.Text);
            cmd.Parameters.AddWithValue("@p3", mskTarih.Text);
            cmd.Parameters.AddWithValue("@p4", mskSaat.Text);
            cmd.Parameters.AddWithValue("@p5", txtVergi.Text);
            cmd.Parameters.AddWithValue("@p6", txtAlıcı.Text);
            cmd.Parameters.AddWithValue("@p7", txtTeslime.Text);
            cmd.Parameters.AddWithValue("@p8", txtTeslimA.Text);
            cmd.Parameters.AddWithValue("@p9", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Fatura Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunler fr = new FrmFaturaUrunler();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null ) 
            {
                fr.id = dr["Bilgi ID"].ToString();
            }
            fr.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select UAD,USATISFIYAT from TBL_URUNLER where UID=@p1",bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtUrunID.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                txtUrunAd.Text = dr[0].ToString();
                txtFiyat.Text = dr[1].ToString();
            }
            bgl.connection().Close();
        }
    }
}
