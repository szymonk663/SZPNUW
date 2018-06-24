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
        public bool RegisterInstructor(InstructorModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                if (!context.Users.Where(x => x.Login == model.Login).Any())
                {
                    if (!context.Lecturers.Where(x => x.Code == model.Code).Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            Users user = new Users { Login = model.Login, Password = SecurityService.GetSHA256Hash(model.Password), Firstname = model.Password, Lastname = model.LastName, Pesel = model.PESEL, City = model.City, Address = model.Address, Dateofbirth = model.DateOfBirth, Usertype = (int)UserTypes.Student };
                            Lecturers lecturer = new Lecturers { Code = model.Code };
                            user.Lecturers = lecturer;
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
                }
                errorMessage = ValidationMessages.UsedLogin;
                return false;
            }
        }

        public List<InstructorModel> GetInstructors()
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                return context.Lecturers.OrderBy(p => p.User.Lastname).Select(x => new InstructorModel
                    {
                        Id = x.Id,
                        Login = x.User.Login,
                        FirstName = x.User.Firstname,
                        LastName = x.User.Lastname,
                        PESEL = x.User.Pesel,
                        Address = x.User.Address,
                        City = x.User.City,
                        Code = x.Code,
                        DateOfBirth = x.User.Dateofbirth,
                        UserId = x.Userid,
                        UserType = (UserTypes)x.User.Usertype
                    }).ToList();
            }
        }

        public InstructorModel GetInstructorById(int id)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Lecturers lecturer = context.Lecturers.Where(s => s.Id == id).FirstOrDefault();
                if (lecturer == null)
                    return null;
                InstructorModel model = new InstructorModel
                {
                    Id = lecturer.Id,
                    Login = lecturer.User.Login,
                    FirstName = lecturer.User.Firstname,
                    LastName = lecturer.User.Lastname,
                    PESEL = lecturer.User.Pesel,
                    Address = lecturer.User.Address,
                    City = lecturer.User.City,
                    Code = lecturer.Code,
                    DateOfBirth = lecturer.User.Dateofbirth,
                    UserId = lecturer.Userid,
                    UserType = (UserTypes)lecturer.User.Usertype,
                };
                return model;
            }
        }

        public InstructorModel GetInstructorByUserId(int id)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Lecturers lecturer = context.Lecturers.Include(x => x.User).FirstOrDefault(s => s.User.Id == id);
                if (lecturer == null)
                    return null;
                InstructorModel model = new InstructorModel
                {
                    Id = lecturer.Id,
                    Login = lecturer.User.Login,
                    FirstName = lecturer.User.Firstname,
                    LastName = lecturer.User.Lastname,
                    PESEL = lecturer.User.Pesel,
                    Address = lecturer.User.Address,
                    City = lecturer.User.City,
                    Code = lecturer.Code,
                    DateOfBirth = lecturer.User.Dateofbirth,
                    UserId = lecturer.Userid,
                    UserType = (UserTypes)lecturer.User.Usertype,
                };
                return model;
            }
        }

        public bool UpdateInstructor(InstructorModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Lecturers lecturer = context.Lecturers.Include(x => x.User).FirstOrDefault(p => p.Id == model.Id);
                if(lecturer != null)
                {
                    lecturer.User.Firstname = model.FirstName;
                    lecturer.User.Lastname = model.LastName;
                    lecturer.User.Pesel = model.PESEL;
                    lecturer.User.Address = model.Address;
                    lecturer.User.City = model.City;
                    lecturer.Code = model.Code;
                    lecturer.User.Dateofbirth = model.DateOfBirth;
                    context.SaveChanges();
                    return true;
                }
                errorMessage = PortalMessages.NoSuchElement;
                return false;
            }
        }
    }
}
