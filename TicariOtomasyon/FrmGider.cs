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
using DevExpress.Utils.CodedUISupport;
using DevExpress.XtraBars.ViewInfo;

namespace TicariOtomasyon
{
    public partial class FrmGider : Form
    {
        public FrmGider()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select GIDERID as 'ID',GIAY as 'Ay',GIYIL as 'Yıl', GIELEKTRIK as 'Elektrik Faturası', GISU as 'Su Faturası',GIDOGALGAZ as 'Doğalgaz Faturası',GIINTERNET as 'İnternet Faturası',GIMAASLAR as 'Maaşlar',GIEXTRA as 'Extra Harcamalar', GINOTLAR as 'Notlar' from TBL_GIDERLER Order By ID ASC", bgl.connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void Temizle()
        {
            txtDogalgaz.Text = string.Empty;
            txtElektrik.Text = string.Empty;
            txtExtra.Text = string.Empty;
            txtID.Text = string.Empty;
            txtInternet.Text = string.Empty;
            txtMaas.Text = string.Empty;
            txtNotlar.Text = string.Empty;
            txtSu.Text = string.Empty;
            cmbAy.Text = string.Empty;
            cmbYil.Text = string.Empty;
        }
        private void FrmGider_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Insert into TBL_GIDERLER (GIAY,GIYIL,GIELEKTRIK,GISU,GIDOGALGAZ,GIINTERNET,GIMAASLAR,GIEXTRA,GINOTLAR) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", cmbAy.Text);
            cmd.Parameters.AddWithValue("@p2", cmbYil.Text);
            cmd.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            cmd.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            cmd.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            cmd.Parameters.AddWithValue("@p7", decimal.Parse(txtMaas.Text));
            cmd.Parameters.AddWithValue("@p8", decimal.Parse(txtExtra.Text));
            cmd.Parameters.AddWithValue("@p9", txtNotlar.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Gider Bilgileri Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_GIDERLER where GIDERID=@p1", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Gider Bilgileri Sistemden Silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            Temizle();
            Listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null) 
            {
                txtDogalgaz.Text = dr["Doğalgaz Faturası"].ToString();
                txtElektrik.Text = dr["ELektrik Faturası"].ToString();
                txtExtra.Text = dr["Extra Harcamalar"].ToString();
                txtID.Text = dr["ID"].ToString();
                txtInternet.Text = dr["İnternet Faturası"].ToString();
                txtMaas.Text = dr["Maaşlar"].ToString();
                txtNotlar.Text = dr["Notlar"].ToString();
                txtSu.Text = dr["Su Faturası"].ToString();
                cmbAy.Text = dr["Ay"].ToString();
                cmbYil.Text = dr["Yıl"].ToString();
            }   
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_GIDERLER set GIAY=@p1,GIYIL=@p2,GIELEKTRIK=@p3,GISU=@p4,GIDOGALGAZ=@p5,GIINTERNET=@p6,GIMAASLAR=@p7,GIEXTRA=@p8,GINOTLAR=@p9 where GIDERID=@p10", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", cmbAy.Text);
            cmd.Parameters.AddWithValue("@p2", cmbYil.Text);
            cmd.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            cmd.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            cmd.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            cmd.Parameters.AddWithValue("@p7", decimal.Parse(txtMaas.Text));
            cmd.Parameters.AddWithValue("@p8", decimal.Parse(txtExtra.Text));
            cmd.Parameters.AddWithValue("@p9", txtNotlar.Text);
            cmd.Parameters.AddWithValue("p10", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Gider Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();

        }


    }
}
