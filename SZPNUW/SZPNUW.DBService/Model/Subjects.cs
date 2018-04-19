using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Subjects
    {
        public Subjects()
        {
            Projects = new HashSet<Projects>();
            Subjectssemesters = new HashSet<Subjectssemesters>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Leaderid { get; set; }

        public Lecturers Leader { get; set; }
        public ICollection<Projects> Projects { get; set; }
        public ICollection<Subjectssemesters> Subjectssemesters { get; set; }
    }
}
