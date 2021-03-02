using DynamoFitness.Controllers;
using GymDataAccess.Common;
using GymDataAccess.Entities.Custom;
using GymDataAccess.Entities.Model;
using GymDataAccess.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamoFitness.Areas.Admin.Controllers
{
    public class NoticeController : BaseController
    {
        public NoticeController(IUnitOfWork _service) : base(_service)
        {

        }
        // GET: Admin/Notice
        public ActionResult Notice()
        {
            CommonCls commonCls = new CommonCls();
            if (commonCls.getUserIdFromSession() == 0)
            {
                return Redirect("/Home/Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]

        public ActionResult NewNotice(string param = "")
        {
            NoticeModel noticeModel = JsonConvert.DeserializeObject<NoticeModel>(param);

            long result = 0;

            if (noticeModel.NoticeId != null)
            {
                result = _service.NoticeRepository.UpdateNotice(noticeModel);
            }
            else
            {
                result = _service.NoticeRepository.InsertNotice(noticeModel);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string NoticeList(string param = "")
        {
            NoticeCustom noticecustom = JsonConvert.DeserializeObject<NoticeCustom>(param);
            noticecustom = _service.NoticeRepository.GetNoticeList(noticecustom);
            return JsonConvert.SerializeObject(noticecustom);

        }

        [HttpGet]
        public ActionResult DeleteNotice(string param = "")
        {
            NoticeModel noticeModel = JsonConvert.DeserializeObject<NoticeModel>(param);
            long result = 0;
            result = _service.NoticeRepository.DeleteNoticeDetails(noticeModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string GetNoticeDetailsById(int Id)
        {
            NoticeModel noticeModel = new NoticeModel();
            noticeModel = _service.NoticeRepository.GetNoticeDetailsById(Id);
            return JsonConvert.SerializeObject(noticeModel);
        }

        
        public ActionResult UpdateViewNoticeEmployee(string param = "")
        {
            NoticeModel noticeModel = JsonConvert.DeserializeObject<NoticeModel>(param);

            long result = 0;          
            result = _service.NoticeRepository.ViewNoticeUpdateEmployee(noticeModel);         
             return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateViewNoticeCustomer(string param = "")
        {
            NoticeModel noticeModel = JsonConvert.DeserializeObject<NoticeModel>(param);

            long result = 0;
            result = _service.NoticeRepository.ViewNoticeUpdateCustomer(noticeModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateViewNoticeTrainer(string param = "")
        {
            NoticeModel noticeModel = JsonConvert.DeserializeObject<NoticeModel>(param);

            long result = 0;
            result = _service.NoticeRepository.ViewNoticeUpdateTrainer(noticeModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string GetSendNoticeById(int Id)
        {
            NoticeModel noticeModel = new NoticeModel();
            noticeModel = _service.NoticeRepository.GetSendNoticeById(Id);
            return JsonConvert.SerializeObject(noticeModel);
        }

    }
}