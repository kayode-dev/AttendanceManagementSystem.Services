using AttendanceMangementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagemnetSystem.Services
{
    public class LevelRepository
    {
        //method to get all Levels
        public static IEnumerable<Level> GetLevels()
        {
            AMSDbContext db = new AMSDbContext();
            return db.Levels;
        }

        //method to get a level by its Id
        public static Level GetLevel(int Id)
        {
            AMSDbContext db = new AMSDbContext();
            var Level = db.Levels.Find(Id);

            if (Level != null)
            {
                return Level;
            }
            return null;
        }

        //method to add a level
        public static bool AddLevel(int LevelNo)
        {
            using (AMSDbContext db = new AMSDbContext())
            {
                if (!db.Levels.Any(a => a.LevelNo == LevelNo))
                {
                    Level lev = new Level
                    {
                        LevelNo = LevelNo
                    };
                    db.Levels.Add(lev);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Level already exist");
                }
                return true;
            }
        }

        //method to edit a level
        public static bool EditLevel(int Id, int LevelNo)
        {
            AMSDbContext db = new AMSDbContext();
            var LevelToUpdate = db.Levels.Find(Id);
            if (LevelToUpdate != null)
            {
                LevelToUpdate.Id = Id;
                LevelToUpdate.LevelNo = LevelNo;

                db.Entry(LevelToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Level already exist");
            }
            return true;
        }

        //method to delete a level
        public static bool DeleteLevel(int Id)
        {
            AMSDbContext db = new AMSDbContext();
            var LevelToDelete = db.Levels.Find(Id);
            if (LevelToDelete != null)
            {
                db.Levels.Remove(LevelToDelete);
                db.SaveChanges();
            }
            return true;
        }
    }
}
