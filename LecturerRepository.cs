using AttendanceMangementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagemnetSystem.Services
{
    public class LecturerRepository
    {
        //method to get all lecturers
        public static IEnumerable<Lecturer> GetLecturers()
        {
            AMSDbContext db = new AMSDbContext();
            return db.Lecturers;
        }

        //method to get a single lectuerer by the Id
        public static Lecturer GetLecturer(int Id)
        {
            AMSDbContext db = new AMSDbContext();
            var Lecturers = db.Lecturers.Find(Id);
            if (Lecturers != null)
            {
                return Lecturers;
            }
            return null;
        }

        public static IEnumerable<Lecturer> GetLecturersByDepartmentId(string DepartmentId)
        {
            AMSDbContext db = new AMSDbContext();
            IEnumerable<Lecturer> lecturers = null;
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                lecturers = db.Lecturers.Where(l => l.DepartmentId == DepartmentId);
            }
            return lecturers;
        }

        public static Lecturer GetLecturerByUserNameAndPassWord(string UserName, string PassWord)
        {
            AMSDbContext db = new AMSDbContext();
            var Lecturer = db.Lecturers.Where(l => l.UserName == UserName && l.Password == PassWord).FirstOrDefault();
            if (Lecturer != null)
            {
                return Lecturer;
            }
            return null;
        }

        //method to add a lecturer
        public static bool AddLecturer(string Title, string FirstName, string MiddleName, string LastName,
            string DepartmentId, string UserName, string PassWord)
        {
            using (AMSDbContext db = new AMSDbContext())
            {
                if (!db.Lecturers.Any(l => l.FirstName == FirstName && l.MiddleName == MiddleName &&
                l.LastName == LastName ||( l.UserName == UserName && l.Password == PassWord)))
                {
                    string Id = Guid.NewGuid().ToString();
                    Lecturer lect = new Lecturer
                    {
                        Id = Id,
                        Title = Title,
                        FirstName = FirstName,
                        MiddleName = MiddleName,
                        LastName = LastName,
                        DepartmentId = DepartmentId,
                        UserName = UserName,
                        Password = PassWord
                    };
                    db.Lecturers.Add(lect);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Lecturer already exist");
                }
                return true;
            }
        }

        //method to Edit a lecturer
        public static bool EditLecturer(string Id, string Title, string FirstName, string MiddleName, string LastName,
            string DepartmentId)
        {
            AMSDbContext db = new AMSDbContext();
            if (!db.Lecturers.Any(l => l.FirstName == FirstName && l.MiddleName == MiddleName &&
                l.LastName == LastName))
            {
                var LecturerToUpdate = db.Lecturers.Find(Id);
                if (LecturerToUpdate != null)
                {
                    LecturerToUpdate.Id = Id;
                    LecturerToUpdate.Title = Title;
                    LecturerToUpdate.FirstName = FirstName;
                    LecturerToUpdate.MiddleName = MiddleName;
                    LecturerToUpdate.LastName = LastName;
                    LecturerToUpdate.DepartmentId = DepartmentId;


                    db.Entry(LecturerToUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            else
            {
                throw new Exception("Lecturer already exist");

            }
            return true;
        }

        //method to Delete lecturer
        public static bool DeleteLecturer(int Id)
        {
            AMSDbContext db = new AMSDbContext();
            var LecturerToDelete = db.Lecturers.Find(Id);
            if (LecturerToDelete != null)
            {
                db.Lecturers.Remove(LecturerToDelete);
                db.SaveChanges();
            }
            return true;
        }
    }
}
