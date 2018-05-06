using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.Data
{
    public class SubjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LeaderId { get; set; }
        public int SemesterId { get; set; }
    }
}
