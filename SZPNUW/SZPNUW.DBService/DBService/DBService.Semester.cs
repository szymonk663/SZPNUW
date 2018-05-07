using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                List<SemesterModel> list = new List<SemesterModel>();
                List<int> semestersId = new List<int>();
                semestersId = context.Subjectssemesters.Where(x => x.Subjectid == subjectId).Select(ps => ps.IdSemestru).ToList();
                foreach (Semestr sem in context.Semestr
                    .Where(s => semestersId.Contains(s.Id))
                    .OrderBy(s => s.Kierunek)
                    .ThenBy(s => s.NrSemestru)
                    .ToList())
                {
                    list.Add(new Semester(sem));
                }
                return list;
            }
        }

        public List<Semester> GetSemestersByStudentId(int id)
        {
            List<Semester> list = new List<Semester>();
            List<int> semestersId = new List<int>();
            semestersId = context.StudSem.Where(ss => ss.IdStudent == id).Select(ss => ss.IdSemestru).ToList();
            foreach (Semestr sem in context.Semestr
                .Where(s => semestersId.Contains(s.Id))
                .OrderBy(s => s.Kierunek)
                .ThenBy(s => s.NrSemestru)
                .ToList())
            {
                list.Add(new Semester(sem));
            }
            return list;
        }
        public Semester GetSemesterById(int id)
        {
            Semestr semestr;
            try
            {
                semestr = context.Semestr.Where(s => s.Id == id).First();
            }
            catch (Exception)
            {
                throw new Exception("Nie ma takiego semestru.");
            }
            Semester sv = new Semester(semestr);
            return sv;
        }
        public void AddSemester(Semester semester)
        {
            Semestr semestr = new Semestr();
            semester.CopyToSemestr(semestr);
            semestr.Id = 0;
            try
            {
                context.Add(semestr);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Dane są niepoprawne.");
            }
        }
        public void UpdateSemester(Semester semester)
        {
            try
            {
                Semestr se = context.Semestr.Where(s => s.Id == semester.id).Single();
                semester.CopyToSemestr(se);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Nie można zmodyfikować tych danych.");
            }
        }
        public List<string> GetDepartments()
        {
            List<string> list = new List<string>();
            foreach (var dep in context.Semestr
                .GroupBy(s => s.Kierunek).Select(s => s.Key)
                .ToList())
            {
                list.Add(dep.ToString().Trim());
            }
            return list;
        }
        public List<string> GetYears()
        {
            List<string> list = new List<string>();
            foreach (var dep in context.Semestr
                .GroupBy(s => s.RokAkademicki).Select(s => s.Key)
                .ToList())
            {
                list.Add(dep.ToString().Trim());
            }
            return list;
        }
        public List<int> GetSemestersNum()
        {
            List<int> list = new List<int>();
            foreach (var dep in context.Semestr
                .GroupBy(s => s.NrSemestru).Select(s => s.Key)
                .ToList())
            {
                list.Add(dep);
            }
            return list;
        }
    }
}
