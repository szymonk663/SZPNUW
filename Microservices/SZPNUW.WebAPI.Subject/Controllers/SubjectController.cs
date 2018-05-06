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
            return new JsonResult(list, JsonSettings.DefaultJsonSettings);
        }

        [HttpGet("{id}")]
        public IActionResult GetSubjectById(int id)
        {
            SubjectModel model = service.GetSubjectById(id);
            return new JsonResult(model, JsonSettings.DefaultJsonSettings);
        }

        [HttpGet("{id}")]
        public IActionResult GetSubjectBySemesterId(int id)
        {
            List<SubjectModel> subjects = service.GetSubjectsBySemesterId(id);
            return new JsonResult(subjects, JsonSettings.DefaultJsonSettings);
        }

        [HttpGet]
        public IActionResult GetSubjectSemester([FromQuery]int idSubject, int idSemester)
        {
            SubjectSemesterModel model = service.GetSubjectSemester(idSubject, idSemester);
            return new JsonResult(model, JsonSettings.DefaultJsonSettings);
        }
        [HttpPut]
        public IActionResult UpdateSubject([FromBody]SubjectModel model)
        {
            if(ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if(service.UpdateSubject(model, ref errorMessage))
                    return new JsonResult(new Result(true), JsonSettings.DefaultJsonSettings);
                return new JsonResult(new Result(errorMessage), JsonSettings.DefaultJsonSettings);
            }
            return new JsonResult(new Result(ModelState.GetFirstError()), JsonSettings.DefaultJsonSettings);
        }
        [HttpPost]
        public IActionResult AddSubject([FromBody]SubjectModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.UpdateSubject(model, ref errorMessage))
                    return new JsonResult(new Result(true), JsonSettings.DefaultJsonSettings);
                return new JsonResult(new Result(errorMessage), JsonSettings.DefaultJsonSettings);
            }
            return new JsonResult(new Result(ModelState.GetFirstError()), JsonSettings.DefaultJsonSettings);
        }
        [HttpPost]
        public IActionResult AddSubjectSemester([FromBody]SubjectSemesterModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.AddSubjectSemester(model, ref errorMessage))
                    return new JsonResult(new Result(true), JsonSettings.DefaultJsonSettings);
                return new JsonResult(new Result(errorMessage), JsonSettings.DefaultJsonSettings);
            }
            return new JsonResult(new Result(ModelState.GetFirstError()), JsonSettings.DefaultJsonSettings);
        }
        [HttpPut]
        public IActionResult UpdateSemester([FromBody]SubjectSemesterModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.UpdateSubjectSemester(model, ref errorMessage))
                    return new JsonResult(new Result(true), JsonSettings.DefaultJsonSettings);
                return new JsonResult(new Result(errorMessage), JsonSettings.DefaultJsonSettings);
            }
            return new JsonResult(new Result(ModelState.GetFirstError()), JsonSettings.DefaultJsonSettings);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSubjectSemester(int id)
        {
            string errorMessage = string.Empty;
            if (service.DeleteSubjectSemester(id, ref errorMessage))
                return new JsonResult(new Result(true), JsonSettings.DefaultJsonSettings);
            return new JsonResult(new Result(errorMessage), JsonSettings.DefaultJsonSettings);
        }
    }
}
