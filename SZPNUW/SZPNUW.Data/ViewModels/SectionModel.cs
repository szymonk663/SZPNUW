using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.Data
{
    public class SectionModel
    {
        public int Id { get; set; }
        public int SectionNumber { get; set; }
        public int SubjectSemesterId { get; set; }
        public int ProjectId { get; set; }
    }
}
