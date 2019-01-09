using Schoolboy_diary.Service;

namespace Schoolboy_diary.Models
{
    public class School
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Grade { get; set; }

    /*    public ICollection<Diary> Diaries { get; set; }
        public School()
        {
            Diaries = new List<Diary>();
        }*/
    }
}