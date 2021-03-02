using DynamoFitness.Controllers;
using GymDataAccess.Common;
using GymDataAccess.Entities.Custom;
using GymDataAccess.Entities.Model;
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
    public class TrainerController : BaseController
    {
        public TrainerController(IUnitOfWork _service) : base(_service)
        {

        }
        public ActionResult TrainerRegistration()
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

        public ActionResult NonVerifiedTrainers()
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

        public ActionResult VerifiedTrainers()
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

        public ActionResult ApproveTrainerKYCDetails(string param = "")
        {
            UserModel usermodel = JsonConvert.DeserializeObject<UserModel>(param);
            long result = _service.TrainerRepository.ApproveTrainersKYCDetails(usermodel);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]

        public string GetNonVerifiedTrainerList(string param = "")
        {
            UserCustom usercustom = JsonConvert.DeserializeObject<UserCustom>(param);
            usercustom = _service.TrainerRepository.GetNonVerifiedTrainerList(usercustom);
            return JsonConvert.SerializeObject(usercustom);

        }

        [HttpGet]

        public string GetVerifiedTrainerList(string param = "")
        {
            UserCustom usercustom = JsonConvert.DeserializeObject<UserCustom>(param);
            usercustom = _service.TrainerRepository.GetVerifiedTrainerList(usercustom);
            return JsonConvert.SerializeObject(usercustom);

        }

        [HttpGet]

        public string GetTrainerKYCDetails(int userid)
        {
            UserModel usermodel = new UserModel();
            usermodel = _service.TrainerRepository.GetTrainerKYCDetailsById(userid);
            return JsonConvert.SerializeObject(usermodel);


        }

        public ActionResult NewTrainerRegistration(string param = "")
        {
            UserModel usermodel = JsonConvert.DeserializeObject<UserModel>(param);
            long result = 0;
            if (usermodel.userid != null)
            {
                result = _service.TrainerRepository.UpdateTrainersDetails(usermodel);
            }
            else
            {
                result = _service.TrainerRepository.InsertTrainersDetails(usermodel);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public string TrainerList(string param = "")
        {
            UserCustom usercustom = JsonConvert.DeserializeObject<UserCustom>(param);
            usercustom = _service.TrainerRepository.GetTrainerList(usercustom);
            return JsonConvert.SerializeObject(usercustom);

        }

        [HttpGet]

        public string GetTrainerDetailsById(int userid)
        {
            UserModel usermodel = new UserModel();
            usermodel = _service.TrainerRepository.GetTrainerDetailsById(userid);
            return JsonConvert.SerializeObject(usermodel);

        }

        [HttpGet]

        public ActionResult DeleteTrainer(string param = "")
        {
            UserModel usermodel = JsonConvert.DeserializeObject<UserModel>(param);
            long result = 0;
            result = _service.TrainerRepository.DeleteTrainersDetails(usermodel);
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}