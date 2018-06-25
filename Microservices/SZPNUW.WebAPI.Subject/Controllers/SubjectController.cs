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
    public class SubjectController : Controller
    {
        private Service service = new Service();
        [HttpGet]
        public IActionResult GetSubjects([FromQuery]string department, int semesterNumber)
        {
            string errorMessage = string.Empty;
            List<SubjectModel> list;
            list = service.GetSubjects(department, semesterNumber, ref errorMessage);
            return Json(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetSubjectById(int id)
        {
            SubjectModel model = service.GetSubjectById(id);
            return Json(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetSubjectBySemesterId(int id)
        {
            List<SubjectModel> subjects = service.GetSubjectsBySemesterId(id);
            return Json(subjects);
        }

        [HttpGet]
        public IActionResult GetSubjectSemester([FromQuery]int subjectId, int semesterId)
        {
            SubjectSemesterModel model = service.GetSubjectSemester(subjectId, semesterId);
            return Json(model);
        }
        [HttpPut]
        public IActionResult UpdateSubject([FromBody]SubjectModel model)
        {
            model.SkipSemesterIdValidation(ModelState);
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if(service.UpdateSubject(model, ref errorMessage))
                    return Json(new Result(true));
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }
        [HttpPost]
        public IActionResult AddSubject([FromBody]SubjectModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.AddSubject(model, ref errorMessage))
                    return Json(new Result(true));
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }
        [HttpPost]
        public IActionResult AddSubjectSemester([FromBody]SubjectSemesterModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.AddSubjectSemester(model, ref errorMessage))
                    return Json(new Result(true));
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }
        [HttpPut]
        public IActionResult UpdateSubjectSemester([FromBody]SubjectSemesterModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.UpdateSubjectSemester(model, ref errorMessage))
                    return Json(new Result(true));
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSubjectSemester(int id)
        {
            string errorMessage = string.Empty;
            if (service.DeleteSubjectSemester(id, ref errorMessage))
                return Json(new Result(true));
            return Json(new Result(errorMessage));
        }
        [HttpGet]
        public IActionResult Test()
        {
            return Json("subject");
        }
    }
}
