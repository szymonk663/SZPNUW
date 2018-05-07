using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.Base
{
    public static class JsonSettings
    {
        public static JsonSerializerSettings DefaultJsonSettings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                };
            }
        }
    }
}
