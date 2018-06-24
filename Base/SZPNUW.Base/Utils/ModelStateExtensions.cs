using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.Base
{
    public static class ModelStateExtensions
    {
        public static void ClearModelStateErrorLazy(this ModelStateDictionary modelState, params string[] keys)
        {
            if(modelState != null && keys.AnyLazy())
            {
                foreach(string key in keys)
                {
                    modelState.Remove(key);
                }
            }
        }
    }
}
