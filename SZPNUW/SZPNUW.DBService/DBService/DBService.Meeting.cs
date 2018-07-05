using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SZPNUW.Base;
using SZPNUW.Base.Resources;
using SZPNUW.Data;
using SZPNUW.DBService.Model;

namespace SZPNUW.DBService
{
    public partial class DBService
    {
        public List<MeetingModel> GetMeetingsBySectionStudent(int sectionId, int studentId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                List<Meetings> meetings = context.Studentssections.Include(x => x.Meetings)
                    .FirstOrDefault(x => x.Sectionid == sectionId && x.Studentid == studentId)?.Meetings.ToList();
                if (meetings.AnyLazy())
                {
                    return meetings.Select(x => new MeetingModel { Id = x.Id, Date = x.Dateofentry.Value, Rate = x.Rating, SectionStudentId = x.Studentssectionsid }).ToList();
                }
                return null;    
            }
        }
        public bool UpdateMeeting(MeetingModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Meetings meeting = context.Meetings.FirstOrDefault(s => s.Id == model.Id);
                if(meeting != null)
                {
                    meeting.Rating = model.Rate;
                    meeting.Dateofentry = model.Date;
                    context.SaveChanges();
                    return true;
                }
                errorMessage = PortalMessages.NoSuchElement;
                return false;
            }
        }
        public void AddMeeting(MeetingModel model)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Meetings meeting = new Meetings
                {
                    Rating = model.Rate,
                    Dateofentry = model.Date,
                    Studentssectionsid = model.SectionStudentId
                };
                context.Meetings.Add(meeting);
                context.SaveChanges();
            }
        }

        public void DeleteMeeting(int projectId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Meetings meeting = new Meetings { Id = projectId };
                context.Meetings.Attach(meeting);
                context.Meetings.Remove(meeting);
                context.SaveChanges();
            }
        }
    }
}
