using AttendanceMangementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagemnetSystem.Services
{
    public class AttendanceRepository
    {
        public static IEnumerable<Attendance> GetAttendances()
        {
            AMSDbContext db = new AMSDbContext();
            return db.Attendances.ToList();
        }

        public static IEnumerable<Attendance> GetAttendancesByCourseIdandStudentId(string CourseId, string StudentId)
        {
            AMSDbContext db = new AMSDbContext();
            return db.Attendances.Where(a => a.CourseId == CourseId && a.StudentId == StudentId).ToList();
        }

        public static IEnumerable<Attendance> GetAttendancesByCourseId(string CourseId)
        {
            AMSDbContext db = new AMSDbContext();
            return db.Attendances.Where(a => a.CourseId == CourseId).ToList();
        }

        public static void AddAttendanceList (List<Attendance> newattendances)
        {
            using (AMSDbContext db = new AMSDbContext())
            {
                foreach (var attendanceTaken in newattendances)
                {
                    attendanceTaken.Id = Guid.NewGuid().ToString();

                    db.Attendances.Add(attendanceTaken);
                    db.SaveChanges();
                }
            }
        }
    }
}
