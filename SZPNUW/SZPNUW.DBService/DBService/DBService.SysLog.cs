using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SZPNUW.Data;
using SZPNUW.DBService.Model;

namespace SZPNUW.DBService
{
    public partial class DBService
    {
        public List<SysLogModel> GetSysLogs()
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                return context.Syslogs.OrderBy(x => x.Date).Select(x => new SysLogModel { Id = x.Id, Name = x.Name, Details = x.Details, Date = x.Date }).ToList();
            }
        }

        public void InsertSysLog(SysLogModel model)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Syslog sysLog = new Syslog { Name = model.Name, Details = model.Details, Date = model.Date };
                try
                {
                    context.Syslogs.Add(sysLog);
                    context.SaveChanges();
                }
                catch
                {
                }
            }
        }
    }
}
