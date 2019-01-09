using Schoolboy_diary.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Schoolboy_diary.Service
{
    public abstract class CrudDiary
    {
        public abstract Diary findDiaryId(int? id);

        public abstract void Edit(Diary diary);

        public abstract void Create(Diary diary);

        public abstract void Delete(int id);

        public abstract List<Diary> getList();

        public abstract SelectList DropDownCreate();

        public abstract SelectList DropDownEdit(int? id);

        public abstract List<Diary> GetDiary(int? searchingSchool, string searchingDate);
    }
}