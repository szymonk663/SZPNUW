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

        public bool RegisterAdmin(UserModel model, ref string errorMessage)
        {
            return service.RegisterAdmin(model, ref errorMessage);
        }

        public List<UserModel> GetAdmins()
        {
            return service.GetAdmins();
        }

        public bool UpdateAdmin(UserModel model, ref string errorMessage)
        {
            return service.UpdateAdmin(model, ref errorMessage);
        }

        public UserModel GetUserByUserId(int userId)
        {
            return service.GetUserByUserId(userId);
        }
    }
}
