using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using insertTraceId.Model;

namespace insertTraceId.DAL
{
    public class TraceIdDAL
    {
        /// <summary>
        /// 根据跟踪号属性查询结果转换成象集合
        /// </summary>
        /// <param name="typeID">邮递方式</param>
        /// <param name="state">可用状态，0--可用 1--不可用</param>
        /// <param name="traceType">跟踪号分类，1--棒谷 2--飞特 3--公用</param>
        /// <returns>符合条件跟踪号的集合</returns>
        public List<TraceId> GetTraceId(string typeID,  bool state,string traceType)
        {
            string sql = "select * from Test where typeid=@typeID and state=@state and tracetype=@traceType;";
            SQLiteParameter[] param={
                                        new SQLiteParameter("@typeID",typeID),
                                        new SQLiteParameter("@state",state),
                                        new SQLiteParameter("@traceType",traceType),
                                    };
            DataTable dt= SqliteHelper.ExecuteTable(sql, param);
            List<TraceId> list =new List<TraceId>();
            if(dt.Rows.Count>0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    TraceId traceid = toTraceId(item);
                    list.Add(traceid);
                }
            }
            return list;
        }
        /// <summary>
        /// 数据集合转换成对象返回
        /// </summary>
        /// <param name="item"></param>
        /// <returns>返回一个对象</returns>
        private TraceId toTraceId(DataRow item)
        {
            TraceId traceid = new TraceId();
            // [ID] integer NOT NULL PRIMARY KEY AUTOINCREMENT,
            //[TypeID] nvarchar(20) NOT NULL,
            //[TraceID] nvarchar(50) NOT NULL,
            //[State] bit NOT NULL DEFAULT 1,
            //[CreateUserName] nvarchar(20),
            //[TraceType] nvarchar(10) NOT NULL,
            //[CheckTime] datetime
            traceid.ID = Convert.ToInt32(item["ID"]);
            traceid.TypeID = item["TypeID"].ToString();
            traceid.TraceID = item["TraceID"].ToString();
            traceid.State = Convert.ToBoolean(item["State"]);
            traceid.CreateUserName = item["CreateUserName"].ToString();
            traceid.TraceType = item["TraceType"].ToString();
            traceid.CheckTime = Convert.ToDateTime(item["CheckTime"]);
            return traceid;
            //throw new NotImplementedException();
        }
    }
}
