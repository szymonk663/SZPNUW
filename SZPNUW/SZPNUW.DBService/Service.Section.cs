using System;
using System.Collections.Generic;
using System.Text;
using SZPNUW.Data;

namespace SZPNUW.DBService
{
    public partial class Service
    {
        public List<SectionStudentsModel> GetSectionsStudents(int subjectId, int semesterId)
        {
            return service.GetSectionsStudents(subjectId, semesterId);
        }

        public int? GetSectionStudentId(int sectionId, int studentId)
        {
            return service.GetSectionStudentId(sectionId, studentId);
        }

        public bool StudentInSection(int sectionId, int studentId)
        {
            return service.StudentInSection(sectionId, studentId);
        }

        public int? AddSections(SectionsCreateModel model, ref string errorMessage)
        {
            return service.AddSections(model, ref errorMessage);
        }

        public bool UpdateSection(SectionModel model, ref string errorMessage)
        {
            return service.UpdateSection(model, ref errorMessage);
        }

        public bool DeleteSection(int id, ref string errorMessage)
        {
            return service.DeleteSection(id, ref errorMessage);
        }

        public bool AddStudentToSection(StudentSectionModel model, ref string errorMessage)
        {
            return service.AddStudentToSection(model, ref errorMessage);
        }

        public bool DeleteStudentFromSection(int studentId, int sectionId, ref string errorMessage)
        {
            return service.DeleteStudentFromSection(studentId, sectionId, ref errorMessage);
        }
    }
}
