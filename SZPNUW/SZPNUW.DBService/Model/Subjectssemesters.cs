using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Subjectssemesters
    {
        public Subjectssemesters()
        {
            Sections = new HashSet<Sections>();
        }

        public int Id { get; set; }
        public int Subjectid { get; set; }
        public int Semesterid { get; set; }

        public Semesters Semester { get; set; }
        public Subjects Subject { get; set; }
        public ICollection<Sections> Sections { get; set; }
    }
}
