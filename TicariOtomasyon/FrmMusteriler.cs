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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_MUSTERILER", bgl.connection());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void sehirlistesi()
        {
            SqlCommand cmd = new SqlCommand("Select ILAD from TBL_ILLER", bgl.connection());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                cmbIl.Properties.Items.Add(dr[0]);
                bgl.connection().Close();
            }
        }
        void temizle()
        {
            txtAd.Text = string.Empty;
            txtAdres.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtID.Text = string.Empty;
            txtSoyad.Text = string.Empty;
            txtVergid.Text = string.Empty;
            mskTC.Text = string.Empty;
            mskTel1.Text = string.Empty;
            mskTel2.Text = string.Empty;
            cmbIl.Text = string.Empty;
            cmbIlce.Text = string.Empty;
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirlistesi();
            temizle();
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            cmbIlce.Text = "";
            SqlCommand cmd = new SqlCommand("Select ILCEAD from TBL_ILCELER where ILCESEHIR=@p1", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", cmbIl.SelectedIndex+1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
                bgl.connection().Close();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Insert into TBL_MUSTERILER (MAD,MSOYAD,MTELEFON,MTELEFON2,MTC,MMAIL,MIL,MILCE,MADRES,MVERGIDAIRE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", mskTel1.Text);
            cmd.Parameters.AddWithValue("@p4", mskTel2.Text);
            cmd.Parameters.AddWithValue("@p5", mskTC.Text);
            cmd.Parameters.AddWithValue("@p6", txtEmail.Text.ToLower());
            cmd.Parameters.AddWithValue("@p7", cmbIl.Text);
            cmd.Parameters.AddWithValue("@p8", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@p9", txtAdres.Text);
            cmd.Parameters.AddWithValue("@p10", txtVergid.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Müşteri Sisteme Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_MUSTERILER where MID=@p1", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Müşteri Sistemden Silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null) 
            {
                txtID.Text = dr["MID"].ToString();
                txtAd.Text = dr["MAD"].ToString();
                txtSoyad.Text = dr["MSOYAD"].ToString();
                mskTel1.Text = dr["MTELEFON"].ToString();
                mskTel2.Text = dr["MTELEFON2"].ToString();
                mskTC.Text = dr["MTC"].ToString();
                txtEmail.Text = dr["MMAIL"].ToString();
                cmbIl.Text = dr["MIL"].ToString();
                cmbIlce.Text = dr["MILCE"].ToString();
                txtVergid.Text = dr["MVERGIDAIRE"].ToString();
                txtAdres.Text = dr["MADRES"].ToString();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update TBL_MUSTERILER set MAD=@p1,MSOYAD=@p2,MTELEFON=@p3,MTELEFON2=@p4,MTC=@p5,MMAIL=@p6,MIL=@p7,MILCE=@p8,MADRES=@p9,MVERGIDAIRE=@p10 where MID=@P11", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", mskTel1.Text);
            cmd.Parameters.AddWithValue("@p4", mskTel2.Text);
            cmd.Parameters.AddWithValue("@p5", mskTC.Text);
            cmd.Parameters.AddWithValue("@p6", txtEmail.Text.ToLower());
            cmd.Parameters.AddWithValue("@p7", cmbIl.Text);
            cmd.Parameters.AddWithValue("@p8", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@p9", txtAdres.Text);
            cmd.Parameters.AddWithValue("@p10", txtVergid.Text);
            cmd.Parameters.AddWithValue("@p11", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
