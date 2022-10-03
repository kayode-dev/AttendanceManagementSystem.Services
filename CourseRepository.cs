using AttendanceMangementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagemnetSystem.Services
{
    public class CourseRepository
    {
        //method to get all courses
        public static IEnumerable<Course> GetCourses()
        {
            AMSDbContext db = new AMSDbContext();
            return db.Courses;
        }

        //method to get a single course by the Id
        public static Course GetCourse(string Id)
        {
            AMSDbContext db = new AMSDbContext();
            var Courses = db.Courses.Find(Id);
            if (Courses != null)
            {
                return Courses;
            }
            return null;
        }

        public static IEnumerable<Course> GetCoursesBydepartmentId(string DepartmentId)
        {
            AMSDbContext db = new AMSDbContext();
            IEnumerable<Course> courses = null;
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                courses = db.Courses.Where(c => c.DepartmentId == DepartmentId);
            }
            return courses;
        }

        //method to add a course
        public static bool AddCourse(string CourseCode, string CourseTitle, string DepartmentId, int LevelId, int SemesterId, int NoOfLectures)
        {
            using (AMSDbContext db = new AMSDbContext())
            {
                if (!db.Courses.Any(d => d.CourseCode == CourseCode && d.CourseTitle == CourseTitle))
                {
                    string Id = Guid.NewGuid().ToString();
                    Course cour = new Course
                    {
                        Id = Id,
                        CourseCode = CourseCode,
                        CourseTitle = CourseTitle,
                        DepartmentId = DepartmentId,
                        LevelId = LevelId,
                        SemesterId = SemesterId,
                        NoOfLectures = NoOfLectures,
                        NoOfLecturesTaken = 0
                    };
                    db.Courses.Add(cour);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Course already exist");
                }
                return true;
            }
        }

        //method to Edit a course
        public static bool EditCourse(string Id, string CourseCode, string CourseTitle, string DepartmentId,
            int LevelId, int SemesterId)
        {
            AMSDbContext db = new AMSDbContext();
            if (!db.Courses.Any(d => d.CourseCode == CourseCode && d.CourseTitle == CourseTitle))
            {
                var CourseToUpdate = db.Courses.Find(Id);
                if (CourseToUpdate != null)
                {
                    CourseToUpdate.Id = Id;
                    CourseToUpdate.CourseCode = CourseCode;
                    CourseToUpdate.CourseTitle = CourseTitle;
                    CourseToUpdate.DepartmentId = DepartmentId;
                    CourseToUpdate.LevelId = LevelId;
                    CourseToUpdate.SemesterId = SemesterId;

                    db.Entry(CourseToUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            else
            {
                throw new Exception("Course already exist");

            }
            return true;
        }

        public static bool EditCourseNoOfLecturesTaken(string Id)
        {
            AMSDbContext db = new AMSDbContext();

            var CourseToUpdate = db.Courses.Find(Id);

            //int LecturestakenUpdate = CourseToUpdate.NoOfLecturesTaken + 1;
            if (CourseToUpdate != null)
            {
                CourseToUpdate.Id = Id;
                CourseToUpdate.NoOfLecturesTaken = CourseToUpdate.NoOfLecturesTaken + 1;

                db.Entry(CourseToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return true;
        }

        //method to Delete Course
        public static bool DeleteCourse(int Id)
        {
            AMSDbContext db = new AMSDbContext();
            var CourseToDelete = db.Courses.Find(Id);
            if (CourseToDelete != null)
            {
                db.Courses.Remove(CourseToDelete);
                db.SaveChanges();
            }
            return true;
        }
    }
}
