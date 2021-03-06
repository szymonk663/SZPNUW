﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SZPNUW.Base;
using SZPNUW.Base.Resources;

namespace SZPNUW.Data
{
    public class SemestersIdModel
    {
        public int StudentId { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        public int? SemesterId { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        public int? NewSemesterId { get; set; }

        public void SkipNewSemesterIdValidation(ModelStateDictionary modelState)
        {
            if (modelState != null)
                modelState.ClearModelStateErrorLazy("NewSemesterId");
        }

        public void SkipSemesterIdValidation(ModelStateDictionary modelState)
        {
            if (modelState != null)
                modelState.ClearModelStateErrorLazy("SemesterId");
        }
    }
}
