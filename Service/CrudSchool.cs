using Schoolboy_diary.Models;
using System.Collections.Generic;

namespace Schoolboy_diary.Service
{
    public abstract class CrudSchool
    {
        public abstract School findSchoolId(int? id);

        public abstract void Edit(School school);

        public abstract void Create(School school);

        public abstract void Delete(int id);

        public abstract List<School> getList();

      //  public abstract List<School> schoolList(int? name);
    }
}