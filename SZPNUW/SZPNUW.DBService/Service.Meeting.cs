using System;
using System.Collections.Generic;
using System.Text;
using SZPNUW.Data;

namespace SZPNUW.DBService
{
    public partial class Service
    {
        public List<MeetingModel> GetMeetingsBySectionStudent(int sectionId, int studentId)
        {
            return service.GetMeetingsBySectionStudent(sectionId, studentId);
        }
        public bool UpdateMeeting(MeetingModel model, ref string errorMessage)
        {
            return service.UpdateMeeting(model, ref errorMessage);
        }
        public void AddMeeting(MeetingModel model)
        {
            service.AddMeeting(model);
        }

        public void DeleteMeeting(int projectId)
        {
            service.DeleteMeeting(projectId);
        }
    }
}
