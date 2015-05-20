using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using insertTraceId.DAL;
using insertTraceId.Model;

namespace insertTraceId.BLL
{
    public class TraceIdBLL
    {
        TraceIdDAL dal = new TraceIdDAL();
        public List<TraceId> GetTraceId(string typeID,  bool state,string traceType)
        {
            return dal.GetTraceId(typeID,state,traceType);
        }
    }
}
