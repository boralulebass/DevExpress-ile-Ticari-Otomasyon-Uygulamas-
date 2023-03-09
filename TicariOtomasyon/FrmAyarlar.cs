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
using DevExpress.XtraDiagram.Bars;

namespace TicariOtomasyon
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from TBL_ADMIN", bgl.connection());
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }
        void temizle()
        {
            textEdit1.Text = string.Empty;
            textEdit2.Text = string.Empty;
        }
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Insert into TBL_ADMIN (KULLANICIAD, SIFRE) values (@p1,@p2)", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", textEdit1.Text);
            cmd.Parameters.AddWithValue("@p2", textEdit2.Text);
            cmd.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Giriş Bilgileri Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null) 
            {
                textEdit1.Text = dr["KULLANICIAD"].ToString();
                textEdit2.Text = dr["SIFRE"].ToString();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("update TBL_ADMIN set SIFRE=@p2 where KULLANICIAD=@p1", bgl.connection());
            cmd2.Parameters.AddWithValue("@p1", textEdit1.Text);
            cmd2.Parameters.AddWithValue("@p2", textEdit2.Text);
            cmd2.ExecuteNonQuery();
            bgl.connection().Close();
            MessageBox.Show("Giriş Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
