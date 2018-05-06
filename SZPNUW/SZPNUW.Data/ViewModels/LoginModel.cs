using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SZPNUW.Base.Resources;

namespace SZPNUW.Data
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        public string UserName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        public string Password { get; set; }
    }
}
