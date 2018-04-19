using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Meetings
    {
        public int Id { get; set; }
        public int Studentssectionsid { get; set; }
        public DateTime? Dateofentry { get; set; }
        public float? Rating { get; set; }

        public Studentssections Studentssections { get; set; }
    }
}
