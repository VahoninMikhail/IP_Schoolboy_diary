using Schoolboy_diary.Models;
using Schoolboy_diary.Service;
using System.Configuration;
using System.Web.Mvc;

namespace Schoolboy_diary.Controllers
{
    public class DiaryController : Controller
    {
        CrudDiary diaryService;

        public DiaryController()
        {
            string dataStore = ConfigurationManager.AppSettings["DataStore"].ToString();
            switch (dataStore)
            {
                case "DB":
                    diaryService = new DiaryService();
                    break;
                case "File":
                    diaryService = new DiaryFileService();
                    break;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditDiary(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            if (diaryService.findDiaryId(id) != null)
            {
                ViewBag.Schools = diaryService.DropDownEdit(id);
                return View(diaryService.findDiaryId(id));
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult UpdateDiary(Diary diary)
        {
            diaryService.Edit(diary);
            return RedirectToAction("Diaries");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateDiary()
        {
            ViewBag.Schools = diaryService.DropDownCreate();
        //  ViewBag.Schools = schools;
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreateDiary(Diary diary)
        {
            diaryService.Create(diary);
            return RedirectToAction("Diaries");
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteDiary(int id)
        {
            diaryService.Delete(id);
            return RedirectToAction("Diaries");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Diaries()
        {
            // return View(diaryService.GetDiary(searchingName, searchingDate));
             return View(diaryService.getList());
          //  string searchingName, string searchingDate
        }

        [HttpGet]
        public ActionResult DiariesUser(int? searchingSchool, string searchingDate)
        {
            ViewBag.searchingSchool = diaryService.DropDownCreate();
            //  ViewBag.Schools = diaryService.GetDiary(searchingSchool);
            // return View(diaryService.GetDiary(searchingName, searchingDate));

               //return View(diaryService.getList());

            return View();

            //  string searchingName, string searchingDate
        }

        [HttpPost]
        public ActionResult DiarySearch(int? searchingSchool, string searchingDate)
        {
            return View(diaryService.GetDiary(searchingSchool, searchingDate));
        }

        public JsonResult JsonSearch(int? searchingSchool, string searchingDate)
        {
            return Json(diaryService.GetDiary(searchingSchool, searchingDate));
        }


        /*     protected override void Dispose(bool disposing)
             {
                 diaryService.Dispose();
                 base.Dispose(disposing);
             }*/
    }
}