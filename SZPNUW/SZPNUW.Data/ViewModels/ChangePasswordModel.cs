using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SZPNUW.Base.Resources;

namespace SZPNUW.Data
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        [StringLength(64, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaximumLengthError")]
        public string OldPassword { get; set; }
        [StringLength(64, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaximumLengthError")]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        public string NewPassword { get; set; }
    }
}
