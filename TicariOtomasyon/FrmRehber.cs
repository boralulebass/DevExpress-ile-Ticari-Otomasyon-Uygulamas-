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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmRehber_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select MAD as 'Ad',MSOYAD as 'Soyad',MTELEFON as 'Telefon',MTELEFON2 as '2. Telefon',MMAIL as 'E-Mail' from TBL_MUSTERILER", bgl.connection());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select FAD as 'Ad',FYETKILISTATU as 'Yetkili Statüsü', FYADSOYAD as 'Yetkili Ad Soyad',FTELEFON1 as 'Telefon', FTELEFON2 as '2. Telefon', FTELEFON3 as '3. Telefon',FMAIL as 'E-Mail', FFAX as 'Fax' from TBL_FIRMALAR", bgl.connection());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frmMail = new FrmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null ) 
            {
                frmMail.mail = dr["E-Mail"].ToString();
            }
            frmMail.Show();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frmMail = new FrmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);

            if (dr != null)
            {
                frmMail.mail = dr["E-Mail"].ToString();
            }
            frmMail.Show();
        }
    }
}
