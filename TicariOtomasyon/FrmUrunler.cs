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
using DevExpress.XtraDashboardLayout;

namespace TicariOtomasyon
{
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl= new sqlbaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select UID as 'ID', UAD as 'Ürün Adı', UMARKA as 'Marka', UMODEL as 'Model', UYIL as 'Yıl', UADET as 'Adet', UALISFIYAT as 'Alış Fiyatı', USATISFIYAT as 'Satış Fiyatı',UDETAY as 'Detay' From TBL_URUNLER", bgl.connection());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        void temizle()
        {
            txtAlis.Text = string.Empty;
            txtDetay.Text = string.Empty;
            txtID.Text = string.Empty;
            txtKategori.Text = string.Empty;
            txtMarka.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtSatis.Text = string.Empty;
            mskYıl.Text = string.Empty;
            numAdet.Text = "0";
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Verileri kaydetme
            SqlCommand cmd = new SqlCommand("insert into TBL_URUNLER (UAD,UMARKA,UMODEL,UYIL,UADET,UALISFIYAT,USATISFIYAT,UDETAY) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtKategori.Text);
            cmd.Parameters.AddWithValue("@p2", txtMarka.Text);
            cmd.Parameters.AddWithValue("@p3", txtModel.Text);
            cmd.Parameters.AddWithValue("@p4", mskYıl.Text);
            cmd.Parameters.AddWithValue("@p5", int.Parse((numAdet.Value).ToString()));
            cmd.Parameters.AddWithValue("@p6", decimal.Parse(txtAlis.Text));
            cmd.Parameters.AddWithValue("@p7", decimal.Parse(txtSatis.Text));
            cmd.Parameters.AddWithValue("@p8", txtDetay.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Ürün sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmdsil = new SqlCommand("Delete From TBL_URUNLER where UID=@p1", bgl.connection());
            cmdsil.Parameters.AddWithValue("@p1", int.Parse(txtID.Text));
            cmdsil.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Ürün silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtID.Text = dr["ID"].ToString();
            txtKategori.Text = dr["Ürün Adı"].ToString();
            txtMarka.Text = dr["Marka"].ToString();
            txtModel.Text = dr["Model"].ToString();
            numAdet.Value = int.Parse(dr["Adet"].ToString());
            txtAlis.Text = dr["Alış Fiyatı"].ToString();
            txtSatis.Text = dr["Satış Fiyatı"].ToString();
            mskYıl.Text = dr["Yıl"].ToString();
            txtDetay.Text = dr["Detay"].ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmdgunc = new SqlCommand("Update TBL_URUNLER set UAD=@P1, UMARKA=@P2, UMODEL=@P3, UYIL=@P4, UADET=@P5, UALISFIYAT=@P6, USATISFIYAT=@P7, UDETAY=@P8 where UID=@P9", bgl.connection());
            cmdgunc.Parameters.AddWithValue("@P1", txtKategori.Text);
            cmdgunc.Parameters.AddWithValue("@P2", txtMarka.Text);
            cmdgunc.Parameters.AddWithValue("@P3", txtModel.Text);
            cmdgunc.Parameters.AddWithValue("@P4", mskYıl.Text);
            cmdgunc.Parameters.AddWithValue("@P5", int.Parse((numAdet.Value).ToString()));
            cmdgunc.Parameters.AddWithValue("@P6", decimal.Parse(txtAlis.Text));
            cmdgunc.Parameters.AddWithValue("@P7", decimal.Parse(txtSatis.Text));
            cmdgunc.Parameters.AddWithValue("@P8", txtDetay.Text);
            cmdgunc.Parameters.AddWithValue("@P9", txtID.Text);
            cmdgunc.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Ürün bilgisi güncellendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
            listele();
        }
    }
}
