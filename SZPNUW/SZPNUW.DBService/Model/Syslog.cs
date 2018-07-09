using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.DBService.Model
{
    public partial class Syslog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
    }
}
