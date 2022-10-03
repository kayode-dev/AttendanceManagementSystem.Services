using AttendanceMangementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagemnetSystem.Services
{
    public class CourseAllocationRepository
    {
        public static IEnumerable<CourseAllocation> GetCoursesRegisteredByStudentId(string StudentId)
        {
            AMSDbContext db = new AMSDbContext();
            return db.CourseAllocations.Include("Student").Include("Course").Where(c => c.StudentId == StudentId).ToList();
        }

        public static IEnumerable<StudentDetail> GetStudentsByCourseId(string CourseId)
        {
            AMSDbContext db = new AMSDbContext();

            List<StudentDetail> students = new List<StudentDetail>();
            var CourseAllocations = db.CourseAllocations.Include("Student").Where(c => c.CourseId == CourseId).ToList();
            foreach (var courseall in CourseAllocations)
            {
                students.Add(courseall.Student);
            }
            return students.ToList();
        }
        //    return db.CourseAllocations.Include("Student").Include("Course").Where(c => c.CourseId == CourseId).ToList();
        //}

        public static void AddAllocatedCourse(List<CourseAllocation> newCoursesAllocation)
        {
            using (AMSDbContext db = new AMSDbContext())
            {

                foreach (var allocatedCourse in newCoursesAllocation)
                {
                    allocatedCourse.Id = Guid.NewGuid().ToString();
                    if (!db.CourseAllocations.Any(c => c.CourseId == allocatedCourse.CourseId &&
                    c.StudentId == allocatedCourse.StudentId))
                    {
                        db.CourseAllocations.Add(allocatedCourse);
                        db.SaveChanges();
                    }
                }

            }
        }
        public static void DeleteAllocatedCourse(string id)
        {
            using (AMSDbContext db = new AMSDbContext())
            {
                var courseAllocated = db.CourseAllocations.Find(id);

                if (courseAllocated != null)
                {
                    db.CourseAllocations.Remove(courseAllocated);
                    db.SaveChanges();
                }

            }

        }
    }
}
