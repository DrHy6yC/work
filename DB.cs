using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_wpf
{
    class DB
    {

        SqlConnection connection = new SqlConnection("Data Source=DRDI;Initial Catalog=wpf_sql;Integrated Security=True;");

        public void openConn()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            
        }
        public void closeConn()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();

        }
    }
}
