using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Semesters
    {
        public Semesters()
        {
            Studentsemester = new HashSet<Studentsemester>();
            Subjectssemesters = new HashSet<Subjectssemesters>();
        }

        public int Id { get; set; }
        public string Academicyear { get; set; }
        public int Semesternumber { get; set; }
        public string Fieldofstudy { get; set; }

        public ICollection<Studentsemester> Studentsemester { get; set; }
        public ICollection<Subjectssemesters> Subjectssemesters { get; set; }
    }
}
