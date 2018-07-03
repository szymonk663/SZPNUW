using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SZPNUW.Base.Resources;
using SZPNUW.Data;
using SZPNUW.DBService.Model;

namespace SZPNUW.DBService
{
    public partial class DBService
    {
        public List<ProjectInstructorModel> GetProjectInstructorBySubjectId(int subjectId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                List<ProjectInstructorModel> list = new List<ProjectInstructorModel>();
                List<Projects> projects = context.Projects.Include(x => x.Lecturer).Include(x => x.Lecturer.User).Where(x => x.Subjectid == subjectId).ToList();
                projects.ForEach(x =>
                {
                    ProjectModel project = new ProjectModel()
                    {
                        Id = x.Id,
                        Topic = x.Topic,
                        Active = x.Active,
                        Description = x.Description,
                        UserId = x.Lecturerid,
                        SubjectId = x.Subjectid
                    };
                    InstructorModel instructor = new InstructorModel()
                    {
                        Id = x.Lecturer.Id,
                        UserId = x.Lecturer.Userid,
                        Login = x.Lecturer.User.Login,
                        FirstName = x.Lecturer.User.Firstname,
                        LastName = x.Lecturer.User.Lastname,
                        Address = x.Lecturer.User.Address,
                        City = x.Lecturer.User.City,
                        Code = x.Lecturer.Code,
                        DateOfBirth = x.Lecturer.User.Dateofbirth,
                        PESEL = x.Lecturer.User.Pesel,
                        UserType = (UserTypes)x.Lecturer.User.Usertype
                    };
                    list.Add(new ProjectInstructorModel(project, instructor));
                });
                return list;
            }
        }
        public List<ProjectSubjectModel> GetProjectSubjectByInstructorId(int userId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                List<ProjectSubjectModel> list = new List<ProjectSubjectModel>();
                List<Projects> projects = context.Projects.Include(x => x.Subject).Include(x => x.Lecturer).Where(x => x.Lecturer.Userid == userId).ToList();
                projects.ForEach(x =>
                {
                    ProjectModel project = new ProjectModel()
                    {
                        Id = x.Id,
                        Topic = x.Topic,
                        Active = x.Active,
                        Description = x.Description,
                        UserId = x.Lecturer.Userid,
                        SubjectId = x.Subjectid
                    };
                    SubjectModel subject = new SubjectModel()
                    {
                        Id = x.Subject.Id,
                        Description = x.Subject.Description,
                        LeaderId = x.Subject.Leaderid,
                        Name = x.Subject.Name
                    };
                    list.Add(new ProjectSubjectModel(project, subject));
                });
                return list;
            }
        }
        public List<ProjectInstructorModel> GetProjectsBySectionId(int sectionId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                int? subSemId, subjectId;
                List<int?> projectsIdExcluded;
                List<Projects> projectList, projectsExclude;
                List<ProjectInstructorModel> list = new List<ProjectInstructorModel>();

                subSemId = context.Sections.Where(x => x.Id == sectionId).Select(x => x.Subcjetsemesterid).FirstOrDefault();
                subjectId = context.Subjectssemesters.Where(x => x.Id == subSemId).Select(x => x.Subjectid).FirstOrDefault();
                projectsIdExcluded = context.Sections.Where(s => s.Subcjetsemesterid == subSemId).Select(s => s.Projectid).ToList();
                projectsExclude = context.Projects.Where(p => projectsIdExcluded.Contains(p.Id)).ToList();
                projectList = context.Projects.Include(x => x.Subject).Where(p => p.Subjectid == subjectId && p.Active).ToList();
                projectsExclude.ForEach(x => projectList.Remove(x));
                projectList.ForEach(x =>
                {
                    ProjectModel project = new ProjectModel()
                    {
                        Id = x.Id,
                        Topic = x.Topic,
                        Active = x.Active,
                        Description = x.Description,
                        UserId = x.Lecturerid,
                        SubjectId = x.Subjectid
                    };
                    SubjectModel subject = new SubjectModel()
                    {
                        Id = x.Subject.Id,
                        Description = x.Subject.Description,
                        LeaderId = x.Subject.Leaderid,
                        Name = x.Subject.Name
                    };
                });
                return list;
            }
        }
        public ProjectModel GetProjectBySectionId(int sectionId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Projects project = context.Sections.Where(s => s.Id == sectionId).Select(s => s.Project).FirstOrDefault();
                return project != null ? new ProjectModel()
                {
                    Id = project.Id,
                    Topic = project.Topic,
                    Active = project.Active,
                    Description = project.Description,
                    UserId = project.Lecturerid,
                    SubjectId = project.Subjectid
                } : null;
            }
        }
        public bool UpdateProject(ProjectModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Lecturers lecturer = context.Lecturers.FirstOrDefault(x => x.Userid == model.UserId);
                if (lecturer == null)
                {
                    errorMessage = PortalMessages.UserDoesNotExist;
                    return false;
                }
                if (!context.Projects.Any(x => x.Topic == model.Topic && x.Id != model.Id))
                {
                    Projects project = context.Projects.FirstOrDefault(x => x.Id == model.Id);
                    if (project != null)
                    {
                        project.Topic = model.Topic;
                        project.Active = model.Active;
                        project.Description = model.Description;
                        project.Lecturerid = lecturer.Id;
                        project.Subjectid = model.SubjectId;
                        context.SaveChanges();
                        return true;
                    }
                    errorMessage = PortalMessages.NoSuchElement;
                    return false;
                }
                errorMessage = PortalMessages.TopicIsUsed;
                return false;
            }
        }
        public bool AddProject(ProjectModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Lecturers lecturer = context.Lecturers.FirstOrDefault(x => x.Userid == model.UserId);
                if (lecturer == null)
                {
                    errorMessage = PortalMessages.UserDoesNotExist;
                    return false;
                }
                if (!context.Projects.Any(x => x.Topic == model.Topic))
                {
                    Projects project = new Projects()
                    {
                        Topic = model.Topic,
                        Active = model.Active,
                        Description = model.Description,
                        Lecturerid = lecturer.Id,
                        Subjectid = model.SubjectId,
                    };
                    context.Projects.Add(project);
                    context.SaveChanges();
                    return true;
                }
                errorMessage = PortalMessages.TopicIsUsed;
                return false;
            }
        }
        public void DeleteProject(int projectId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Projects project = new Projects { Id = projectId };
                context.Projects.Attach(project);
                context.Projects.Remove(project);
                context.SaveChanges();
            }
        }
    }
}
