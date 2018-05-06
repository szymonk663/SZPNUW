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
            List<Semester> list = querySemester.GetSemesters();
            if (list.Count != 0)
            {
                Response.StatusCode = 200;
                return new JsonResult(list, JsonSettings.DefaultJsonSettings);
            }
            Response.StatusCode = 204;
            return StatusCode(204);
        }

        [HttpGet("semesters/{id}")]
        public IActionResult GetSemestersBySubjectId(int id)
        {
            List<Semester> list = querySemester.GetSemestersBySubjectId(id);
            if (list.Count != 0)
            {
                Response.StatusCode = 200;
                return new JsonResult(list, JsonSettings.DefaultJsonSettings);
            }
            Response.StatusCode = 204;
            return StatusCode(204);
        }
        [HttpGet("semesters/student/{id}")]
        public IActionResult GetSemestersByStudentId(int id)
        {
            List<Semester> list = querySemester.GetSemestersByStudentId(id);
            if (list.Count != 0)
            {
                Response.StatusCode = 200;
                return new JsonResult(list, JsonSettings.DefaultJsonSettings);
            }
            Response.StatusCode = 204;
            return StatusCode(204);
        }
        [HttpGet("{id}")]
        public IActionResult GetSemesterById(int id)
        {
            try
            {
                Semester sv = querySemester.GetSemesterById(id);
                Response.StatusCode = 200;
                return new JsonResult(sv, JsonSettings.DefaultJsonSettings);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return new JsonResult(ex.Message);
            }
        }
        [HttpPost("new")]
        public IActionResult AddNew([FromBody]Semester semester)
        {
            if (semester != null)
            {
                try
                {
                    querySemester.AddSemester(semester);
                    return StatusCode(201);
                }
                catch (Exception e)
                {
                    Response.StatusCode = 400;
                    return new JsonResult(e.Message);
                }
            }
            Response.StatusCode = 400;
            return new JsonResult("Formularz został błędnie wypełniony.");
        }
        [HttpPut("update")]
        public IActionResult Update([FromBody]Semester semester)
        {
            if (semester != null)
            {
                try
                {
                    querySemester.UpdateSemester(semester);
                    return StatusCode(201);
                }
                catch (Exception e)
                {
                    Response.StatusCode = 400;
                    return new JsonResult(e.Message);
                }
            }
            Response.StatusCode = 400;
            return new JsonResult("Formularz został błędnie wypełniony.");
        }
        [HttpGet("departments")]
        public IActionResult GetDepartments()
        {
            List<string> list = querySemester.GetDepartments();
            if (list.Count != 0)
            {
                Response.StatusCode = 200;
                return new JsonResult(list, JsonSettings.DefaultJsonSettings);
            }
            Response.StatusCode = 204;
            return StatusCode(204);
        }
        [HttpGet("years")]
        public IActionResult GetYears()
        {
            List<string> list = querySemester.GetYears();
            if (list.Count != 0)
            {
                Response.StatusCode = 200;
                return new JsonResult(list, JsonSettings.DefaultJsonSettings);
            }
            Response.StatusCode = 204;
            return StatusCode(204);
        }
        [HttpGet("semestersnum")]
        public IActionResult GetSemestersNum()
        {
            List<int> list = querySemester.GetSemestersNum();
            if (list.Count != 0)
            {
                Response.StatusCode = 200;
                return new JsonResult(list, JsonSettings.DefaultJsonSettings);
            }
            Response.StatusCode = 204;
            return StatusCode(204);
        }
    }
}
