using GymDataAccess.Exceptionhandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamoFitness.Areas.Trainer.Controllers
{
    [GYMExceptionHandler]
    public class DashboardController : Controller
    {

        // GET: Trainer/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}