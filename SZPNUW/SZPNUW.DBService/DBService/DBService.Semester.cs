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
        public List<SemesterModel> GetSemesters()
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                List<SemesterModel> list = new List<SemesterModel>();
                return context.Semesters
                    .OrderBy(x => x.Fieldofstudy)
                    .ThenBy(x => x.Semesternumber)
                    .Select(x => new SemesterModel()
                    {
                        Id = x.Id,
                        Department = x.Fieldofstudy,
                        SemesterNumber = x.Semesternumber,
                        Year = x.Academicyear
                    }).ToList();
            }  
        }
        public List<SemesterModel> GetSemestersBySubjectId(int subjectId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                List<Semesters> semesters = context.Subjectssemesters
                    .Where(x => x.Subjectid == subjectId)
                    .Select(x => x.Semester)
                    .OrderBy(s => s.Fieldofstudy)
                    .ThenBy(s => s.Academicyear)
                    .ToList();
                return semesters.Select(x => new SemesterModel()
                {
                    Id = x.Id,
                    Department = x.Fieldofstudy,
                    Year = x.Academicyear,
                    SemesterNumber = x.Semesternumber
                }).ToList();
            }
        }

        public List<SemesterModel> GetSemestersByStudentId(int studentId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                List<Semesters> semesters = context.Studentsemester
                    .Where(x => x.Studentid == studentId)
                    .Select(x => x.Semester)
                    .OrderBy(s => s.Fieldofstudy)
                    .ThenBy(s => s.Academicyear)
                    .ToList();
                return semesters.Select(x => new SemesterModel()
                {
                    Id = x.Id,
                    Department = x.Fieldofstudy,
                    Year = x.Academicyear,
                    SemesterNumber = x.Semesternumber
                }).ToList();
            }
                
        }
        public SemesterModel GetSemesterById(int id)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Semesters semester = context.Semesters.FirstOrDefault(s => s.Id == id);
                SemesterModel model = new SemesterModel()
                {
                    Id = semester.Id,
                    Department = semester.Fieldofstudy,
                    SemesterNumber = semester.Semesternumber,
                    Year = semester.Academicyear
                };
                return model;
            }
        }
        public void AddSemester(SemesterModel model)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Semesters semester = new Semesters()
                {
                    Fieldofstudy = model.Department,
                    Academicyear = model.Year,
                    Semesternumber = model.SemesterNumber
                };
                context.Add(semester);
                context.SaveChanges()
            }
        }
        public bool UpdateSemester(SemesterModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Semesters semester = context.Semesters.FirstOrDefault(x => x.Id == model.Id);
                if(semester != null)
                {
                    semester.Fieldofstudy = model.Department;
                    semester.Academicyear = model.Year;
                    semester.Semesternumber = model.SemesterNumber;
                    context.SaveChanges();
                    return true;
                }
                errorMessage = PortalMessages.NoSuchElement;
                return false;
            }
        }
        public List<string> GetDepartments()
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                return context.Semesters.GroupBy(x => x.Fieldofstudy).Select(x => x.Key).ToList();
            }
        }
        public List<string> GetYears()
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                return context.Semesters.GroupBy(x => x.Academicyear).Select(x => x.Key).ToList();
            }
        }
        public List<int> GetSemestersNum()
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                return context.Semesters.GroupBy(x => x.Semesternumber).Select(x => x.Key).ToList();
            }
        }
    }
}
