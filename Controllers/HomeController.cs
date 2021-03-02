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
using System.Web.Security;

namespace DynamoFitness.Controllers
{
    [GYMExceptionHandler]
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork _service) : base(_service)
        {

        }

        [HttpGet]
        public string ExceptionLogger(string param = "")
        {
            ExceptionLoggerCustom exceptionLoggerCustom = JsonConvert.DeserializeObject<ExceptionLoggerCustom>(param);
            exceptionLoggerCustom = _service.ExceptionLoggerRepository.GetExceptionLoggerList(exceptionLoggerCustom);
            return JsonConvert.SerializeObject(exceptionLoggerCustom);
        }
        public ActionResult Login()
        {

            return View();



        }

        #region code for logout



        public ActionResult Logout()
        {


            int? userid = Convert.ToInt32(Session["userid"]);

            if (userid != 0)
            {
                Session["userid"] = null;
            }


            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Expires = -1000;
            Response.CacheControl = "no-cache";
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return Redirect("/Home/Login");


        }

        #endregion
        public ActionResult UserLogin(FormCollection objfrm)
        {
            if (ModelState.IsValid)
            {

                string userid = Convert.ToString(objfrm["email"]);
                string pwd = Convert.ToString(objfrm["password"]);

                var result = _service.LoginRepository.Login(userid, pwd,"employee");


                if (result.Rows.Count > 0)
                {
                    Session["type"] = Convert.ToString(result.Rows[0]["type"]);
                    var type = Convert.ToString(Session["type"]);
                    Session["userid"] = Convert.ToInt32(result.Rows[0]["userid"]);
                    Session["name"] = Convert.ToString(result.Rows[0]["name"]);
                    Session["lastlogin"] = Convert.ToString(result.Rows[0]["lastlogin"]);
                    Session["verified"]= Convert.ToString(result.Rows[0]["isverified"]);

                    if (type == "Admin")

                    {
                        return Redirect("/Admin/Dashboard/Dashboard");
                    }

                    else if (type == "employee")
                    {
                        return Redirect("/Employee/Profile/ProfileDetails");
                        
                    }
                    else
                    {
                        return Redirect("/Trainer/Dashboard/Dashboard");
                    }
                }
                else
                {
                    TempData["msg"] = "Wrong UserId Or Password";
                }



            }

            return View("Login");
        }

        public ActionResult Error()
        {

            return View();

        }

        public ActionResult ErrorList()
        {

            return View();

        }


        public int ForgotPassword(string EmailID)
        {

            int successfull = 0;

            var result = _service.LoginRepository.MailExists(EmailID);
            if (result == 1)
            {

                var isMailSent = _service.LoginRepository.ForgotPassword(EmailID);

                if (isMailSent == 6)
                {

                    successfull = 1;
                }
            }
            else
            {
                successfull = 2;

            }



            return successfull;
        }

    }
}