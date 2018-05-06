using System;
using System.Collections.Generic;
using System.Text;
using SZPNUW.Data;

namespace SZPNUW.DBService
{
    public partial class Service
    {
        public List<SubjectModel> GetSubjects(string department, int semesterNumber, ref string errorMessage)
        {
            return service.GetSubjects(department, semesterNumber, ref errorMessage);
        }

        public SubjectModel GetSubjectById(int id)
        {
            return service.GetSubjectById(id);
        }

        public List<SubjectModel> GetSubjectsBySemesterId(int id)
        {
            return service.GetSubjectsBySemesterId(id);
        }

        public SubjectSemesterModel GetSubjectSemester(int subjectId, int semesterId)
        {
            return service.GetSubjectSemester(subjectId, semesterId);
        }

        public bool UpdateSubject(SubjectModel model, ref string errorMessage)
        {
            return service.UpdateSubject(model, ref errorMessage);
        }

        public bool AddSubject(SubjectModel model, ref string errorMessage)
        {
            return service.AddSubject(model, ref errorMessage);
        }

        public bool AddSubjectSemester(SubjectSemesterModel model, ref string errorMessage)
        {
            return service.AddSubjectSemester(model, ref errorMessage);
        }

        public bool UpdateSubjectSemester(SubjectSemesterModel model, ref string errorMessage)
        {
            return service.UpdateSubjectSemester(model, ref errorMessage);
        }

        public bool DeleteSubjectSemester(int id, ref string errorMessage)
        {
            return service.DeleteSubjectSemester(id, ref errorMessage);
        }
    }
}
