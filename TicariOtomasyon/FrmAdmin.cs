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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void simpleButton1_MouseHover(object sender, EventArgs e)
        {
            simpleButton1.BackColor = Color.Chartreuse;
        }

        private void simpleButton1_MouseLeave(object sender, EventArgs e)
        {
            simpleButton1.BackColor = SystemColors.Control;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from TBL_ADMIN where KULLANICIAD=@p1 and SIFRE=@p2", bgl.connection());
            cmd.Parameters.AddWithValue("@p1", txtKullanici.Text);
            cmd.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read()) 
            {
                MessageBox.Show("Hoşgeldiniz", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmAnaModül fr = new FrmAnaModül();
                fr.kullanici = txtKullanici.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            bgl.connection().Close();
        }

    }
}
