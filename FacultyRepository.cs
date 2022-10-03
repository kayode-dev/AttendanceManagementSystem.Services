using AttendanceMangementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagemnetSystem.Services
{
    public class FacultyRepository
    {
        //method to get all faculties
        public static IEnumerable<Faculty> GetFaculties()
        {
            AMSDbContext db = new AMSDbContext();
            return db.Faculties.ToList();
        }

        //method to get a single faculty by the Id
        public static Faculty GetFaculty(int Id)
        {
            AMSDbContext db = new AMSDbContext();
            var Faculties = db.Faculties.Find(Id);
            if (Faculties != null)
            {
                return Faculties;
            }
            return null;
        }

        //method to add a Faculty
        public static bool AddFaculty(string Name)
        {
            using (AMSDbContext db = new AMSDbContext())
            {
                if (!db.Faculties.Any(f => f.FacultyName == Name))
                {
                    Faculty fac = new Faculty
                    {
                        FacultyName = Name
                    };
                    db.Faculties.Add(fac);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Faculty already exist");
                }
                return true;
            }
        }

        //method to Edit a Faculty
        public static bool EditFaculty(int Id, string Name)
        {
            AMSDbContext db = new AMSDbContext();
            if (!db.Faculties.Any(f => f.FacultyName == Name))
            {
                var FacultyToUpdate = db.Faculties.Find(Id);
                if (FacultyToUpdate != null)
                {
                    FacultyToUpdate.Id = Id;
                    FacultyToUpdate.FacultyName = Name;

                    db.Entry(FacultyToUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            else
            {
                throw new Exception("Faculty already exist");

            }
            return true;
        }

        //method to Delete Faculty
        public static bool DeleteFaculty(int Id)
        {
            AMSDbContext db = new AMSDbContext();
            var FacultyToDelete = db.Faculties.Find(Id);
            if (FacultyToDelete != null)
            {
                db.Faculties.Remove(FacultyToDelete);
                db.SaveChanges();
            }
            return true;
        }
    }
}
