using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Database
{
    public class Repository
    {
        public bool ExecuteDml(SqlCommand cmd)
        {
            SqlConnection DBCon = new SqlConnection("Data Source = localhost\\SQLEXPRESS; Initial Catalog = TRUE; Integrated Security = True");
            cmd.Connection = DBCon;
            try
            {
                DBCon.Open();
                cmd.ExecuteNonQuery();
                DBCon.Close();
                DBCon.Dispose();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public DataTable ExecuteRead(string query)
        {
            SqlConnection DBCon = new SqlConnection("Data Source = localhost\\SQLEXPRESS; Initial Catalog = TRUE; Integrated Security = True");
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand(query, DBCon);
                DBCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                DBCon.Close();
                DBCon.Dispose();
                return dataTable;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

    }
    
}
