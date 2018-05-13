using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SZPNUW.Base;
using SZPNUW.Base.Resources;
using SZPNUW.Data;
using SZPNUW.DBService;

namespace SZPNUW.WebAPI.Sections.Controllers
{
    [Route("[controller]/[action]")]
    public class SectionController : Controller
    {
        private Service service = new Service();
        [HttpGet]
        public IActionResult GetSectionsStudents([FromQuery]int subjectId, int semesterId)
        {
            List<SectionStudentsModel> list = service.GetSectionsStudents(subjectId, semesterId);
            return Json(list);
        }
        [HttpGet]
        public IActionResult GetSectionStudentId([FromQuery]int sectionId, int studentId)
        {
            int? id = service.GetSectionStudentId(sectionId, studentId);
            return Json(id);
        }
        [HttpGet]
        public IActionResult StudentInSection([FromQuery]int sectionId, int studentId)
        {
            bool response = service.StudentInSection(sectionId, studentId);
            return new JsonResult(response);
        }
        [HttpPost]
        public IActionResult AddSections([FromBody]SectionsCreateModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                int? count = service.AddSections(model, ref errorMessage);
                if (count.HasValue)
                {
                    Result result = new Result(true) { PortalMessages = String.Format(PortalMessages.CreatedSectionsSuccesfull, count.Value) };
                    return Json(result);
                }
                return Json(new Result(errorMessage));  
            }
            return Json(new Result(ModelState.GetFirstError()));
        }
        [HttpPut]
        public IActionResult UpdateSection([FromBody]SectionModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if(service.UpdateSection(model, ref errorMessage))
                    return Json(new Result(true));
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSection(int id)
        {
            string errorMessage = string.Empty;
            if(service.DeleteSection(id, ref errorMessage))
                return Json(new Result(true));
            return Json(new Result(errorMessage));
        }
        [HttpPost]
        public IActionResult AddStudentToSection([FromBody]StudentSectionModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if(service.AddStudentToSection(model, ref errorMessage))
                    return Json(new Result(true));
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }
        [HttpDelete]
        public IActionResult DeleteStudentFromSection([FromQuery]int studentId, int sectionId)
        {
            string errorMessage = string.Empty;
            if(service.DeleteStudentFromSection(studentId, sectionId, ref errorMessage))
                return Json(new Result(true));
            return Json(new Result(errorMessage));
        }
    }
}
