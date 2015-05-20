using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insertTraceId.Model
{
    public class TraceId
    {
 // [ID] integer NOT NULL PRIMARY KEY AUTOINCREMENT,
 //[TypeID] nvarchar(20) NOT NULL,
 //[TraceID] nvarchar(50) NOT NULL,
 //[State] bit NOT NULL DEFAULT 1,
 //[CreateUserName] nvarchar(20),
 //[TraceType] nvarchar(10) NOT NULL,
 //[CheckTime] datetime
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _TypeID;

        public string TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }
        }
        private string _TraceID;

        public string TraceID
        {
            get { return _TraceID; }
            set { _TraceID = value; }
        }
        private bool _State;

        public bool State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _CreateUserName;

        public string CreateUserName
        {
            get { return _CreateUserName; }
            set { _CreateUserName = value; }
        }
        private string _TraceType;

        public string TraceType
        {
            get { return _TraceType; }
            set { _TraceType = value; }
        }
        private DateTime _CheckTime;

        public DateTime CheckTime
        {
            get { return _CheckTime; }
            set { _CheckTime = value; }
        }
    }
}
