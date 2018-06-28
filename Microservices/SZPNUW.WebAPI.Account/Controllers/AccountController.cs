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
    public class AccountController : Controller
    {
        Service service = new Service();

        public ISession Session
        {
            get
            {
                return this.HttpContext.Session;
            }
        }

        public UserModel CurrentUserSessionData
        {
            get
            {
                return HttpContext.Session.GetItem<UserModel>();
            }
        }
        #region Common

        [HttpPost]
        public IActionResult Login([FromBody]LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                UserModel user = service.Login(model, ref errorMessage);
                if (!errorMessage.HasValue())
                {
                    LoginUser(user);
                    Auth auth = new Auth(true) { Id = user.UserId, UserType = user.UserType };
                    return Json(auth);
                }
                return Json(new Auth(errorMessage));
            }
            return Json(new Auth(ModelState.GetFirstError()));
        }

        private void LoginUser(UserModel user)
        {
            Session.AddItem<UserModel>(user);
            string userDataString = string.Join(";", user.UserId, user.UserType);
            List<Claim> userClaims = new List<Claim>
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            };
            ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "local"));
            AuthenticationProperties props = new AuthenticationProperties
            {
                IsPersistent = false,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
            };
            HttpContext.SignInAsync(Consts.AuthenticateScheme, principal, props);
        }

        [HttpPut]
        public IActionResult ChangePassword([FromBody]ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.ChangePassword(CurrentUserSessionData.UserId, model, ref errorMessage))
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

        [HttpGet]
        public IActionResult GetAuth()
        {
            UserModel user = HttpContext.Session.GetItem<UserModel>();
            return Json(user);
        }

        [HttpGet]
        public void LogOut()
        {
            HttpContext.SignOutAsync(Consts.AuthenticateScheme);
            Session.Clear();
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentsBySemesterId(int id)
        {
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
                service.RewriteStudentSemester(model.StudentId, model.SemesterId.Value, ref errorMessage);
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
                service.UpdateStudentSemester(model.StudentId, model.SemesterId.Value, model.NewSemesterId.Value, ref errorMessage);
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

        [HttpGet]
        public IActionResult GetCurrentInstructor()
        {
            InstructorModel model = service.GetInstructorByUserId(CurrentUserSessionData.UserId);
            return Json(model);
        }
        [HttpPut]
        public IActionResult UpdateInstructor([FromBody]InstructorModel model)
        {
            model.SkipPasswordValidation(ModelState);
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
