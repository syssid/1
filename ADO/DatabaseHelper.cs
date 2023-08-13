using System.Data;
using System.Data.SqlClient;

namespace _1.ADO
{
    public class DatabaseHelper
    {
        private readonly string cs;
        public DatabaseHelper(string cs)
        {
            this.cs = cs;
        }
        public DataTable ExecSPReader(string sp, SqlParameter[] param)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    con.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                    return dt;
                }
            }
        }
        public int ExecNonQuery(string sp, SqlParameter[] param = null)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    con.Open();
                    int retval = cmd.ExecuteNonQuery();
                    return retval;
                }
            }
        }
    }
}