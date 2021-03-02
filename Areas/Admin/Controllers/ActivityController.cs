using DynamoFitness.Controllers;
using GymDataAccess.Common;
using GymDataAccess.Entities.Model;
using GymDataAccess.Entities.Custom;
using GymDataAccess.Exceptionhandler;
using GymDataAccess.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamoFitness.Areas.Admin.Controllers
{
    [GYMExceptionHandler]
    public class ActivityController : BaseController
    {
        public ActivityController(IUnitOfWork _service) : base(_service)
        {

        }
        // GET: Admin/Activity
        public ActionResult Activity()
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

        public ActionResult NewActivity(string param = "")
        {
            ActivityModel activityModel = JsonConvert.DeserializeObject<ActivityModel>(param);
            
            long result = 0;
           
            if (activityModel.Id != null)
            {
                result = _service.ActivityRepository.UpdateActivity(activityModel);
            }
            else
            {
                result = _service.ActivityRepository.InsertActivity(activityModel);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string ActivityList(string param = "")
        {
            ActivityCustom activitycustom = JsonConvert.DeserializeObject<ActivityCustom>(param);
            activitycustom = _service.ActivityRepository.GetActivityList(activitycustom);           
            return JsonConvert.SerializeObject(activitycustom);

        }
       
        [HttpGet]
        public ActionResult DeleteActivity(string param="")
        {
            ActivityModel activitymodel = JsonConvert.DeserializeObject<ActivityModel>(param);
            long result = 0;
            result = _service.ActivityRepository.DeleteActivityDetails(activitymodel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       

        public string GetActivityDetailsById(int Id)
        {
            ActivityModel activityModel = new ActivityModel();
            activityModel = _service.ActivityRepository.GetActivityDetailsById(Id);
            return JsonConvert.SerializeObject(activityModel);
        }

    }
}