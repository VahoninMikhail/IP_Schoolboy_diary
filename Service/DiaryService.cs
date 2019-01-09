using Schoolboy_diary.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Schoolboy_diary.Service
{
    public class DiaryService : CrudDiary
    {
        DatabaseDiaryContext db = new DatabaseDiaryContext();

        public override void Delete(int id)
        {
            Diary b = db.Diaries.Find(id);
            if (b != null)
            {
                db.Diaries.Remove(b);
                db.SaveChanges();
            }
        }

        public override void Edit(Diary diary)
        {
            db.Entry(diary).State = EntityState.Modified;
            db.SaveChanges();
        }

        public override void Create(Diary diary)
        {
            db.Diaries.Add(diary);
            db.SaveChanges();
        }

        public override Diary findDiaryId(int? id)
        {
            Diary diary = db.Diaries.Find(id);
            return diary;
        }

        public override List<Diary> getList()
        {
            return db.Diaries.ToList();
        }

        public override SelectList DropDownCreate()
        {
            SelectList schools = new SelectList(db.Schools, "Id", "Name");
            return schools;
        }

        public override SelectList DropDownEdit(int? id)
        {
            Diary diary = db.Diaries.Find(id);
            SelectList schools = new SelectList(db.Schools, "Id", "Name", diary.SchoolId);
            return schools;
        }

        public override List<Diary> GetDiary(int? searchingSchool, string searchingDate)
        {
            var result = db.Diaries.Where(x => x.Date == searchingDate)
            .Where(x => x.SchoolId == searchingSchool).ToList();
            return result;
            //  var result = db.Diaries.Where(x => x.SchoolId.Contains(searchingSchool) || searchingSchool == null).ToList();
        }
    }
}