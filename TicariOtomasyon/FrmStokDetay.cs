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
    public partial class FrmStokDetay : Form
    {
        public FrmStokDetay()
        {
            InitializeComponent();
        }
        public string ad;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmStokDetay_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select UID as 'ID', UAD as 'Ürün Adı', UMARKA as 'Marka', UMODEL as 'Model', UYIL as 'Yıl', UADET as 'Adet', UALISFIYAT as 'Alış Fiyatı', USATISFIYAT as 'Satış Fiyatı',UDETAY as 'Detay' from TBL_URUNLER where UAD='"+ad+"'",bgl.connection());
            adapter.Fill(dt);
            gridControl1.DataSource = dt;
        }
    }
}
