using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CmsEntity
{
    public static class MySqlHelper
    {
        /// <summary>
        /// EF SQL 语句返回 dataTable
        /// </summary>
        /// <param name="db">ef数据上下文</param>
        /// <param name="sql">语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static DataTable SqlQueryForDataTatable(this Database db, string sql, DbParameter[] parameters = null)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = db.Connection.ConnectionString;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            if (parameters != null && parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
            }
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
