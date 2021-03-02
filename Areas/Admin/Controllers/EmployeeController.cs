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
    public class EmployeeController : BaseController
    {
        public EmployeeController(IUnitOfWork _service) : base(_service)
        {

        }



        public ActionResult EmployeeRegistration()
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

        public ActionResult NonVerifiedEmployees()
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

        public ActionResult VerifiedEmployees()
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

        public ActionResult NewEmployeeRegistration(string param = "")
        {
            UserModel usermodel = JsonConvert.DeserializeObject<UserModel>(param);
            long result = 0;
            if (usermodel.userid != null)
            {
                result = _service.EmployeeRepository.UpdateEmployeesDetails(usermodel);
            }
            else
            {
                result = _service.EmployeeRepository.InsertEmployeesDetails(usermodel);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public string EmployeeList(string param = "")
        {
            UserCustom usercustom = JsonConvert.DeserializeObject<UserCustom>(param);
            usercustom = _service.EmployeeRepository.GetEmployeeList(usercustom);
            return JsonConvert.SerializeObject(usercustom);

        }

        [HttpGet]

        public string GetNonVerifiedEmployeeList(string param = "")
        {
            UserCustom usercustom = JsonConvert.DeserializeObject<UserCustom>(param);
            usercustom = _service.EmployeeRepository.GetNonVerifiedEmployeeList(usercustom);
            return JsonConvert.SerializeObject(usercustom);

        }

        [HttpGet]

        public string GetVerifiedEmployeeList(string param = "")
        {
            UserCustom usercustom = JsonConvert.DeserializeObject<UserCustom>(param);
            usercustom = _service.EmployeeRepository.GetVerifiedEmployeeList(usercustom);
            return JsonConvert.SerializeObject(usercustom);

        }

        [HttpGet]

        public string GetEmployeeKYCDetails(int userid)
        {
            UserModel usermodel = new UserModel();
            usermodel = _service.EmployeeRepository.GetEmployeeKYCDetailsById(userid);
            return JsonConvert.SerializeObject(usermodel);
            

        }

        [HttpGet]

        public string GetEmployeeDetailsById(int userid)
        {
            UserModel usermodel = new UserModel();
            usermodel = _service.EmployeeRepository.GetEmployeeDetailsById(userid);
            return JsonConvert.SerializeObject(usermodel);

        }

        [HttpGet]

        public ActionResult DeleteEmployee(string param = "")
        {
            UserModel usermodel = JsonConvert.DeserializeObject<UserModel>(param);
            long result = 0;
            result = _service.EmployeeRepository.DeleteEmployeesDetails(usermodel);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]

        public ActionResult ApproveEmployeeKYCDetails(string param = "")
        {
            UserModel usermodel = JsonConvert.DeserializeObject<UserModel>(param);
            long result = _service.EmployeeRepository.ApproveEmployeeKYCDetails(usermodel);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


    }
}