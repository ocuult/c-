using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


using System.Configuration;namespace insertTraceId.DAL
{
    public class SqlHelper
    {
        //获取连接字符串
        private static readonly string str = ConfigurationManager.ConnectionStrings["conStr_sql"].ConnectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] param) //对连接执行 Transact-SQL 语句并返回受影响的行数。
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns>首行首列</returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] param)  //执行查询，并返回查询所返回的结果集中第一行的第一列。 忽略其他列或行。
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// 查询表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns>返回查询结果-table</returns>
        public static DataTable ExecuteTable(string sql, params SqlParameter[] param)//返回一个table
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, str))
            {
                if (param != null)
                {
                    sda.SelectCommand.Parameters.AddRange(param);
                }
                sda.Fill(dt);
            }
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] param) //将 CommandText 发送到 Connection 并生成一个 SqlDataReader
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    try
                    {
                        con.Open();
                        return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        con.Dispose();
                        throw ex;
                    }
                }
            }
        }
    }
}
