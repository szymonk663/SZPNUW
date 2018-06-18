using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SZPNUW.Base;
using SZPNUW.Data;
using SZPNUW.DBService;

namespace SZPNUW.WebAPI.Sections.Controllers
{
    [Route("[controller]/[action]")]
    public class MeetingController : Controller
    {
        private Service service = new Service();

        [HttpGet]
        public IActionResult GetMeetingsBySectionStudent([FromQuery]int sectionId, int studentId)
        {

            List<MeetingModel> meetings = service.GetMeetingsBySectionStudent(sectionId, studentId);
            return Json(meetings);
        }

        [HttpPost]
        public IActionResult AddMeeting([FromBody]MeetingModel model)
        {
            if (ModelState.IsValid)
            {
                service.AddMeeting(model);
                return Json(new Result(true));
            }
            return Json(ModelState.GetFirstError());
        }

        [HttpPut]
        public IActionResult UpdateMeeting([FromBody]MeetingModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                if (service.UpdateMeeting(model, ref errorMessage))
                {
                    return Json(new Result(true));
                }
                return Json(new Result(errorMessage));
            }
            return Json(ModelState.GetFirstError());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMeeting(int id)
        {
            service.DeleteMeeting(id);
            return Json(new Result(true));
        }
        [HttpGet]
        public IActionResult Test()
        {
            return Json("meeting");
        }
    }
}
