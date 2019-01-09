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
    public class SchoolFileService : AbstractClassFileSchool
    {
        new string Name = "School";
        new string currentPath = HttpContext.Current.Server.MapPath("~") + "/Files/Schools";
        new XmlSerializer xsSubmit = new XmlSerializer(typeof(School));

        public SchoolFileService()
        {
            base.Name = Name;
            base.xsSubmit = xsSubmit;
            base.currentPath = currentPath;
        }

        /*    public override List<School> schoolList(int? name)
            {
                List<School> names = db.Schools.ToList();
                SchoolListViewModel slvm = new SchoolListViewModel
                {
                    Names = new SelectList(names, "Id", "Name"),
                };
                return names;
            }*/
    }
}