using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.Data
{
    public class Auth : Result
    {
        public UserTypes UserType { get; set; }

        public Auth()
        {

        }

        public Auth(bool isSuceeded, string errorMessage) : base(isSuceeded, errorMessage)
        {

        }

        public Auth(string errorMessage) : base(false, errorMessage)
        {

        }

        public Auth(bool isSuceeded) : base(isSuceeded, string.Empty)
        {

        }
    }
}
