using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;

namespace insertTraceId.DAL
{
    public class SqliteHelper
    {
        //获取连接字符串
        private static readonly string str = ConfigurationManager.ConnectionStrings["conStr_sqlite"].ConnectionString;
        /// <summary>
        /// 对连接执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="param">SQL参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, params SQLiteParameter[] param) //对连接执行 Transact-SQL 语句并返回受影响的行数。
        {
            using (SQLiteConnection con = new SQLiteConnection(str))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
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
        public static object ExecuteScalar(string sql, params SQLiteParameter[] param)  //执行查询，并返回查询所返回的结果集中第一行的第一列。 忽略其他列或行。
        {
            using (SQLiteConnection con = new SQLiteConnection(str))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
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
        public static DataTable ExecuteTable(string sql, params SQLiteParameter[] param)//返回一个table
        {
            DataTable dt = new DataTable();
            using (SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, str))
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
        public static SQLiteDataReader ExecuteReader(string sql, params SQLiteParameter[] param) //将 CommandText 发送到 Connection 并生成一个 SqlDataReader
        {
            using (SQLiteConnection con = new SQLiteConnection(str))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
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


        /// <summary>  
        /// 执行多条SQL语句，实现数据库事务。  
        /// </summary>  
        /// <param name="SQLStringList">多条SQL语句</param>       
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            using( SQLiteConnection conn = new SQLiteConnection(str))
            {
                conn.Open(); //打开数据库连接  
                SQLiteCommand cmd = new SQLiteCommand(); //创建SqlCommand命令  
                cmd.Connection = conn; //设置命令连接  
                SQLiteTransaction tx = conn.BeginTransaction();//开始事务  
                cmd.Transaction = tx;//设置执行命令的事务  
                try
                {
                    int count = 0;//定义int类型变量，存放该函数返回值  
                    for (int i = 0; i < SQLStringList.Count; i++)//循环传入的sql语句  
                    {
                        string strsql = SQLStringList[i]; //第n条sql语句  
                        if (strsql.Trim().Length > 1) //如果第n条sql语句不为空  
                        {
                            cmd.CommandText = strsql; //设置执行命令的sql语句  
                            count += cmd.ExecuteNonQuery(); //调用执行增删改sql语句的函数ExecuteNonQuery(),执行sql语句  
                        }
                    }
                    tx.Commit();//提交事务  
                    return count;//返回受影响行数  
                }
                catch
                {
                    tx.Rollback();
                    return 0;
                }
            }
        }  
    }
}
