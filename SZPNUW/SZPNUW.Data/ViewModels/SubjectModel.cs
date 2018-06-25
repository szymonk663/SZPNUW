using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SZPNUW.Base;
using SZPNUW.Base.Resources;

namespace SZPNUW.Data
{
    public class SubjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        public int? LeaderId { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        public int? SemesterId { get; set; }

        public void SkipSemesterIdValidation(ModelStateDictionary modelState)
        {
            if (modelState != null)
                modelState.ClearModelStateErrorLazy("SemesterId");
        }
    }
}
