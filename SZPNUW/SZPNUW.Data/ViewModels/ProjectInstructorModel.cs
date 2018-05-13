using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.Data
{
    public class ProjectInstructorModel
    {
        public ProjectModel Project { get; set; }
        public InstructorModel Instructor { get; set; }

        public ProjectInstructorModel()
        {

        }

        public ProjectInstructorModel(ProjectModel project, InstructorModel instructor)
        {
            this.Project = project;
            this.Instructor = instructor;
        }
    }
}
