using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SZPNUW.Base;
using SZPNUW.Data;
using SZPNUW.DBService;

namespace SZPNUW.WebAPI.Subject.Controllers
{
    [Route("[controller]/[action]")]
    public class SemesterController : Controller
    {
        private Service service = new Service();
        [HttpGet]
        public IActionResult GetSemesters()
        {
            List<SemesterModel> list = service.GetSemesters();
            return Json(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetSemestersBySubjectId(int id)
        {
            List<SemesterModel> list = service.GetSemestersBySubjectId(id);
            return Json(list);
        }
        [HttpGet("{id}")]
        public IActionResult GetSemestersByStudentId(int id)
        {
            List<SemesterModel> list = service.GetSemestersByStudentId(id);
            return Json(list);
        }
        [HttpGet("{id}")]
        public IActionResult GetSemesterById(int id)
        {
            SemesterModel model = service.GetSemesterById(id);
            return Json(model);
        }
        [HttpPost]
        public IActionResult AddSemester([FromBody]SemesterModel model)
        {
            if (ModelState.IsValid)
            {
                service.AddSemester(model);
                return Json(new Result(true));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }
        [HttpPut]
        public IActionResult UpdateSemester([FromBody]SemesterModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if(service.UpdateSemester(model, ref errorMessage))
                    return Json(new Result(true));
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }
        [HttpGet]
        public IActionResult GetDepartments()
        {
            List<string> list = service.GetDepartments();
            return Json(list);
        }
        [HttpGet]
        public IActionResult GetYears()
        {
            List<string> list = service.GetYears();
            return Json(list);
        }
        [HttpGet("semestersnum")]
        public IActionResult GetSemestersNum()
        {
            List<int> list = service.GetSemestersNum();
            return Json(list);
        }
    }
}
