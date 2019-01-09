using Schoolboy_diary.Models;
using Schoolboy_diary.Service;
using System.Configuration;
using System.Web.Mvc;

namespace Schoolboy_diary.Controllers
{
    public class SchoolController : Controller
    {
        CrudSchool schoolService;

        public SchoolController()
        {
            string dataStore = ConfigurationManager.AppSettings["DataStore"].ToString();
            switch (dataStore)
            {
                case "DB":
                    schoolService = new SchoolService();
                    break;
                case "File":
                    schoolService = new SchoolFileService();
                    break;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditSchool(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            if (schoolService.findSchoolId(id) != null)
            {
                return View(schoolService.findSchoolId(id));
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult UpdateSchool(School school)
        {
            schoolService.Edit(school);
            return RedirectToAction("Schools");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateSchool()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreateSchool(School school)
        {
            schoolService.Create(school);
            return RedirectToAction("Schools");
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteSchool(int id)
        {
            schoolService.Delete(id);
            return RedirectToAction("Schools");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Schools()
        {
          //  return View(schoolService.schoolList(name));
            return View(schoolService.getList());
        }

      /*  protected override void Dispose(bool disposing)
        {
            schoolService.Dispose();
            base.Dispose(disposing);
        } */
    }
}