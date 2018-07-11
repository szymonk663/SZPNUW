using System;
using System.Collections.Generic;
using System.Text;
using SZPNUW.Data;

namespace SZPNUW.DBService
{
    public partial class Service
    {
        public List<SysLogModel> GetSysLogs()
        {
            return service.GetSysLogs();
        }

        public void InsertSysLog(SysLogModel model)
        {
            service.InsertSysLog(model);
        }

    }
}
