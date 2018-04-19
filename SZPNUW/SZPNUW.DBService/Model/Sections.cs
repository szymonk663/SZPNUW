using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Sections
    {
        public Sections()
        {
            Raports = new HashSet<Raports>();
            Studentssections = new HashSet<Studentssections>();
        }

        public int Id { get; set; }
        public int Sectionnumber { get; set; }
        public int Subcjetsemesterid { get; set; }
        public int Projectid { get; set; }

        public Projects Project { get; set; }
        public Subjectssemesters Subcjetsemester { get; set; }
        public ICollection<Raports> Raports { get; set; }
        public ICollection<Studentssections> Studentssections { get; set; }
    }
}
