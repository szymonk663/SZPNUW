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

        public List<UserModel> GetAdmins()
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                return context.Users.OrderBy(p => p.Lastname).Where(x => x.Usertype == (int)UserTypes.Admin).Select(x => new UserModel
                {
                    UserId = x.Id,
                    Login = x.Login,
                    FirstName = x.Firstname,
                    LastName = x.Lastname,
                    PESEL = x.Pesel,
                    Address = x.Address,
                    City = x.City,
                    DateOfBirth = x.Dateofbirth,
                    UserType = (UserTypes)x.Usertype
                }).ToList();
            }
        }

        public bool RegisterAdmin(UserModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                if (!context.Users.Where(x => x.Login == model.Login).Any())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        Users user = new Users { Login = model.Login, Password = SecurityService.GetSHA256Hash(model.Password), Firstname = model.FirstName, Lastname = model.LastName, Pesel = model.PESEL, City = model.City, Address = model.Address, Dateofbirth = model.DateOfBirth, Usertype = (int)UserTypes.Admin };
                        try
                        {
                            context.Add(user);
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            errorMessage = PortalMessages.InsertDBError;
                        }
                    }
                    return true;
                }
                errorMessage = ValidationMessages.UsedLogin;
                return false;
            }
        }

        public bool UpdateAdmin(UserModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Users user = context.Users.FirstOrDefault(p => p.Id == model.UserId);
                if (user != null)
                {
                    user.Firstname = model.FirstName;
                    user.Lastname = model.LastName;
                    user.Pesel = model.PESEL;
                    user.Address = model.Address;
                    user.City = model.City;
                    user.Dateofbirth = model.DateOfBirth;
                    context.SaveChanges();
                    return true;
                }
                errorMessage = PortalMessages.NoSuchElement;
                return false;
            }
        }

        public UserModel GetUserByUserId(int id)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Users user = context.Users.FirstOrDefault(s => s.Id == id);
                if (user == null)
                    return null;
                UserModel model = new UserModel
                {
                    UserId = user.Id,
                    Login = user.Login,
                    FirstName = user.Firstname,
                    LastName = user.Lastname,
                    PESEL = user.Pesel,
                    Address = user.Address,
                    City = user.City,
                    DateOfBirth = user.Dateofbirth,
                    UserType = (UserTypes)user.Usertype,
                };
                return model;
            }
        }
    }
}
