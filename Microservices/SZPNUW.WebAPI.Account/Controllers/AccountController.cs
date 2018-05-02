using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SZPNUW.Base;
using SZPNUW.Data;

namespace SZPNUW.WebAPI.Account.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult RegisterStudent(StudentModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return Json(new Result(ModelState.GetFirstError()));
        }
    }
}
