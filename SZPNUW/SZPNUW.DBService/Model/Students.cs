using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Students
    {
        public Students()
        {
            Studentsemester = new HashSet<Studentsemester>();
            Studentssections = new HashSet<Studentssections>();
        }

        public int Id { get; set; }
        public int Userid { get; set; }
        public string Albumnumber { get; set; }

        public Users User { get; set; }
        public ICollection<Studentsemester> Studentsemester { get; set; }
        public ICollection<Studentssections> Studentssections { get; set; }
    }
}
