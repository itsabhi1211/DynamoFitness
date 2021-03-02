using DynamoFitness.Controllers;
using GymDataAccess.Entities.Custom;
using GymDataAccess.Exceptionhandler;
using GymDataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamoFitness.Areas.Admin.Controllers
{
    [GYMExceptionHandler]
    public class CustomerController : BaseController
    {
        public CustomerController(IUnitOfWork _service) : base(_service)
        {
                
        }
        public ActionResult CustomerRegistration()
        {
            PackageDetailCustom objpkgdetailscustom = new PackageDetailCustom();
            objpkgdetailscustom.lstpackages = _service.PackageRepository.bindPackage();
            return View(objpkgdetailscustom);
        }
    }
}