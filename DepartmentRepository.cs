using AttendanceMangementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagemnetSystem.Services
{
    public class DepartmentRepository
    {
        //function to get all departments
        public static IEnumerable<Department> GetDepartments()
        {
            AMSDbContext db = new AMSDbContext();
            return db.Departments.Include("Faculty").ToList();
        }

        //function to get a single department by the Id
        public static Department GetDepartment(string Id)
        {
            using (AMSDbContext db = new AMSDbContext())
            {
                return db.Departments.Include("Faculty").SingleOrDefault(d => d.Id == Id);
            }
        }

        public static IEnumerable<Department> GetDepartmentsByFacultyId(int facultyId)
        {
            AMSDbContext db = new AMSDbContext();
            return db.Departments.Where(d => d.FacultyId == facultyId).ToList();
        }

        //function to add a Department

        public static bool AddDepartment(int FacultyId, string DepartmentName)
        {
            AMSDbContext db = new AMSDbContext();
            if (!db.Departments.Any(d => d.DepartmentName == DepartmentName))
            {
                string Id = Guid.NewGuid().ToString();
                Department dept = new Department
                {
                    Id = Id,
                     FacultyId = FacultyId,
                     DepartmentName = DepartmentName

                };
                db.Departments.Add(dept);
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Department already exist");
            }
            return true;
        }

        //function to Edit a Department
        public static bool EditDepartment(string Id, string Name, int FacultyId)
        {
            AMSDbContext db = new AMSDbContext();
            if (!db.Departments.Any(d => d.DepartmentName == Name))
            {
                var DepartmentToUpdate = db.Departments.Find(Id);
                if (DepartmentToUpdate != null)
                {
                    DepartmentToUpdate.Id = Id;
                    DepartmentToUpdate.DepartmentName = Name;
                    DepartmentToUpdate.FacultyId = FacultyId;

                    db.Entry(DepartmentToUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            else
            {
                throw new Exception("Department already exist");

            }
            return true;
        }

        //function to Delete Faculty
        public static bool DeleteDepartment(string Id)
        {
            AMSDbContext db = new AMSDbContext();
            var DepartmentToDelete = db.Departments.Find(Id);
            if (DepartmentToDelete != null)
            {
                db.Departments.Remove(DepartmentToDelete);
                db.SaveChanges();
            }
            return true;
        }
    }
}
