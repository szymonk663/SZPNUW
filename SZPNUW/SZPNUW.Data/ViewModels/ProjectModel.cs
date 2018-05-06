using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.Data
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public int InstructorId { get; set; }
        public int SubjectId { get; set; }
        public bool Active { get; set; }
    }
}
