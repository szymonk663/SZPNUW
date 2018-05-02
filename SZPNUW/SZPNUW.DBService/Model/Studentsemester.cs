using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Studentsemester
    {
        public int Studentid { get; set; }
        public int Semesterid { get; set; }

        public Semesters Semester { get; set; }
        public Students Student { get; set; }
    }
}
