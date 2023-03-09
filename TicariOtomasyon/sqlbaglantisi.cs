using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TicariOtomasyon
{
    public class sqlbaglantisi
    {
        public SqlConnection connection()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-SN51610\SQLEXPRESS;Initial Catalog=DboTicariOtomasyon;Integrated Security=True");
            conn.Open();
            return conn;
        }
    }
}
