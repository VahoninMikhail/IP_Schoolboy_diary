using Schoolboy_diary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Serialization;

namespace Schoolboy_diary.Service
{
    public abstract class AbstractClassFileSchool : CrudSchool
    {
        //   string currentPath = HttpContext.Current.Server.MapPath("~") + "/Files/Schools";
        //   XmlSerializer xsSubmit = new XmlSerializer(typeof(School));

        public XmlSerializer xsSubmit { get; set; }
        public string currentPath { get; set; }
        public String Name { get; set; }


        public override void Create(School school)
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
            school.Id = id;
            string newFilePath = currentPath + "/" + Name + id + ".txt";
            StringWriter txtWriter = new StringWriter();
            xsSubmit.Serialize(txtWriter, school);
            File.WriteAllText(newFilePath, txtWriter.ToString());
            txtWriter.Close();
        }

        public override void Delete(int id)
        {
            File.Delete(currentPath + "/" + Name + id + ".txt");
        }

        public override void Edit(School school)
        {
            StringWriter txtWriter = new StringWriter();
            xsSubmit.Serialize(txtWriter, school);
            File.WriteAllText(currentPath + "/" + Name + school.Id + ".txt", txtWriter.ToString());
            txtWriter.Close();
        }

        public override School findSchoolId(int? id)
        {
            School school;
            using (StreamReader stream = new StreamReader(currentPath + "/" + Name + id + ".txt", true))
            {
                school = (School)xsSubmit.Deserialize(stream);
                stream.Close();
            }
            return school;
        }

        public override List<School> getList()
        {
            string[] filesPaths = Directory.GetFiles(currentPath, "*", SearchOption.TopDirectoryOnly);
            List<School> schoolList = new List<School>();
            foreach (string item in filesPaths)
            {
                StreamReader stream = new StreamReader(item, true);
                School school = (School)xsSubmit.Deserialize(stream);
                schoolList.Add(school);
                stream.Close();
            }
            return schoolList;
        }
    }
}