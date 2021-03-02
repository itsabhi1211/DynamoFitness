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

namespace DynamoFitness.Areas.Employee.Controllers
{
    [GYMExceptionHandler]
    public class ProfileController : BaseController
    {
        public ProfileController(IUnitOfWork _service) : base(_service)
        {


        }

        public ActionResult KYC()
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

        [HttpGet]

        public string GetEmployeeKYCDetails(int userid)
        {
            UserModel usermodel = new UserModel();
            usermodel = _service.EmployeeRepository.GetEmployeeKYCDetailsById(userid);
            return JsonConvert.SerializeObject(usermodel);


        }
        public ActionResult ProfileDetails()
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
        public ActionResult ChangePassword()
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

        [HttpGet]

        public string ShowProfileDetails(int userid)
        {
            UserModel usermodel = new UserModel();
            usermodel = _service.EmployeeRepository.ShowEmployeeProfileDetails(userid);
            return JsonConvert.SerializeObject(usermodel);

        }

        [HttpPost]

        public ActionResult UpdateProfile(string param = "")
        {
            UserModel usermodel = JsonConvert.DeserializeObject<UserModel>(param);

            long result = _service.EmployeeRepository.UpdateEmployeeProfileDetails(usermodel);


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult UpdateProfileImage(string param = "")
        {
            UserModel usermodel = JsonConvert.DeserializeObject<UserModel>(param);
            long result = 0;
            result = _service.EmployeeRepository.UpdateEmployeeProfileImage(usermodel);


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult ChangePassword(string param = "")
        {
            UserCustom usermodel = JsonConvert.DeserializeObject<UserCustom>(param);
            long result = 0;
            result = _service.EmployeeRepository.ChangeEmployeePassword(usermodel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult UpdateEmployeeKYC(string param = "")
        {
            UserModel usermodel = JsonConvert.DeserializeObject<UserModel>(param);
            long result = _service.EmployeeRepository.UpdateEmployeeKYCDetails(usermodel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}