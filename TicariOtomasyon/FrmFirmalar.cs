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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select FID as 'ID', FAD as 'Firma Adı', FSEKTOR as 'Sektör', FYETKILISTATU as 'Yetkili Statüsü', FYADSOYAD as 'Yetkili Ad Soyad', FYETKILITC as 'Yetkili TC No', FTELEFON1 as 'Telefon', FTELEFON2 as '2. Telefon', FTELEFON3 as '3. Telefon', FMAIL as 'E-Mail', FFAX as 'Fax', FIL as 'İl', FILCE as 'İlçe', FVERGIDAIRE as 'Vergi Dairesi', FADRES as 'Adres', FOZELKOD1, FOZELKOD2, FOZELKOD3  from TBL_FIRMALAR", bgl.connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        void Temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtSektor.Text = "";
            txtYGorev.Text = "";
            txtYetkili.Text = "";
            mskYTC.Text = "";
            txtEmail.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            txtVergid.Text = "";
            txtAdres.Text = "";
            mskFax.Text = "";
            mskYTel1.Text = "";
            mskYTel2.Text = "";
            mskYTel3.Text = "";
            txtKod1.Text = "";
            txtKod2.Text = "";
            txtKod3.Text = "";
            txtAd.Focus();
        }
        void Sehirlistesi()
        {
            SqlCommand cmd = new SqlCommand("Select ILAD from TBL_ILLER", bgl.connection());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
                bgl.connection().Close();
            }
        }
        void Carikodaciklama()
        {
            SqlCommand cmd = new SqlCommand("Select FIRMAKOD1 from TBL_KODLAR", bgl.connection());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                rchKod1.Text= dr[0].ToString();
            }
            bgl.connection().Close();
        }
        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            Listele();
            Sehirlistesi();
            Temizle();
            Carikodaciklama();
            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["ID"].ToString();
                txtAd.Text = dr["Firma Adı"].ToString();
                txtSektor.Text = dr["Sektör"].ToString();
                txtYGorev.Text = dr["Yetkili Statüsü"].ToString();
                txtYetkili.Text = dr["Yetkili Ad Soyad"].ToString();
                mskYTC.Text = dr["Yetkili TC No"].ToString();
                txtEmail.Text = dr["E-Mail"].ToString();
                cmbIl.Text = dr["İl"].ToString();
                cmbIlce.Text = dr["İlçe"].ToString();
                txtVergid.Text = dr["Vergi Dairesi"].ToString();
                txtAdres.Text = dr["Adres"].ToString();
                mskFax.Text = dr["Fax"].ToString();
                mskYTel1.Text = dr["Telefon"].ToString();
                mskYTel2.Text = dr["2. Telefon"].ToString();
                mskYTel3.Text = dr["3. Telefon"].ToString();
                txtKod1.Text = dr["FOZELKOD1"].ToString();
                txtKod2.Text = dr["FOZELKOD2"].ToString();
                txtKod3.Text = dr["FOZELKOD3"].ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Insert into TBL_FIRMALAR (FAD,FSEKTOR,FYETKILISTATU,FYADSOYAD,FYETKILITC,FTELEFON1,FTELEFON2,FTELEFON3,FMAIL,FFAX,FIL,FILCE,FVERGIDAIRE,FADRES,FOZELKOD1,FOZELKOD2,FOZELKOD3) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSektor.Text);
            cmd.Parameters.AddWithValue("@p3", txtYGorev.Text);
            cmd.Parameters.AddWithValue("@p4", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@p5", mskYTC.Text);
            cmd.Parameters.AddWithValue("@p6", mskYTel1.Text);
            cmd.Parameters.AddWithValue("@p7", mskYTel2.Text);
            cmd.Parameters.AddWithValue("@p8", mskYTel3.Text);
            cmd.Parameters.AddWithValue("@p9", txtEmail.Text.ToLower());
            cmd.Parameters.AddWithValue("@p10", mskFax.Text);
            cmd.Parameters.AddWithValue("@p11", cmbIl.Text);
            cmd.Parameters.AddWithValue("@p12", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@p13", txtVergid.Text);
            cmd.Parameters.AddWithValue("@p14", txtAdres.Text);
            cmd.Parameters.AddWithValue("@p15", txtKod1.Text);
            cmd.Parameters.AddWithValue("@p16", txtKod2.Text);
            cmd.Parameters.AddWithValue("@p17", txtKod3.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Firma Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
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

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_FIRMALAR where FID=@p1", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Firma Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Listele();
            Temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_FIRMALAR set FAD=@p1,FSEKTOR=@p2,FYETKILISTATU=@p3,FYADSOYAD=@p4,FYETKILITC=@p5,FTELEFON1=@p6,FTELEFON2=@p7,FTELEFON3=@p8,FMAIL=@p9,FFAX=@p10,FIL=@p11,FILCE=@p12,FVERGIDAIRE=@p13,FADRES=@p14,FOZELKOD1=@p15,FOZELKOD2=@p16,FOZELKOD3=@p17 where FID=@p18", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSektor.Text);
            cmd.Parameters.AddWithValue("@p3", txtYGorev.Text);
            cmd.Parameters.AddWithValue("@p4", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@p5", mskYTC.Text);
            cmd.Parameters.AddWithValue("@p6", mskYTel1.Text);
            cmd.Parameters.AddWithValue("@p7", mskYTel2.Text);
            cmd.Parameters.AddWithValue("@p8", mskYTel3.Text);
            cmd.Parameters.AddWithValue("@p9", txtEmail.Text.ToLower());
            cmd.Parameters.AddWithValue("@p10", mskFax.Text);
            cmd.Parameters.AddWithValue("@p11", cmbIl.Text);
            cmd.Parameters.AddWithValue("@p12", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@p13", txtVergid.Text);
            cmd.Parameters.AddWithValue("@p14", txtAdres.Text);
            cmd.Parameters.AddWithValue("@p15", txtKod1.Text);
            cmd.Parameters.AddWithValue("@p16", txtKod2.Text);
            cmd.Parameters.AddWithValue("@p17", txtKod3.Text);
            cmd.Parameters.AddWithValue("@p18", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Firma Bilgileri Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();
            Temizle();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
