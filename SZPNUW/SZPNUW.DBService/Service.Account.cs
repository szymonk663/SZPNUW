using System;
using System.Collections.Generic;
using System.Text;
using SZPNUW.Data;

namespace SZPNUW.DBService
{
    public partial class Service
    {
        public bool ChangePassword(int userId, ChangePasswordModel model, ref string errorMessage)
        {
            return service.ChangePassword(userId, model, ref errorMessage);
        }

        public UserModel Login(LoginModel model, ref string errorMessage)
        {
            return service.Login(model, ref errorMessage);
        }

        public UserModel GetUser(int userId)
        {
            return service.GetUser(userId);
        }
    }
}
