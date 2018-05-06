using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SZPNUW.Base.Resources;

namespace SZPNUW.Data
{
    public class StudentModel : UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        [StringLength(6, MinimumLength = 6, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "LengthError")]
        public string AlbumNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        public int SemesterId { get; set; }
    }
}
