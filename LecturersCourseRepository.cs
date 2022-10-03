using AttendanceMangementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagemnetSystem.Services
{
    public class LecturersCourseRepository
    {
        public static IEnumerable<LecturersCourse> GetLecturersCourses()
        {
            AMSDbContext db = new AMSDbContext();
            return db.LecturersCourses;
        }

        public static LecturersCourse GetShowTicketType(string Id)
        {
            AMSDbContext db = new AMSDbContext();
            var LecturersCourses = db.LecturersCourses.Find(Id);
            if (LecturersCourses != null)
            {
                return LecturersCourses;
            }
            return null;
        }

        public static IEnumerable<LecturersCourse> GetLecturersCoursesByLecturerId(string LecturerId)
        {
            AMSDbContext db = new AMSDbContext();
            IEnumerable<LecturersCourse> lecturersCourses = null;
            if (!string.IsNullOrEmpty(LecturerId))
            {
                lecturersCourses = db.LecturersCourses.Include("Course").Where(st => st.LecturerId == LecturerId);
            }
            return lecturersCourses;
            
        }

        ////public static ICollection<Show_TicketType> GetShow_PriceByShowIdandTicketType(string ShowId, int TicketId)
        ////{
        ////    FrvkyDbContext db = new FrvkyDbContext();
        ////    var Show_TicketTypesByShow = db.Show_TicketTypes.Where(st => st.ShowId == ShowId && st.TicketTypeId == TicketId).ToList();
        ////    if (Show_TicketTypesByShow != null)
        ////    {
        ////        return Show_TicketTypesByShow;
        ////    }
        ////    return null;
        ////}

        public static bool AddLecturersCourses(string LecturerId, string CourseId)
        {
            string Id = Guid.NewGuid().ToString();
            using (AMSDbContext db = new AMSDbContext())
                if (!db.LecturersCourses.Any(lc => lc.LecturerId == LecturerId && lc.CourseId == CourseId))
                {
                    LecturersCourse lc = new LecturersCourse
                    {
                        Id = Id,
                        LecturerId = LecturerId,
                        CourseId = CourseId
                    };
                    db.LecturersCourses.Add(lc);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("This course has already been allocated to this lecturer ");
                }
            return true;
        }

        //public static bool EditShow_TicketType(string Id, string ShowId, int TicketId, int Price, int Quantity)
        //{
        //    using (FrvkyDbContext db = new FrvkyDbContext())
        //    {
        //        var Show_TicketTypeToUpdate = db.Show_TicketTypes.Find(Id);
        //        if (Show_TicketTypeToUpdate != null)
        //        {
        //            Show_TicketTypeToUpdate.Id = Id;
        //            Show_TicketTypeToUpdate.ShowId = ShowId;
        //            Show_TicketTypeToUpdate.TicketTypeId = TicketId;
        //            Show_TicketTypeToUpdate.Price = Price;
        //            Show_TicketTypeToUpdate.Quantity = Quantity;

        //            db.Entry(Show_TicketTypeToUpdate).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //        return true;

        //    }
        //}

        public static bool DeleteLecturerCourse(string Id)
        {
            AMSDbContext db = new AMSDbContext();
            var Show_TicketTypeToDelete = db.LecturersCourses.Find(Id);
            if (Show_TicketTypeToDelete != null)
            {
                db.LecturersCourses.Remove(Show_TicketTypeToDelete);
                db.SaveChanges();
            }
            return true;
        }
    }
}
