using DynamoFitness.Controllers;
using GymDataAccess.UnitOfWork;
using GymDataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymDataAccess.Exceptionhandler;

namespace DynamoFitness.Areas.Admin.Controllers
{
    [GYMExceptionHandler]
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard

        public DashboardController(IUnitOfWork _service):base(_service)
        {

        }
        public ActionResult Dashboard()
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
    }
}