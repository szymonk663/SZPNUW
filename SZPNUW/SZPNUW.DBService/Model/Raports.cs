using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Raports
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public byte[] Content { get; set; }
        public int Sectionid { get; set; }

        public Sections Section { get; set; }
    }
}
