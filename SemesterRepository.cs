using AttendanceMangementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagemnetSystem.Services
{
    public class SemesterRepository
    {
        //method to get all Semesters
        public static IEnumerable<Semester> GetSemesters()
        {
            AMSDbContext db = new AMSDbContext();
            return db.Semesters;
        }

        //method to get a semester by Id
        public static Semester GetSemester(int Id)
        {
            AMSDbContext db = new AMSDbContext();
            var Semester = db.Semesters.Find(Id);

            if (Semester != null)
            {
                return Semester;
            }
            return null;
        }

        //method to add a Semester
        public static bool AddSemester(string SemesterName)
        {
            using (AMSDbContext db = new AMSDbContext())
            {
                if (!db.Semesters.Any(s => s.semesterName == SemesterName))
                {
                    Semester sem = new Semester
                    {
                        semesterName = SemesterName
                    };
                    db.Semesters.Add(sem);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Semester already exist");
                }
                return true;
            }
        }

        //method to Edit a Semester
        public static bool EditSemester(int Id, string SemesterName)
        {
            AMSDbContext db = new AMSDbContext();
            var SemesterToUpdate = db.Semesters.Find(Id);
            if (SemesterToUpdate != null)
            {
                SemesterToUpdate.Id = Id;
                SemesterToUpdate.semesterName = SemesterName;

                db.Entry(SemesterToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Semester already exist");
            }
            return true;
        }

        //method to Delete a semester
        public static bool DeleteSemester(int Id)
        {
            AMSDbContext db = new AMSDbContext();
            var SemesterTODelete = db.Semesters.Find(Id);
            if (SemesterTODelete != null)
            {
                db.Semesters.Remove(SemesterTODelete);
                db.SaveChanges();
            }
            return true;
        }
    }
}
