﻿using System;
using System.Collections.Generic;
using System.Text;
using SZPNUW.Data;

namespace SZPNUW.DBService
{
    public partial class Service
    {
        public List<SemesterModel> GetSemesters()
        {
            return service.GetSemesters();
        }
        public List<SemesterModel> GetSemestersBySubjectId(int subjectId)
        {
            return service.GetSemestersBySubjectId(subjectId);
        }

        public List<SemesterModel> GetSemestersByStudentId(int studentId)
        {
            return service.GetSemestersByStudentId(studentId);

        }
        public SemesterModel GetSemesterById(int id)
        {
            return service.GetSemesterById(id);
        }
        public void AddSemester(SemesterModel model)
        {
            service.AddSemester(model);
        }
        public bool UpdateSemester(SemesterModel model, ref string errorMessage)
        {
            return service.UpdateSemester(model, ref errorMessage);
        }
        public List<string> GetDepartments()
        {
            return service.GetDepartments();
        }
        public List<string> GetYears()
        {
            return service.GetYears();
        }
        public List<int> GetSemestersNum()
        {
            return service.GetSemestersNum();
        }
    }
}
