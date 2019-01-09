using Schoolboy_diary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Schoolboy_diary.Service
{
    public class SchoolService : CrudSchool
    {
        DatabaseDiaryContext db = new DatabaseDiaryContext();

        public override void Delete(int id)
        {
            School b = db.Schools.Find(id);
            if (b != null)
            {
                db.Schools.Remove(b);
                db.SaveChanges();
            }
        }

        public override void Edit(School school)
        {
            db.Entry(school).State = EntityState.Modified;
            db.SaveChanges();
        }

        public override void Create(School school)
        {
            db.Schools.Add(school);
            db.SaveChanges();
        }

        public override School findSchoolId(int? id)
        {
            School school = db.Schools.Find(id);
            return school;
        }

        public override List<School> getList()
        {
            return db.Schools.ToList();
        }

  /*/      public override List<School> schoolList(int? name)
        {
            IQueryable<School> schools = db.Schools.Include(p => p.Name);
            List<School> names = db.Schools.ToList();
            SchoolListViewModel slvm = new SchoolListViewModel
            {
                Names = new SelectList(names, "Id", "Name"),
            };
            return names;
        }*/
    }
}