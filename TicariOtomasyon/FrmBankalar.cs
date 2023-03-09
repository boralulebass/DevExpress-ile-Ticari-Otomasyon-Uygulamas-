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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri", bgl.connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
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
        void FirmaListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select FID,FAD from TBL_FIRMALAR", bgl.connection());
            da.Fill(dt);
            cmbFirma.Properties.NullText = "";
            cmbFirma.Properties.ValueMember = "FID";
            cmbFirma.Properties.DisplayMember = "FAD";
            cmbFirma.Properties.DataSource = dt;

        }
        void Temizle()
        {
            txtAd.Text = string.Empty;
            txtHesapNo.Text = string.Empty;
            txtHesapT.Text = string.Empty;
            txtIBAN.Text = string.Empty;
            txtID.Text = string.Empty;
            txtSube.Text = string.Empty;
            txtYetkili.Text = string.Empty;
            cmbFirma.Text = string.Empty;
            cmbIl.Text = string.Empty;
            cmbIlce.Text = string.Empty;
            mskTarih.Text = string.Empty;
            mskTel.Text = string.Empty;
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            Listele();
            Sehirlistesi();
            FirmaListesi();
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Insert into TBL_BANKALAR (BAD,BIL,BILCE,BSUBE,BIBAN,BHESAPNO,BYETKILI,BTELEFON,BTARIH,BHESAPTURU,FIRMAID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)",bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", cmbIl.Text);
            cmd.Parameters.AddWithValue("@p3", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@p4", txtSube.Text);
            cmd.Parameters.AddWithValue("@p5", txtIBAN.Text);
            cmd.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            cmd.Parameters.AddWithValue("@p7", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@p8", mskTel.Text);
            cmd.Parameters.AddWithValue("@p9", mskTarih.Text);
            cmd.Parameters.AddWithValue("@p10", txtHesapT.Text);
            cmd.Parameters.AddWithValue("@p11", cmbFirma.EditValue);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            Listele();
            Temizle();
            MessageBox.Show("Banka Bilgileri Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null) 
            {
                txtAd.Text = dr["Banka Adı"].ToString();
                txtHesapNo.Text = dr["Hesap No"].ToString();
                txtHesapT.Text = dr["Hesap Türü"].ToString();
                txtIBAN.Text = dr["IBAN"].ToString();
                txtID.Text = dr["ID"].ToString();
                txtSube.Text = dr["Şube"].ToString();
                txtYetkili.Text = dr["Yetkili Ad Soyad"].ToString(); ;
                cmbFirma.Text = dr["Firma Adı"].ToString();
                cmbIl.Text = dr["İl"].ToString();
                cmbIlce.Text = dr["İlçe"].ToString();
                mskTarih.Text = dr["Tarih"].ToString();
                mskTel.Text = dr["Telefon"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from TBL_BANKALAR where BID=@p1",bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            Listele();
            Temizle();
            MessageBox.Show("Banka Sistemden Silindi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_BANKALAR set BAD=@p1,BIL=@p2,BILCE=@p3,BSUBE=@p4,BIBAN=@p5,BHESAPNO=@p6,BYETKILI=@p7,BTELEFON=@p8,BTARIH=@p9,BHESAPTURU=@p10,FIRMAID=@p11 where BID=@p12", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", cmbIl.Text);
            cmd.Parameters.AddWithValue("@p3", cmbIlce.Text);
            cmd.Parameters.AddWithValue("@p4", txtSube.Text);
            cmd.Parameters.AddWithValue("@p5", txtIBAN.Text);
            cmd.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            cmd.Parameters.AddWithValue("@p7", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@p8", mskTel.Text);
            cmd.Parameters.AddWithValue("@p9", mskTarih.Text);
            cmd.Parameters.AddWithValue("@p10", txtHesapT.Text);
            cmd.Parameters.AddWithValue("@p11", cmbFirma.EditValue);
            cmd.Parameters.AddWithValue("@p12", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            Listele();
            Temizle();
            MessageBox.Show("Banka Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
