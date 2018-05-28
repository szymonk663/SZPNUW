using System;
using System.Collections.Generic;
using System.Text;
using SZPNUW.Data;

namespace SZPNUW.DBService
{
    public partial class Service
    {

        public bool RegisterStudent(StudentModel model, ref string errorMessage)
        {
            return service.RegisterStudent(model, ref errorMessage);
        }

        public StudentModel GetStudentById(int id)
        {
            return service.GetStudentById(id);
        }

        public float GetStudentAverageRating(int sectionId, int studentId)
        {
            return service.GetStudentAverageRating(sectionId, studentId);
        }

        public StudentSectionModel GetSectionStudent(int sectionId, int studentId)
        {
            return service.GetSectionStudent(sectionId, studentId);
        }

        public List<StudentModel> GetStudentBySemesterId(int semesterId)
        {
            return service.GetStudentBySemesterId(semesterId);
        }

        public bool UpdateStudent(StudentModel model, ref string errorMessage)
        {
            return UpdateStudent(model, ref errorMessage);
        }

        public bool UpdateStudentSection(StudentSectionModel model, ref string errorMessage)
        {
            return service.UpdateStudentSection(model, ref errorMessage);
        }

        public void RewriteStudentsSemester(SemestersIdModel model, ref string errorMessage)
        {
            service.RewriteStudentsSemester(model, ref errorMessage);
        }

        public void RewriteStudentSemester(int studentId, int semesterId, ref string errorMessage)
        {
            service.RewriteStudentSemester(studentId, semesterId, ref errorMessage);
        }

        public void UpdateStudentsSemester(SemestersIdModel model, ref string errorMessage)
        {
            service.UpdateStudentsSemester(model, ref errorMessage);
        }

        public void UpdateStudentSemester(int studentId, int oldSemesterId, int newSemesterId, ref string errorMessage)
        {
            service.UpdateStudentSemester(studentId, oldSemesterId, newSemesterId, ref errorMessage);
        }

        public void DeleteStudentsSemester(int semesterId, ref string errorMessage)
        {
            service.DeleteStudentsSemester(semesterId, ref errorMessage);
        }

        public bool DeleteStudentSemester(int studentId, int semesterId, ref string errorMessage)
        {
            return service.DeleteStudentSemester(studentId, semesterId, ref errorMessage);
        }
    }
}
