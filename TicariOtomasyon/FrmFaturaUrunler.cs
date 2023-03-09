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
    public partial class FrmFaturaUrunler : Form
    {
        public FrmFaturaUrunler()
        {
            InitializeComponent();
        }
        public string id;
        sqlbaglantisi bgl = new sqlbaglantisi();
        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select FDURUNID as 'Ürün ID',FDUAD as 'Ürün Adı',FDUMIKTAR as 'Miktar',FDUFIYAT as 'Fiyat',FDTUTAR as 'Tutar', FDFATURAID as 'Fatura ID' from TBL_FATURADETAY where FDFATURAID='" + id + "'", bgl.connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmFaturaUrunler_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDuzenleme fr = new FrmFaturaUrunDuzenleme();
            DataRow dr= gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null ) 
            {
                fr.urunid = dr["Ürün ID"].ToString(); 
            }
            fr.Show();
        }
    }
}
