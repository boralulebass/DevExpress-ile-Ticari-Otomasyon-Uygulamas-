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
using System.Data.Common;

namespace TicariOtomasyon
{
    public partial class FrmHareketler : Form
    {
        public FrmHareketler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void Firmalistesi()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute FirmaHareketler", bgl.connection());
            da.Fill(dataTable);
            gridControl2.DataSource = dataTable;
        }
        void Musterilistesi()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketler",bgl.connection());
            da.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }
        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            Firmalistesi();
            Musterilistesi();
        }
    }
}
