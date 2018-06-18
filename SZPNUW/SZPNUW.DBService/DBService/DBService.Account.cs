using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SZPNUW.Base;
using SZPNUW.Base.Resources;
using SZPNUW.Data;
using SZPNUW.DBService.Model;

namespace SZPNUW.DBService
{
    public partial class DBService
    {
        public Auth Login(LoginModel model)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Users user = context.Users.FirstOrDefault(x => x.Login == model.UserName && x.Password == SecurityService.GetSHA256Hash(model.Password));
                if (user != null)
                {
                    return new Auth(true) { Id = user.Id, UserType = (UserTypes)user.Usertype };
                }
                return new Auth(ValidationMessages.WrongUserNameOrPassword);
            }
        }

        public bool ChangePassword(ChangePasswordModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Users user = context.Users.FirstOrDefault(s => s.Id == model.UserId);
                if(user != null)
                {
                    if(user.Password == model.OldPassword)
                    {
                        user.Password = SecurityService.GetSHA256Hash(model.NewPassword);
                        context.SaveChanges();
                        return true;
                    }
                    errorMessage = ValidationMessages.OldPasswordError;
                    return false;
                }
                errorMessage = PortalMessages.NoSuchElement;
                return false;
            }
        }

    }
}
