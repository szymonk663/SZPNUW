using Microsoft.EntityFrameworkCore.Storage;
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
        public bool RegisterStudent(StudentModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                if (!context.Users.Where(x => x.Login == model.Login).Any())
                {
                    if (!context.Students.Where(x => x.Albumnumber == model.AlbumNumber).Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            Users user = new Users { Login = model.Login, Password = model.Password, Firstname = model.Password, Lastname = model.LastName, Pesel = model.PESEL, City = model.City, Address = model.Address, Dateofbirth = model.DateOfBirth, Usertype = (int)UserTypes.Student };
                            Students student = new Students { Albumnumber = model.AlbumNumber, };
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
                        return true;
                    }
                }
                errorMessage = ValidationMessages.UsedLogin;
                return false;
            }
        }
        public StudentModel GetStudentById(int id)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Students student = context.Students.Where(s => s.Id == id).FirstOrDefault();
                if (student == null)
                    return null;
                int semesterId = student.Studentsemester.OrderBy(x => x.Semester.Semesternumber).Last().Semesterid;
                StudentModel model = new StudentModel
                {
                    Id = student.Id,
                    Login = student.User.Login,
                    FirstName = student.User.Firstname,
                    LastName = student.User.Lastname,
                    PESEL = student.User.Pesel,
                    Address = student.User.Address,
                    City = student.User.City,
                    AlbumNumber = student.Albumnumber,
                    DateOfBirth = student.User.Dateofbirth,
                    UserId = student.Userid,
                    UserType = (UserTypes)student.User.Usertype,
                    SemesterId = semesterId
                };
                return model;
            }

        }
        public float GetStudentAverageRating(int sectionId, int studentId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                List<float?> list = context.Studentssections
                    .FirstOrDefault(x => x.Sectionid == sectionId && x.Studentid == studentId)
                    .Meetings
                    .Select(x => x.Rating)
                    .ToList();
                list.RemoveAll(item => item == null);
                if (list.Count != 0)
                {
                    float ave = 0;
                    foreach (float val in list)
                    {
                        ave += val;
                    }
                    ave /= list.Count;
                    return ave;
                }
                else return 0;
            }
        }
        public StudentSectionModel GetSectionStudent(int sectionId, int studentId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Studentssections studentSection = context.Studentssections
                    .FirstOrDefault(x => x.Sectionid == sectionId && x.Studentid == studentId);
                if(studentSection != null)
                {
                    return new StudentSectionModel
                    {
                        Id = studentSection.Id,
                        Date = studentSection.Dateofentry,
                        Rate = studentSection.Rating,
                        SectionId = studentSection.Sectionid,
                        StudentId = studentSection.Studentid
                    };
                }
                return null;
            }
        }
        public List<StudentModel> GetStudentBySemesterId(int semesterId)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                List<Students> students = context.Studentsemester.Where(x => x.Semesterid == semesterId).Select(x => x.Student).ToList();
                List<StudentModel> model = new List<StudentModel>();
                students.ForEach(x => model.Add(new StudentModel
                {
                    Id = x.Id,
                    Login = x.User.Login,
                    FirstName = x.User.Firstname,
                    LastName = x.User.Lastname,
                    PESEL = x.User.Pesel,
                    Address = x.User.Address,
                    City = x.User.City,
                    AlbumNumber = x.Albumnumber,
                    DateOfBirth = x.User.Dateofbirth,
                    UserId = x.Userid,
                    UserType = (UserTypes)x.User.Usertype,
                    SemesterId = semesterId
                }));
                return model;
            }
        }

        public bool UpdateStudent(StudentModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Students student = context.Students.FirstOrDefault(s => s.Id == model.Id);
                if(student != null)
                {
                    student.User.Login = model.Login;
                    student.User.Firstname = model.FirstName;
                    student.User.Lastname = model.LastName;
                    student.User.Pesel = model.PESEL;
                    student.User.Address = model.Address;
                    student.User.City = model.City;
                    student.Albumnumber = model.AlbumNumber;
                    student.User.Dateofbirth = model.DateOfBirth;
                    context.SaveChanges();
                    return true;
                }
                errorMessage = PortalMessages.NoSuchElement;
                return false;
            }
        }

        public bool UpdateStudentSection(StudentSectionModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                Studentssections studentSection = context.Studentssections.FirstOrDefault(x => x.Id == model.Id);
                if(studentSection != null)
                {
                    studentSection.Rating = model.Rate;
                    studentSection.Dateofentry = model.Date;
                    context.SaveChanges();
                    return true;
                }
                errorMessage = PortalMessages.NoSuchElement;
                return false;
            }
        }
        
        public void RewriteStudentsSemester(SemestersIdModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            using (IDbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<int> studentsId = context.Studentsemester.Where(x => x.Semesterid == model.SemesterId).Select(x => x.Studentid).ToList();
                    if (studentsId.AnyLazy())
                    {
                        foreach (int id in studentsId)
                        {
                            RewriteStudentSemester(context, id, model.NewSemesterId.Value, ref errorMessage);
                        }
                        transaction.Commit();
                    }
                    else
                        errorMessage = PortalMessages.NoStudentsOnSemester;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    errorMessage = e.Message;
                }
            }
        }

        public void RewriteStudentSemester(int studentId, int semesterId, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                RewriteStudentSemester(context, studentId, semesterId, ref errorMessage);
            }
        }

        private void RewriteStudentSemester(SZPNUWContext context, int studentId, int semesterId, ref string errorMessage)
        {
            if (CheckStudSem(context, studentId, semesterId, ref errorMessage))
            {
                Studentsemester studSem = new Studentsemester { Semesterid = semesterId, Studentid = studentId };
                context.Studentsemester.Add(studSem);
                context.SaveChanges();
            }
        }

        public void UpdateStudentsSemester(SemestersIdModel model, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            using (IDbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<int> studentsId = context.Studentsemester.Where(x => x.Semesterid == model.SemesterId).Select(x => x.Studentid).ToList();
                    if (studentsId.AnyLazy())
                    {
                        foreach (int studentId in studentsId)
                        {
                            UpdateStudentSemester(context, studentId, model.SemesterId.Value, model.NewSemesterId.Value, ref errorMessage);
                        }
                        transaction.Commit();
                    }
                    else
                        errorMessage = PortalMessages.NoStudentsOnSemester;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    errorMessage = e.Message;
                }
            }
        }

        public void UpdateStudentSemester(int studentId, int oldSemesterId, int newSemesterId, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                UpdateStudentSemester(context, studentId, oldSemesterId, newSemesterId, ref errorMessage);
            }
        }

        private void UpdateStudentSemester(SZPNUWContext context, int studentId, int oldSemesterId, int newSemesterId, ref string errorMessage)
        {
            if(CheckStudSem(context, studentId, newSemesterId, ref errorMessage))
            {
                Studentsemester studSem = context.Studentsemester.Where(x => x.Studentid == studentId && x.Semesterid == oldSemesterId).FirstOrDefault();
                if(studSem != null)
                {
                    studSem.Semesterid = newSemesterId;
                    context.SaveChanges();
                }
                else
                {
                    Students student = context.Students.FirstOrDefault(s => s.Id == studentId);
                    if (student != null)
                        errorMessage += PortalMessages.StudentSemesterCanNotChange.WithFormatExtesion(new string[] { student.User.Firstname, student.User.Lastname });
                    else
                        errorMessage += PortalMessages.NoSuchElement;
                }
            }
        }

        private bool CheckStudSem(SZPNUWContext context, int studentId, int semesterId, ref string errorMessage)
        {
            if (context.Studentsemester.Where(x => x.Studentid == studentId && x.Semesterid == semesterId).Any())
            {
                Students student = context.Students.Where(s => s.Id == studentId).FirstOrDefault();
                if(student != null)
                {
                    errorMessage += PortalMessages.StudentSemesterExist.WithFormatExtesion( new string[] { student.User.Firstname, student.User.Lastname });
                    return false;
                }
                errorMessage += PortalMessages.UserDoesNotExist;
                return false;
            }
            return true;
        }

        public void DeleteStudentsSemester(int semesterId, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<int> studentsId = context.Studentsemester.Where(x => x.Semesterid == semesterId).Select(x => x.Studentid).ToList();
                    foreach (int studentId in studentsId)
                    {
                        DeleteStudentSemester(context, studentId, semesterId, ref errorMessage);
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    errorMessage = e.Message;
                }
            }
        }

        public bool DeleteStudentSemester(int studentId, int semesterId, ref string errorMessage)
        {
            using (SZPNUWContext context = new SZPNUWContext())
            {
                return DeleteStudentSemester(context, studentId, semesterId, ref errorMessage);
            }
        }

        private bool DeleteStudentSemester(SZPNUWContext context, int studentId, int semesterId, ref string errorMessage)
        {
            Students student = context.Students.Where(s => s.Id == studentId).FirstOrDefault();
            if(student != null)
            {
                if (student.Studentsemester.Count <= 1)
                {
                    errorMessage += PortalMessages.StudentSemesterCanNotDelete.WithFormatExtesion(new string[] { student.User.Firstname, student.User.Lastname });
                    return false;
                }
                Studentsemester studSem = context.Studentsemester.Where(x => x.Studentid == studentId && x.Semesterid == semesterId).FirstOrDefault();
                context.Studentsemester.Remove(studSem);
                context.SaveChanges();
                return true;
            }
            errorMessage += PortalMessages.NoSuchElement;
            return false;
        }
    }
}
