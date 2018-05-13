using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.Data
{
    public class Result
    {
        public bool IsSucceeded { get; set; }
        public string ErrorMessages { get; set; }
        public string PortalMessages { get; set; }
        public int? Id { get; set; }

        public Result()
        {

        }

        public Result(bool isSuceeded, string errorMessage)
        {
            this.IsSucceeded = isSuceeded;
            this.ErrorMessages = errorMessage;
        }

        public Result(string errorMessage) : this(false, errorMessage)
        {

        }

        public Result(bool isSuceeded) : this(isSuceeded, string.Empty)
        {

        }
    }
}
