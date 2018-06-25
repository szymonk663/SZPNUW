using System;
using System.Collections.Generic;
using System.Text;
using SZPNUW.Data;
using SZPNUW.DBService.Model;
using System.Linq;
using SZPNUW.Base.Resources;
using SZPNUW.Base;

namespace SZPNUW.DBService
{
    public partial class DBService
    {
        public List<SubjectModel> GetSubjects(string department, int semesterNumber, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                List<int> subjectsId;
                List<Subjects> subjects = new List<Subjects>();
                try
                {
                    subjectsId = this.GetSubjectIdByDepartmentAndSemesterNum(context, department, semesterNumber, ref errorMessage);
                    if(subjectsId.AnyLazy())
                        subjects = context.Subjects.Where(x => subjectsId.Contains(x.Id)).ToList();
                }
                catch (ArgumentNullException)
                {
                    errorMessage = PortalMessages.MissingSubject;
                    return null;
                }
                List<SubjectModel> result = subjects.Select(x => new SubjectModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name,
                    LeaderId = x.Leaderid
                }).ToList();
                return result;
            } 
        }

        private List<int> GetSubjectIdByDepartmentAndSemesterNum(SZPNUWContext context, string department, int semesterNumber, ref string errorMessage)
        {
            try
            {
                return (from s in context.Semesters
                        join ss in context.Subjectssemesters on s.Id equals ss.Semesterid
                        where s.Fieldofstudy == department && s.Semesternumber == semesterNumber
                        group ss by ss.Subjectid into r
                        select r.Key).Distinct().ToList();
            }
            catch (ArgumentNullException)
            {
                errorMessage = PortalMessages.MissingSemester;
                return null;
            }
        }

        public SubjectModel GetSubjectById(int id)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Subjects subject = context.Subjects.FirstOrDefault(p => p.Id == id);
                if (subject != null)
                    return new SubjectModel
                    {
                        Id = subject.Id,
                        Description = subject.Description,
                        Name = subject.Name,
                        LeaderId = subject.Leaderid
                    };
                return null;
            } 
        }

        public List<SubjectModel> GetSubjectsBySemesterId(int id)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                List<int> subjectsId;
                List<Subjects> subjects;

                subjectsId = context.Subjectssemesters.Where(x => x.Semesterid == id).Select(x => x.Subjectid).ToList();
                if (subjectsId.AnyLazy())
                {
                    subjects = context.Subjects.Where(x => subjectsId.Contains(x.Id)).ToList();

                    List<SubjectModel> result = subjects.Select(x => new SubjectModel
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Name = x.Name,
                        LeaderId = x.Leaderid
                    }).ToList();
                    return result;
                }
                return null;
            }
        }

        public SubjectSemesterModel GetSubjectSemester(int subjectId, int semesterId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Subjectssemesters subsem = context.Subjectssemesters.FirstOrDefault(x => x.Subjectid == subjectId && x.Semesterid == semesterId);
                return subsem == null ? null : new SubjectSemesterModel
                {
                    Id = subsem.Id,
                    SemesterId = subsem.Semesterid,
                    SubjectId = subsem.Subjectid
                };
            }
        }

        public bool UpdateSubject(SubjectModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Subjects subject = context.Subjects.FirstOrDefault(x => x.Id == model.Id);
                if(subject != null)
                {
                    subject.Name = model.Name;
                    subject.Description = model.Description;
                    context.SaveChanges();
                    return true;
                }
                errorMessage = PortalMessages.NoSuchElement;
                return false;
            }
        }

        public bool AddSubject(SubjectModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                if (!context.Subjects.Where(x => x.Name == model.Name).AnyLazy())
                {
                    Subjects subject = new Subjects()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Leaderid = model.LeaderId.Value
                    };
                    Subjectssemesters subSem = new Subjectssemesters();
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Add(subject);
                            context.SaveChanges();
                            int id = context.Subjects.Where(x => x.Name == subject.Name).Select(x => x.Id).First();
                            subSem.Subjectid = id;
                            subSem.Semesterid = model.SemesterId.Value;
                            context.Add(subSem);
                            context.SaveChanges();
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            errorMessage = PortalMessages.InvalidData;
                        }
                    }
                }
                else
                    errorMessage = PortalMessages.SubjectExist;
                return false;
            }
        }

        #region Zarządzanie przedmiotami na semestrach

        public bool AddSubjectSemester(SubjectSemesterModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                if (!context.Subjectssemesters.Where(x => x.Subjectid == model.SubjectId && x.Semesterid == model.SemesterId).AnyLazy())
                {
                    Subjectssemesters subSem = new Subjectssemesters();
                    subSem.Subjectid = model.SubjectId;
                    subSem.Semesterid = model.SemesterId;
                    context.Add(subSem);
                    context.SaveChanges();
                    return true;
                }
                else
                    errorMessage = PortalMessages.ObjectExist;
                return false;
            }
        }
        public bool UpdateSubjectSemester(SubjectSemesterModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                if (!context.Subjectssemesters.Where(x => x.Subjectid == model.SubjectId && x.Semesterid == model.SemesterId).AnyLazy())
                {
                    Subjectssemesters subSem = context.Subjectssemesters
                        .Where(x => x.Id == model.Id)
                        .First();
                    subSem.Subjectid = model.SubjectId;
                    subSem.Semesterid = model.SemesterId;
                    context.SaveChanges();
                    return true;
                }
                else
                    errorMessage = PortalMessages.ObjectExist;
                return false;
            }
        }
        public bool DeleteSubjectSemester(int id, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Subjectssemesters subSem = context.Subjectssemesters.FirstOrDefault(x => x.Id == id);
                if(subSem == null)
                {
                    errorMessage = PortalMessages.NoSuchElement;
                    return false;
                }
                if (context.Subjectssemesters.Where(x => x.Subjectid == subSem.Subjectid).ToList().Count <= 1)
                {
                    errorMessage = PortalMessages.LastEntryForTheItem;
                    return false;
                } 
                if (context.Sections.Where(x => x.Subcjetsemesterid == id).AnyLazy())
                {
                    errorMessage = PortalMessages.SemesterDependence;
                    return false;
                }
                context.Subjectssemesters.Attach(subSem);
                context.Subjectssemesters.Remove(subSem);
                context.SaveChanges();
                return true;
            }
        }

        #endregion
    }
}
