using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SZPNUW.Base;
using SZPNUW.Data;
using SZPNUW.DBService;

namespace SZPNUW.WebAPI.Account.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        Service service = new Service();
        #region Common

        [HttpPost]
        public IActionResult Login([FromBody]LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Auth auth = service.Login(model);
                return Json(auth);
            }
            return Json(new Auth(ModelState.GetFirstError()));
        }

        [HttpPut]
        public IActionResult ChangePassword([FromBody]ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.ChangePassword(model, ref errorMessage))
                {
                    return Json(new Result(true));
                }
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }

        #endregion

        #region Students

        [HttpPost]
        public IActionResult RegisterUser([FromBody]StudentModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if(service.RegisterStudent(model, ref errorMessage))
                {
                    return Json(new Result(true));
                }
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentBySemesterId(int id)
        {
            var a =HttpContext.Request;
                List<StudentModel> list = service.GetStudentBySemesterId(id);
                return Json(list);
        }

        [HttpGet]
        public IActionResult GetStudentAverageRating([FromQuery]int studentId, int sectionId)
        {
            float average;
            average = service.GetStudentAverageRating(sectionId, studentId);
            return Json(average);
        }

        [HttpGet]
        public IActionResult GetSectionStudent([FromQuery]int studentId, int sectionId)
        {
            StudentSectionModel studSec = service.GetSectionStudent(sectionId, studentId);
            return Json(studSec);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            StudentModel model = service.GetStudentById(id);
            return new JsonResult(model);
        }

        [HttpPut]
        public IActionResult UpdateStudent([FromBody]StudentModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if(service.UpdateStudent(model, ref errorMessage))
                {
                    return Json(new Result(true));
                }
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }

        [HttpPut]
        public IActionResult UpdateStudentSection([FromBody]StudentSectionModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.UpdateStudentSection(model, ref errorMessage))
                {
                    return Json(new Result(true));
                }
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }

        [HttpPost]
        public IActionResult RewriteStudentsSemester([FromBody]SemestersIdModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                service.RewriteStudentsSemester(model, ref errorMessage);
                if(errorMessage.HasValue())
                    return Json(new Result(errorMessage));
                return Json(new Result(true));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }

        [HttpPut]
        public IActionResult UpdateStudentsSemester([FromBody]SemestersIdModel semestersId)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                service.UpdateStudentsSemester(semestersId, ref errorMessage);
                if (errorMessage.HasValue())
                    return Json(new Result(errorMessage));
                return Json(new Result(true));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }

        [HttpPost]
        public IActionResult RewriteStudentSemester([FromBody]SemestersIdModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                service.RewriteStudentSemester(model.StudentId, model.SemesterId, ref errorMessage);
                if (errorMessage.HasValue())
                    return Json(new Result(errorMessage));
                return Json(new Result(true));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }

        [HttpPut]
        public IActionResult UpdateStudentSemester([FromBody]SemestersIdModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                service.UpdateStudentSemester(model.StudentId, model.SemesterId, model.NewSemesterId, ref errorMessage);
                if (errorMessage.HasValue())
                    return Json(new Result(errorMessage));
                return Json(new Result(true));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSemester(int id)
        {
            string errorMessage = string.Empty;
            service.DeleteStudentsSemester(id, ref errorMessage);
            if (errorMessage.HasValue())
                return Json(new Result(errorMessage));
            return Json(new Result(true));
        }

        [HttpDelete]
        public IActionResult DeleteSemester([FromQuery] int studentId, int semesterId)
        {
            string errorMessage = string.Empty;
            service.DeleteStudentSemester(studentId, semesterId, ref errorMessage);
            if (errorMessage.HasValue())
                return Json(new Result(errorMessage));
            return Json(new Result(true));
        }

        #endregion

        #region Instructors

        [HttpPost]
        public IActionResult RegisterInstructor([FromBody]InstructorModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.RegisterInstructor(model, ref errorMessage))
                {
                    return Json(new Result(true));
                }
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }

        [HttpGet]
        public IActionResult GetInstructors()
        {
            List<InstructorModel> instructors = service.GetInstructors();
            return Json(instructors);
        }
        [HttpGet("{id}")]
        public IActionResult GetInstructor(int id)
        {
            InstructorModel model = service.GetInstructorById(id);
            return Json(model);
        }
        [HttpPut]
        public IActionResult UpdateInstructor([FromBody]InstructorModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.UpdateInstructor(model, ref errorMessage))
                {
                    return Json(new Result(true));
                }
                return Json(new Result(errorMessage));
            }
            return Json(new Result(ModelState.GetFirstError()));
        }
        [HttpGet]
        public IActionResult Test()
        {
            return Json(SecurityService.GetSHA256Hash("qwerty"));
        }
        #endregion
    }
}
