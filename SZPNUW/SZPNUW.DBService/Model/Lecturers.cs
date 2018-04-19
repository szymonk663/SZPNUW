using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Lecturers
    {
        public Lecturers()
        {
            Projects = new HashSet<Projects>();
            Subjects = new HashSet<Subjects>();
        }

        public int Id { get; set; }
        public int Userid { get; set; }
        public char Code { get; set; }

        public Users User { get; set; }
        public ICollection<Projects> Projects { get; set; }
        public ICollection<Subjects> Subjects { get; set; }
    }
}
