using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Students
    {
        public Students()
        {
            Studentssections = new HashSet<Studentssections>();
        }

        public int Id { get; set; }
        public int Userid { get; set; }
        public char Albumnumber { get; set; }

        public Users User { get; set; }
        public ICollection<Studentssections> Studentssections { get; set; }
    }
}
