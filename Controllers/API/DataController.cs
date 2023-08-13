using _1.ADO;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Web;

namespace _1.Controllers.API
{
    public class DataController : ApiController
    {
        static string cs = ConfigurationManager.ConnectionStrings["One"].ConnectionString;

        [HttpGet]
        public HttpResponseMessage Get()
        {
            DatabaseHelper obj = new DatabaseHelper(cs);

            SqlParameter[] param = new SqlParameter[]
       {
            new SqlParameter("@ID", ""),
            new SqlParameter("@NAME", ""),
            new SqlParameter("@EMAIL", ""),
            new SqlParameter("@PHONE", ""),
            new SqlParameter("@SALARY", DBNull.Value),
            new SqlParameter("@OPERATION",  1),
       };
            DataTable dt = obj.ExecSPReader("SP_MVC", param);
            if (dt.Rows.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, dt);
            }
            else
                return Request.CreateResponse(HttpStatusCode.OK, "NO DATA FOUND");
        }

        [HttpGet]
        public HttpResponseMessage GetLogin(string id, string pass)
        {
            DatabaseHelper obj = new DatabaseHelper(cs);

            SqlParameter[] param = new SqlParameter[]
       {
            new SqlParameter("@id", id),
            new SqlParameter("@pass", pass)
       };
            DataTable dt = obj.ExecSPReader("sp_api_login", param);
            if (dt.Rows.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, dt);
            }
            else
                return Request.CreateResponse(HttpStatusCode.OK, "NO DATA FOUND");
        }

        [HttpPost]
        public string SaveData(string name, string email, string phone, decimal salary)
        {
            DatabaseHelper obj = new DatabaseHelper(cs);

            SqlParameter[] param = new SqlParameter[]
       {
            new SqlParameter("@ID", ""),
            new SqlParameter("@NAME", name),
            new SqlParameter("@EMAIL", email),
            new SqlParameter("@PHONE", phone),
            new SqlParameter("@SALARY", salary),
            new SqlParameter("@OPERATION",  2),
       };
            int retval = -1;
            retval = obj.ExecNonQuery("SP_MVC", param);
            if (retval != -1)
            {
                return "Data Saved Successfully";
            }
            else
                return "Failed To Save Data";

        }
        [HttpPut]
        public string UpdateData(int id, string name, string email, string phn, decimal salary)
        {
            DatabaseHelper obj = new DatabaseHelper(cs);

            SqlParameter[] param = new SqlParameter[]
       {
            new SqlParameter("@ID", id),
            new SqlParameter("@NAME", name),
            new SqlParameter("@EMAIL", email),
            new SqlParameter("@PHONE", phn),
            new SqlParameter("@SALARY", salary),
            new SqlParameter("@OPERATION",  3),
       };
            int retval = -1;
            retval = obj.ExecNonQuery("SP_MVC", param);
            if (retval != -1)
            {
                return "Updated SUcessfully";
            }
            else
                return "Failed Update Data";

        }
        [HttpDelete]
        public string Delete(int id)
        {
            DatabaseHelper obj = new DatabaseHelper(cs);

            SqlParameter[] param = new SqlParameter[]
       {
            new SqlParameter("@ID", id),
            new SqlParameter("@NAME", ""),
            new SqlParameter("@EMAIL", ""),
            new SqlParameter("@PHONE", ""),
            new SqlParameter("@SALARY", DBNull.Value),
            new SqlParameter("@OPERATION",  4),
       };
            int retval = -1;
            retval = obj.ExecNonQuery("SP_MVC", param);
            if (retval != -1)
            {
                return "Deleted SUcessfully";
            }
            else
                return "Failed Delete Data";
        }

        [Route("api/Data/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpreq = HttpContext.Current.Request;
                var PostFile = httpreq.Files[0];

                string filename = PostFile.FileName;
                var path = HttpContext.Current.Server.MapPath("~/Photos/" + filename);
                PostFile.SaveAs(path);

                return filename;
            }
            catch (Exception)
            {
                return "No File Found";
            }
        }
    }
}
