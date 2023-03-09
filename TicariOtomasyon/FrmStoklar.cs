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
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.Utils.FormShadow;

namespace TicariOtomasyon
{
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select UAD as 'Ürün Adı',Sum(UADET) as 'Ürün Sayısı' from TBL_URUNLER group by UAD",bgl.connection());
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            gridControl1.DataSource = dt;

            SqlCommand cmd = new SqlCommand("Select UAD as 'Ürün Adı',Sum(UADET) as 'Ürün Sayısı' from TBL_URUNLER group by UAD", bgl.connection());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            bgl.connection().Close();

            SqlCommand cmd2 = new SqlCommand("Select FIL as 'İl',Count(*) as 'Firma Sayısı' From TBL_FIRMALAR Group By FIL", bgl.connection());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read()) 
            {
                chartControl2.Series["Series 2"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            bgl.connection().Close();

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay fr = new FrmStokDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.ad = dr["Ürün Adı"].ToString();
            }
            fr.Show();
        }
    }
    
}
