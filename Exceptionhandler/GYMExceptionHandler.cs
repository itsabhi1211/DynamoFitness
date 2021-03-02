using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace GymDataAccess.Exceptionhandler
{
  public  class GYMExceptionHandler:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            try
            {
                string exceptionmsg = filterContext.Exception.Message ?? string.Empty;
                string exceptionstacktrack = filterContext.Exception.StackTrace ?? string.Empty;
                string controllername = filterContext.RouteData.Values["controller"].ToString() ?? string.Empty;
                string actionname = filterContext.RouteData.Values["action"].ToString() ?? string.Empty;
                string currentarea = filterContext.RouteData.DataTokens["area"] !=null ? filterContext.RouteData.DataTokens["area"].ToString():string.Empty;

                string connectionstring = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
                var conn = new SqlConnection(connectionstring);
                conn.Open();
                var cmd = new SqlCommand("[SP_ExceptionLogger]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@servertype", "localhost");
                cmd.Parameters.AddWithValue("@exceptionmsg", exceptionmsg);
                cmd.Parameters.AddWithValue("@exceptionstacktrack", exceptionstacktrack);
                cmd.Parameters.AddWithValue("@controllername", controllername);
                cmd.Parameters.AddWithValue("@actionname", actionname);
                cmd.Parameters.AddWithValue("@currentarea", currentarea);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectResult("/Home/Error");    
        }
    }
}
