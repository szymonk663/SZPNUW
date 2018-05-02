using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SZPNUW.Base
{
    public static class Extensions
    {
        public static string GetFirstError(this ModelStateDictionary modelState)
        {
            if(modelState != null && modelState.ErrorCount > 0)
            {
                return WebUtility.HtmlEncode(modelState.Keys.SelectMany(x => modelState[x].Errors).Select(x => x.ErrorMessage).First());
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
