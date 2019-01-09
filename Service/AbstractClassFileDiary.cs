using Schoolboy_diary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Schoolboy_diary.Service
{
    public abstract class AbstractClassFileDiary : CrudDiary
    {
        //   DatabaseDiaryContext db = new DatabaseDiaryContext();
        //   string currentPath = HttpContext.Current.Server.MapPath("~") + "/Files/Diaries";
        //   string currentPathSchool = HttpContext.Current.Server.MapPath("~") + "/Files/Schools";

        //   XmlSerializer xsSubmit = new XmlSerializer(typeof(Diary));
        //   XmlSerializer xsSubmitSchool = new XmlSerializer(typeof(School));

        public XmlSerializer xsSubmit { get; set; }
        public XmlSerializer xsSubmitSchool { get; set; }
        public string currentPath { get; set; }
        public string currentPathSchool { get; set; }
        public String Name { get; set; }
      //  public String Name2 { get; set; }

        public override void Create(Diary diary)
        {
            int max = 0;
            foreach (var path in Directory.GetFiles(currentPath, "*", SearchOption.TopDirectoryOnly))
            {
                Match m = Regex.Match(path, @"" + Name + @"\d+");
                int currentId = Convert.ToInt32(m.Value.Replace(Name, ""));
                if (currentId > max)
                {
                    max = currentId;
                }
            }
            int id = max + 1;
            diary.Id = id;
            string newFilePath = currentPath + "/" + Name + id + ".txt";
            StringWriter txtWriter = new StringWriter();
            xsSubmit.Serialize(txtWriter, diary);
            File.WriteAllText(newFilePath, txtWriter.ToString());
            txtWriter.Close();
        }

        public override void Delete(int id)
        {
            File.Delete(currentPath + "/" + Name + id + ".txt");
        }

        public override void Edit(Diary diary)
        {
            StringWriter txtWriter = new StringWriter();
            xsSubmit.Serialize(txtWriter, diary);
            File.WriteAllText(currentPath + "/" + Name + diary.Id + ".txt", txtWriter.ToString());
            txtWriter.Close();

        }

        public override Diary findDiaryId(int? id)
        {
            Diary diary;
            using (StreamReader stream = new StreamReader(currentPath + "/" + Name + id + ".txt", true))
            {
                diary = (Diary)xsSubmit.Deserialize(stream);
                stream.Close();
            }
            return diary;
        }

        public override List<Diary> getList()
        {
            string[] filesPaths = Directory.GetFiles(currentPath, "*", SearchOption.TopDirectoryOnly);
            List<Diary> diaryList = new List<Diary>();
            foreach (string item in filesPaths)
            {
                StreamReader stream = new StreamReader(item, true);
                Diary diary = (Diary)xsSubmit.Deserialize(stream);
                diaryList.Add(diary);
                stream.Close();
            }
            return diaryList;
        }

        public override SelectList DropDownCreate()
        {
            string[] filesPaths = Directory.GetFiles(currentPathSchool, "*", SearchOption.TopDirectoryOnly);
            List<School> schoolList = new List<School>();
            foreach (string item in filesPaths)
            {
                StreamReader stream = new StreamReader(item, true);
                School school = (School)xsSubmitSchool.Deserialize(stream);
                schoolList.Add(school);
                stream.Close();
            }
            SelectList schools = new SelectList(schoolList, "Id", "Name");
            return schools;
        }

        public override SelectList DropDownEdit(int? id)
        {
            Diary diary;
            using (StreamReader stream = new StreamReader(currentPath + "/" + Name + id + ".txt", true))
            {
                diary = (Diary)xsSubmit.Deserialize(stream);
                stream.Close();
            }

            string[] filesPaths = Directory.GetFiles(currentPathSchool, "*", SearchOption.TopDirectoryOnly);
            List<School> schoolList = new List<School>();
            foreach (string item in filesPaths)
            {
                StreamReader stream = new StreamReader(item, true);
                School school = (School)xsSubmitSchool.Deserialize(stream);
                schoolList.Add(school);
                stream.Close();
            }
            SelectList schools = new SelectList(schoolList, "Id", "Name", diary.SchoolId); /////
            return schools;
        }

        public override List<Diary> GetDiary(int? searchingSchool, string searchingDate)
        {
            string[] filesPaths = Directory.GetFiles(currentPath, "*", SearchOption.TopDirectoryOnly);
            List<Diary> diaryList = new List<Diary>();
            foreach (string item in filesPaths)
            {
                StreamReader stream = new StreamReader(item, true);
                Diary diary = (Diary)xsSubmit.Deserialize(stream);
                diaryList.Add(diary);
                stream.Close();
            }
            var result = diaryList.Where(x => x.Date == searchingDate)
             .Where(x => x.SchoolId == searchingSchool).ToList();
            return result;
        }
    }
}