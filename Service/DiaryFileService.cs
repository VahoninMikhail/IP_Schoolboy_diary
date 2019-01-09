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
    public class DiaryFileService : AbstractClassFileDiary
    {
        //   DatabaseDiaryContext db = new DatabaseDiaryContext();
        //   string currentPath = HttpContext.Current.Server.MapPath("~") + "/Files/Diaries";
        //   string currentPathSchool = HttpContext.Current.Server.MapPath("~") + "/Files/Schools";

        //   XmlSerializer xsSubmit = new XmlSerializer(typeof(Diary));
        //   XmlSerializer xsSubmitSchool = new XmlSerializer(typeof(School));

        new string Name = "Diary";
        new string currentPath = HttpContext.Current.Server.MapPath("~") + "/Files/Diaries";
        new string currentPathSchool = HttpContext.Current.Server.MapPath("~") + "/Files/Schools";
        new XmlSerializer xsSubmit = new XmlSerializer(typeof(Diary));
        new XmlSerializer xsSubmitSchool = new XmlSerializer(typeof(School));

        public DiaryFileService()
        {
            base.Name = Name;
            base.xsSubmit = xsSubmit;
            base.xsSubmitSchool = xsSubmitSchool;
            base.currentPath = currentPath;
            base.currentPathSchool = currentPathSchool;
        }
    }
}