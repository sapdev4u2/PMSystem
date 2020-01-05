using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PMOSYS
{
    class Con_Cls
    {
       public SqlConnection con = new SqlConnection(@"Data Source=12802D204945-01\SQLEXPRESS;" +
                                                "Initial Catalog=ProjectManagmentSystemDB;" +
                                                "Integrated Security=SSPI;");



        public void OpenConnection()
        {
            if (con.State == ConnectionState.Closed)
            {
               con.Open();
            }
        }


        public void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
