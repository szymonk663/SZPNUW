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
        public List<SectionStudentsModel> GetSectionsStudents(int subjectId, int semesterId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                int? subjectSemesterId = context.Subjectssemesters.Where(x => x.Subjectid == subjectId && x.Semesterid == semesterId).Select(x => x.Id).FirstOrDefault();
                if (subjectSemesterId.HasValue)
                {
                    List<Sections> sections = context.Sections.Where(s => s.Subcjetsemesterid == subjectSemesterId).ToList();
                    List<SectionStudentsModel> model = new List<SectionStudentsModel>();

                    foreach (Sections section in sections)
                    {
                        List<int> studentsId = context.Studentssections.Where(x => x.Sectionid == section.Id).Select(x => x.Studentid).ToList();
                        List<StudentModel> students = context.Students
                            .Where(s => studentsId.Contains(s.Id))
                            .Select(x => new StudentModel
                            {
                                Id = x.Id,
                                UserId = x.Userid,
                                Login = x.User.Login,
                                FirstName = x.User.Firstname,
                                LastName = x.User.Lastname,
                                PESEL = x.User.Pesel,
                                DateOfBirth = x.User.Dateofbirth,
                                AlbumNumber = x.Albumnumber,
                                Address = x.User.Address,
                                City = x.User.City,
                                UserType = (UserTypes)x.User.Usertype
                            })
                            .ToList();
                        SectionModel sectionModel = new SectionModel { Id = section.Id, ProjectId = section.Projectid, SectionNumber = section.Sectionnumber, SubjectSemesterId = section.Subcjetsemesterid };
                        model.Add(new SectionStudentsModel(sectionModel, students));
                    }
                    return model;
                }
                return null;
            }
        }

        public int? GetSectionStudentId(int sectionId, int studentId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                return context.Studentssections.Where(x => x.Sectionid == sectionId && x.Studentid == studentId).Select(x => x.Id).FirstOrDefault();
            }
        }

        public bool StudentInSection(int sectionId, int studentId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                return context.Studentssections.Where(x => x.Sectionid == sectionId && x.Studentid == studentId).Any();
            }
        }

        public int? AddSections(SectionsCreateModel model, ref string errorMessage)
        {
            int MAX_SECTIONS = 50;
            int c;
            int? subSemId;
            int notCreated = 0;
            using (SZPNUWContext context = new SZPNUWContext())
            {
                subSemId = context.Subjectssemesters.Where(x => x.Subjectid == model.SubjectId && x.Semesterid == model.SemesterId).Select(x => x.Id).FirstOrDefault();
                if (subSemId.HasValue)
                {
                    c = context.Sections.Where(s => s.Subcjetsemesterid == subSemId).ToList().Count;
                    int i = 0;
                    while (i < model.Count && c < MAX_SECTIONS)
                    {
                        try
                        {
                            context.Sections.Add(new Sections { Subcjetsemesterid = subSemId.Value, Sectionnumber = c + 1 });
                            context.SaveChanges();
                            c++;
                        }
                        catch
                        {
                            notCreated++;
                        }
                        i++;
                    }
                    return model.Count - notCreated;
                }
                errorMessage = PortalMessages.NoSubjectOnSemester;
                return null;
            }
        }

        public bool UpdateSection(SectionModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Sections section = context.Sections.FirstOrDefault(s => s.Id == model.Id);
                if(section != null)
                {
                    section.Projectid = model.ProjectId;
                    section.Sectionnumber = model.SectionNumber;
                    section.Subcjetsemesterid = model.SubjectSemesterId;
                    context.SaveChanges();
                }
                errorMessage = PortalMessages.NoSuchElement;
                return false;
            }
        }

        public bool DeleteSection(int id, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Sections section = new Sections { Id = id };
                try
                {
                    context.Sections.Attach(section);
                    context.Sections.Remove(section);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    errorMessage = PortalMessages.SectionWithStudents;
                    return false;
                }
            }
        }

        public bool AddStudentToSection(StudentSectionModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Sections section = context.Sections.Where(x => x.Id == model.SectionId).FirstOrDefault();
                if(section != null)
                {
                    bool result = (from ss in context.Studentssections
                                  join s in context.Sections on ss.Sectionid equals s.Id
                                  where s.Subcjetsemesterid == section.Subcjetsemesterid && ss.Studentid == model.StudentId
                                  select ss).Any();
                    if(result)
                    {
                        Studentssections studSec = new Studentssections()
                        {
                            Sectionid = model.SectionId,
                            Studentid = model.StudentId
                        };
                        context.Studentssections.Add(studSec);
                        context.SaveChanges();
                        return true;
                    }
                }
                errorMessage = PortalMessages.CanNotAddStudentToSection;
                return false;
            }
        }

        public bool DeleteStudentFromSection(int studentId, int sectionId, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Studentssections section = new Studentssections { Sectionid = sectionId, Studentid = studentId };
                try
                {
                    context.Studentssections.Attach(section);
                    context.Studentssections.Remove(section);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    errorMessage = PortalMessages.SectionWithStudents;
                    return false;
                }
            }
        }
    }
}
