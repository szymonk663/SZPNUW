using Microsoft.EntityFrameworkCore;
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
        public UserModel Login(LoginModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Users user = context.Users.Include(x => x.Students).Include(x => x.Lecturers).FirstOrDefault(x => x.Login == model.UserName && x.Password == SecurityService.GetSHA256Hash(model.Password));
                int? pId = null;
                if (user != null)
                {
                    if (user.Lecturers != null)
                        pId = user.Lecturers.Id;
                    else if (user.Students != null)
                        pId = user.Students.Id;
                    return new UserModel() { PId = pId, UserId = user.Id, Login = user.Login, FirstName = user.Firstname, LastName = user.Lastname, Address = user.Address, City = user.Address, DateOfBirth = user.Dateofbirth, PESEL = user.Pesel, UserType = (UserTypes)user.Usertype };
                }
                errorMessage = ValidationMessages.WrongUserNameOrPassword;
                return null;
            }
        }

        public bool ChangePassword(int userId, ChangePasswordModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Users user = context.Users.FirstOrDefault(s => s.Id == userId);
                if(user != null)
                {
                    if(user.Password == SecurityService.GetSHA256Hash(model.OldPassword))
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

        public UserModel GetUser(int userId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Users user = context.Users.FirstOrDefault(s => s.Id == userId);
                if (user != null)
                {
                    return new UserModel {
                        UserId = user.Id,
                        Login = user.Login,
                        FirstName = user.Firstname,
                        LastName = user.Lastname,
                        Address = user.Address,
                        City = user.City,
                        DateOfBirth = user.Dateofbirth,
                        PESEL = user.Pesel,
                        UserType = (UserTypes)user.Usertype
                    };
                }
                return null;
            }
        }

    }
}
