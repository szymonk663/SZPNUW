using System;
using System.Collections.Generic;
using System.Text;
using SZPNUW.Data;

namespace SZPNUW.DBService
{
    public partial class Service
    {
        public bool ChangePassword(ChangePasswordModel model, ref string errorMessage)
        {
            return service.ChangePassword(model, ref errorMessage);
        }

        public Auth Login(LoginModel model, ref string errorMessage)
        {
            return service.Login(model, ref errorMessage);
        }
    }
}
