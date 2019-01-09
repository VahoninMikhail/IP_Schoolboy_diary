using Schoolboy_diary.Service;

namespace Schoolboy_diary.Models
{
    public class Diary
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public int NumLesson { get; set; }

        public string NameLesson { get; set; }

        public int Mark { get; set; }

        public string Homework { get; set; }

        public string Note { get; set; }

        public int? SchoolId { get; set; }

      //  public School School { get; set; }
    }
}