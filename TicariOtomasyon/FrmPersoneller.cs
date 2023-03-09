using DevExpress.XtraBars;
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

namespace TicariOtomasyon
{
    public partial class FrmPersoneller : Form
    {
        public FrmPersoneller()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select PID as 'ID', PAD as 'Ad', PSOYAD as 'Soyad',PTELEFON as 'Telefon', PTC as 'TC No', PMAIL as 'E-Mail', PIL as 'İl', PILCE as 'İlçe',PADRES as 'Adres', PTITLE as 'Görev' From TBL_PERSONELLER",bgl.connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            bgl.connection().Close();
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
        void Temizle()
        {
            txtAd.Text = string.Empty;
            txtSoyad.Text = string.Empty;
            txtID.Text = string.Empty;
            txtGorev.Text = string.Empty;
            txtAdres.Text = string.Empty;
            txtEmail.Text = string.Empty;
            mskTC.Text = string.Empty;
            mskTel1.Text = string.Empty;
            cmbIl.Text = string.Empty;
            cmbIlce.Text = string.Empty;
        }
        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
            Listele();
            sehirlistesi();
            Temizle();
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            cmbIlce.Text = "";
            SqlCommand cmd = new SqlCommand("Select ILCEAD from TBL_ILCELER where ILCESEHIR=@p1", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", cmbIl.SelectedIndex + 1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
                bgl.connection().Close();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Insert into TBL_PERSONELLER (PAD,PSOYAD,PTELEFON,PTC,PMAIL,PIL,PILCE,PADRES,PTITLE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", mskTel1.Text);
            cmd.Parameters.AddWithValue("@p4", mskTC.Text);
            cmd.Parameters.AddWithValue("@p5", txtEmail.Text);
            cmd.Parameters.AddWithValue("@p6", cmbIl.Text);
            cmd.Parameters.AddWithValue("@p7", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@p8", txtAdres.Text);
            cmd.Parameters.AddWithValue("@p9", txtGorev.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Personel Sisteme Eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();
            Temizle();
            

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null) 
            {
                txtID.Text = dr["ID"].ToString();
                txtAd.Text = dr["Ad"].ToString();
                txtSoyad.Text = dr["Soyad"].ToString();
                txtEmail.Text = dr["E-Mail"].ToString();
                txtGorev.Text = dr["Görev"].ToString();
                txtAdres.Text = dr["Adres"].ToString();
                mskTC.Text = dr["TC No"].ToString();
                mskTel1.Text = dr["Telefon"].ToString();
                cmbIl.Text = dr["İl"].ToString();
                cmbIlce.Text = dr["İlçe"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_PERSONELLER where PID=@p1", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Personel Sistemden Silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            Listele();
            Temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_PERSONELLER set PAD=@p1,PSOYAD=@p2,PTELEFON=@p3,PTC=@p4,PMAIL=@p5,PIL=@p6,PILCE=@p7,PADRES=@p8,PTITLE=@p9 where PID=@p10",bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", mskTel1.Text);
            cmd.Parameters.AddWithValue("@p4", mskTC.Text);
            cmd.Parameters.AddWithValue("@p5", txtEmail.Text);
            cmd.Parameters.AddWithValue("@p6", cmbIl.Text);
            cmd.Parameters.AddWithValue("@p7", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@p8", txtAdres.Text);
            cmd.Parameters.AddWithValue("@p9", txtGorev.Text);
            cmd.Parameters.AddWithValue("@p10", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Personel Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }
    }
}
