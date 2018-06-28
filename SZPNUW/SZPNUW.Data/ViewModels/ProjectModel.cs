using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SZPNUW.Base.Resources;

namespace SZPNUW.Data
{
    public class ProjectModel
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        public string Topic { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "RequiredError")]
        public int? UserId { get; set; }
        public int SubjectId { get; set; }
        public bool Active { get; set; }
    }
}
