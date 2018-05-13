using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.Data
{
    public class ProjectSubjectModel
    {
        public ProjectModel Project { get; set; }
        public SubjectModel Subject { get; set; }

        public ProjectSubjectModel()
        {

        }

        public ProjectSubjectModel(ProjectModel project, SubjectModel subject)
        {
            this.Project = project;
            this.Subject = subject;
        }
    }
}
