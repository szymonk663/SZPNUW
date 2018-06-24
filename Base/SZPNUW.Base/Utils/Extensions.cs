using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Force.DeepCloner;
using System.Security.Principal;

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

        public static bool AnyLazy<T>(this IEnumerable<T> collection)
        {
            return collection != null && collection.Any();
        }

        public static bool HasValue(this string value)
        {
            return value != null && value.Any();
        }

        public static string WithFormatExtesion(this string value, object[] args)
        {
            return string.Format(value, args);
        }

        public static T Clone<T>(this T item) where T : class, new()
        {
            return item != null ? item.DeepClone() : default(T);
        }

        public static bool IsLogedIn(this IPrincipal principal)
        {
            try
            {
                return principal.Identity.IsAuthenticated;
            }
            catch
            {
                return false;
            }
        }
    }
}
