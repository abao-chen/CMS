using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using CmsUtils;

namespace CmsWeb.API
{
    /// <summary>
    /// TicketsApi 的摘要说明
    /// </summary>
    public class TicketsApi : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string method = HttpContext.Current.Request.Form["Method"];
            switch (method)
            {
                case "TicketsProcess":
                    TicketsProcess();
                    break;
                case "TicketsTop":
                    TicketsTop();
                    break;
                default:
                    break;
            }
        }

        private void TicketsTop()
        {
            DataTable dt = GetDataTable(@"select UserCode,UserName,max(Tickets) Tickets from TB_TouPiaoLog group by UserCode,UserName
                                          order by max(Tickets) DESC limit 20 ");
            List<string> dataList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                dataList.Add(dr["UserName"] + "|" + dr["Tickets"]);
            }
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(dataList.ToJson());
            HttpContext.Current.Response.End();
        }

        private void TicketsProcess()
        {
            string userCode = HttpContext.Current.Request.Form["UserCode"];
            DataTable dt = GetDataTable(string.Format(@"SELECT
	                                                        *
                                                        FROM
	                                                        (
		                                                        SELECT
			                                                        UserCode,
			                                                        UserName,
			                                                        max(Tickets) Tickets,
			                                                        DATE_FORMAT(CreateTime, '%Y-%m-%d') TicketsTime
		                                                        FROM
			                                                        TB_TouPiaoLog
		                                                        WHERE
			                                                        UserCode = '{0}' or UserName='{0}'
		                                                        GROUP BY
			                                                        UserCode,
			                                                        UserName,
			                                                        DATE_FORMAT(CreateTime, '%Y-%m-%d')
		                                                        ORDER BY
			                                                        CreateTime DESC
		                                                        LIMIT 50
	                                                        ) t
                                                        ORDER BY
	                                                        TicketsTime ASC", userCode));
            List<string> dataList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                dataList.Add(dr["TicketsTime"] + "|" + dr["Tickets"]);
            }
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(dataList.ToJson());
            HttpContext.Current.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public static DataTable GetDataTable(string sql)
        {
            MySqlConnection conn = new MySqlConnection("Database=mytemp;Data Source=139.196.75.172;Port=3306;User Id=root;Password=soft*1023;CharSet=utf8;");
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public static int ExcuteSql(string sql)
        {
            MySqlConnection conn = new MySqlConnection("Database=mytemp;Data Source=139.196.75.172;Port=3306;User Id=root;Password=soft*1023;CharSet=utf8;");
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandTimeout = 1800;
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}