using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SZPNUW.Base;
using SZPNUW.Base.Consts;
using SZPNUW.Data;
using SZPNUW.DBService;

namespace SZPNUW.WebAPI.Account.Controllers
{
    [Route("[controller]/[action]")]
    public class SysLogController : Controller
    {
        Service service = new Service();

        [HttpGet]
        public IActionResult GetSysLogs()
        {
            List<SysLogModel> sysLogs = service.GetSysLogs();
            return Json(sysLogs);
        }
    }
}
