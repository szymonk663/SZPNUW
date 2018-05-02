using System;
using System.Collections.Generic;

namespace SZPNUW.DBService.Model
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public short Usertype { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Pesel { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public Lecturers Lecturers { get; set; }
        public Students Students { get; set; }
    }
}
