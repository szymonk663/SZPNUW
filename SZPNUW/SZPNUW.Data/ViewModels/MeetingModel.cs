using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.Data
{
    public class MeetingModel
    {
        public int Id { get; set; }
        public int SectionStudentId { get; set; }
        public DateTime Date { get; set; }
        public float? Rate { get; set; }
    }
}
