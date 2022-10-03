using AttendanceMangementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagemnetSystem.Services
{
    public class StudentDetailRepository
    {
         //method to get all students
        public static IEnumerable<StudentDetail> GetStudents()
        {
            AMSDbContext db = new AMSDbContext();
            return db.StudentDetails;
        }

        //method to get a single student by the Id
        public static StudentDetail GetStudent(string Id)
        {
            AMSDbContext db = new AMSDbContext();
            var Student = db.StudentDetails.Find(Id);
            if (Student != null)
            {
                return Student;
            }
            return null;
        }

        //method to add a student
        public static bool AddStudent(string FirstName, string MiddleName, string LastName,
            string DepartmentId)
        {
            using (AMSDbContext db = new AMSDbContext())
            {
                if (!db.StudentDetails.Any(s => s.FirstName == FirstName && s.MiddleName == MiddleName &&
                s.LastName == LastName))
                {
                    string Id = Guid.NewGuid().ToString();
                    StudentDetail studentDetail = new StudentDetail
                    {
                        Id = Id,
                        FirstName = FirstName,
                        MiddleName = MiddleName,
                        LastName = LastName,
                        DepartmentId = DepartmentId
                    };
                    db.StudentDetails.Add(studentDetail);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Student already exist");
                }
                return true;
            }
        }

        //method to Edit a student
        public static bool EditStudent(string Id,string FirstName, string MiddleName, string LastName,
            string DepartmentId)
        {
            AMSDbContext db = new AMSDbContext();
            if (!db.StudentDetails.Any(s => s.FirstName == FirstName && s.MiddleName == MiddleName &&
                s.LastName == LastName))
            {
                var StudentToUpdate = db.StudentDetails.Find(Id);
                if (StudentToUpdate != null)
                {
                    StudentToUpdate.Id = Id;
                    StudentToUpdate.FirstName = FirstName;
                    StudentToUpdate.MiddleName = MiddleName;
                    StudentToUpdate.LastName = LastName;
                    StudentToUpdate.DepartmentId = DepartmentId;


                    db.Entry(StudentToUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            else
            {
                throw new Exception("Student already exist");

            }
            return true;
        }

        //method to Delete student
        public static bool DeleteStudent(int Id)
        {
            AMSDbContext db = new AMSDbContext();
            var StudentToDelete = db.StudentDetails.Find(Id);
            if (StudentToDelete != null)
            {
                db.StudentDetails.Remove(StudentToDelete);
                db.SaveChanges();
            }
            return true;
        }
    }
}
