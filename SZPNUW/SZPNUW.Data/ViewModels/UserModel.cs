using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SZPNUW.Base;
using SZPNUW.Base.Resources;

namespace SZPNUW.Data
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        [StringLength(64, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaximumLengthError")]
        public string Login { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        [StringLength(64, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaximumLengthError")]
        public string Password { get; set; }
        public UserTypes UserType { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        [StringLength(64, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaximumLengthError")]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        [StringLength(64, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaximumLengthError")]
        public string LastName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        [StringLength(11, MinimumLength = 11, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "LengthError")]
        public string PESEL { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        [StringLength(64, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaximumLengthError")]
        public string City { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        [StringLength(128, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaximumLengthError")]
        public string Address { get; set; }

        public void SkipPasswordValidation(ModelStateDictionary modelState)
        {
            if(modelState != null)
                modelState.ClearModelStateErrorLazy("Password");
        }
    }
}
