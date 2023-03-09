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
using System.Xml;
using DevExpress.Utils.Behaviors;

namespace TicariOtomasyon
{
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select ID, TARIH as 'Tarih',SAAT as 'Saat',BASLIK as 'Başlık',DETAY as 'Detay',OLUSTURAN as 'Oluşturan',HITAP as 'Hitap' from TBL_NOTLAR", bgl.connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void Temizle()
        {
            txtBaslik.Text = string.Empty;
            txtDetay.Text = string.Empty;
            txtHitap.Text = string.Empty;
            txtID.Text = string.Empty;
            txtOlusturan.Text = string.Empty;
            mskSaat.Text = string.Empty;
            mskTarih.Text = string.Empty;
        }
        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Insert into TBL_NOTLAR (TARIH,SAAT,BASLIK,DETAY,OLUSTURAN,HITAP) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", mskTarih.Text);
            cmd.Parameters.AddWithValue("@p2", mskSaat.Text);
            cmd.Parameters.AddWithValue("@p3", txtBaslik.Text);
            cmd.Parameters.AddWithValue("@p4", txtDetay.Text);
            cmd.Parameters.AddWithValue("@p5", txtOlusturan.Text);
            cmd.Parameters.AddWithValue("@p6", txtHitap.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Not Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtBaslik.Text = dr["Başlık"].ToString();
                txtDetay.Text = dr["Detay"].ToString();
                txtHitap.Text = dr["Hitap"].ToString();
                txtID.Text = dr["ID"].ToString();
                txtOlusturan.Text = dr["Oluşturan"].ToString();
                mskSaat.Text = dr["Saat"].ToString();
                mskTarih.Text = dr["Tarih"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_NOTLAR where ID=@p1", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Not Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Listele();
            Temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_NOTLAR set TARIH=@p1,SAAT=@p2,BASLIK=@p3,DETAY=@p4,OLUSTURAN=@p5,HITAP=@p6 where ID=@p7",bgl.connection());
            cmd.Parameters.AddWithValue("@p1", mskTarih.Text);
            cmd.Parameters.AddWithValue("@p2", mskSaat.Text);
            cmd.Parameters.AddWithValue("@p3", txtBaslik.Text);
            cmd.Parameters.AddWithValue("@p4", txtDetay.Text);
            cmd.Parameters.AddWithValue("@p5", txtOlusturan.Text);
            cmd.Parameters.AddWithValue("@p6", txtHitap.Text);
            cmd.Parameters.AddWithValue("@p7", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Not Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay frm = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null ) 
            {
                frm.metin = dr["Detay"].ToString();
            }
            frm.Show();
        }
    }
}
