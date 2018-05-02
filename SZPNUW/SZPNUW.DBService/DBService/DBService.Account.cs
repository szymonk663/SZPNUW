using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SZPNUW.Base.Resources;
using SZPNUW.Data;
using SZPNUW.DBService.Model;

namespace SZPNUW.DBService
{
    public partial class DBService
    {
        public string RegisterStudent(StudentModel model)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                if(!context.Users.Where(x => x.Login == model.Login).Any())
                {
                    if(!context.Students.Where(x => x.Albumnumber == model.AlbumNumber).Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            Users user = new Users { Login = model.Login, Password = model.Password, Firstname = model.Password, Lastname = model.LastName, Pesel = model.PESEL, City = model.City, Address = model.Address, Dateofbirth = model.DateOfBirth, Usertype = (int)UserTypes.Student };
                            Students student = new Students { Albumnumber = model.AlbumNumber,  };
                            user.Students = student;
                            try
                            {
                                Semesters semester = context.Semesters.First(x => x.Id == model.SemesterId);
                                student.Studentsemester.Add(new Studentsemester { Semester = semester });
                                context.Add(user);
                                context.SaveChanges();
                                transaction.Commit();
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                throw new Exception(PortalMessages.InsertDBError);
                            }
                        }
                        return null;
                    }
                }
                return ValidationMessages.UsedLogin;
            }
        }
    }
}
