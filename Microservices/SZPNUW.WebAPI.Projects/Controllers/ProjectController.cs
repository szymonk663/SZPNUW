using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SZPNUW.Base;
using SZPNUW.Data;
using SZPNUW.DBService;

namespace SZPNUW.WebAPI.Projects.Controllers
{
    [Route("[controller]/[action]")]
    public class ProjectController : Controller
    {
        private Service service = new Service();

        [HttpGet("{id}")]
        public IActionResult GetBySubjectId(int id)
        {
            List<ProjectInstructorModel> list = service.GetProjectInstructorBySubjectId(id);
            return Json(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectSubjectByInstructorId(int id)
        {
            List<ProjectSubjectModel> list = service.GetProjectSubjectByInstructorId(id);
            return Json(list);
        }
        [HttpGet("{id}")]
        public IActionResult GetProjectsBySectionId(int id)
        {
            List<ProjectInstructorModel> list = service.GetProjectsBySectionId(id);
            return Json(list);
        }
        [HttpGet("{id}")]
        public IActionResult GetProjectBySectionId(int id)
        {
            ProjectModel model = service.GetProjectBySectionId(id);
            return Json(model);
        }
        [HttpPost]
        public IActionResult AddProject([FromBody]ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.AddProject(model, ref errorMessage))
                    return Json(new Result(true));
                return Json(errorMessage);
            }
            return Json(new Result(ModelState.GetFirstError()));
        }
        [HttpPut]
        public IActionResult UpdateProject([FromBody]ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.UpdateProject(model, ref errorMessage))
                    return Json(new Result(true));
                return Json(errorMessage);
            }
            return Json(new Result(ModelState.GetFirstError()));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            service.DeleteProject(id);
            return Json(new Result(true));
        }
        [HttpGet]
        public IActionResult Test()
        {
            return Json("project");
        }
    }
}
