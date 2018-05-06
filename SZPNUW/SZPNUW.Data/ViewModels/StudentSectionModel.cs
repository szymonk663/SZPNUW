using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.Data
{
    public class StudentSectionModel
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int StudentId { get; set; }
        public float? Rate { get; set; }
        public DateTime? Date { get; set; }
    }
}
