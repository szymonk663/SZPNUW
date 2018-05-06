using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.Data
{
    public class SectionStudentsModel
    {
        public SectionModel Section { get; set; }
        public List<StudentModel> Students { get; set; }
    }
}
