using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Studentssections
    {
        public Studentssections()
        {
            Meetings = new HashSet<Meetings>();
        }

        public int Id { get; set; }
        public int Studentid { get; set; }
        public int Sectionid { get; set; }
        public float? Rating { get; set; }
        public DateTime? Dateofentry { get; set; }

        public Sections Section { get; set; }
        public Students Student { get; set; }
        public ICollection<Meetings> Meetings { get; set; }
    }
}
