using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Projects
    {
        public Projects()
        {
            Sections = new HashSet<Sections>();
        }

        public int Id { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public int Lecturerid { get; set; }
        public int Subjectid { get; set; }
        public bool Active { get; set; }

        public Lecturers Lecturer { get; set; }
        public Subjects Subject { get; set; }
        public ICollection<Sections> Sections { get; set; }
    }
}
