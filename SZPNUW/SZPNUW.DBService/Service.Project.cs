using System;
using System.Collections.Generic;
using System.Text;
using SZPNUW.Data;

namespace SZPNUW.DBService
{
    public partial class Service
    {
        public List<ProjectInstructorModel> GetProjectInstructorBySubjectId(int subjectId)
        {
            return service.GetProjectInstructorBySubjectId(subjectId);
        }
        public List<ProjectSubjectModel> GetProjectSubjectByInstructorId(int userId)
        {
            return service.GetProjectSubjectByInstructorId(userId);
        }
        public List<ProjectInstructorModel> GetProjectsBySectionId(int sectionId)
        {
            return service.GetProjectsBySectionId(sectionId);
        }
        public ProjectModel GetProjectBySectionId(int sectionId)
        {
            return service.GetProjectBySectionId(sectionId);
        }
        public bool UpdateProject(ProjectModel model, ref string errorMessage)
        {
            return service.UpdateProject(model, ref errorMessage);
        }
        public bool AddProject(ProjectModel model, ref string errorMessage)
        {
            return service.AddProject(model, ref errorMessage);
        }
        public void DeleteProject(int projectId)
        {
            service.DeleteProject(projectId);
        }
    }
}
