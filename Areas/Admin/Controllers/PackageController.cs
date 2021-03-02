using DynamoFitness.Controllers;
using GymDataAccess.Common;
using GymDataAccess.Entities.Custom;
using GymDataAccess.Entities.Model;
using GymDataAccess.Exceptionhandler;
using GymDataAccess.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamoFitness.Areas.Admin.Controllers
{
    [GYMExceptionHandler]
    public class PackageController : BaseController
    {
        public PackageController(IUnitOfWork _service) : base(_service)
        {
                
        }
        public ActionResult NewPackage()
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

        public ActionResult NewPackage(string param = "")
        {
            PackageModel packagemodel = JsonConvert.DeserializeObject<PackageModel>(param);
            long result = 0;
            if (packagemodel.Packageid != null)
            {
                result = _service.PackageRepository.UpdatePackagesDetails(packagemodel);
            }
            else
            {
                result = _service.PackageRepository.InsertPackagesDetails(packagemodel);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public string PackageList(string param = "")
        {
            
            PackageCustom packagecustom = JsonConvert.DeserializeObject<PackageCustom>(param);
            packagecustom = _service.PackageRepository.GetPackageList(packagecustom);
            return JsonConvert.SerializeObject(packagecustom);

        }

        [HttpGet]

        public string GetPackageDetailsById(int packageid)
        {
            PackageModel packagemodel = new PackageModel();
            packagemodel = _service.PackageRepository.GetPackageDetailsById(packageid);
            return JsonConvert.SerializeObject(packagemodel);

        }

        [HttpGet]

        public ActionResult DeletePackage(string param = "")
        {
            PackageModel packagemodel = JsonConvert.DeserializeObject<PackageModel>(param);
            long result = 0;
            result = _service.PackageRepository.DeletePackagesDetails(packagemodel);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult NewPackageDetails()
        {
            CommonCls commonCls = new CommonCls();
            if (commonCls.getUserIdFromSession() == 0)
            {
                return Redirect("/Home/Login");
            }
            else
            {
                PackageDetailCustom objpkgdetailscustom = new PackageDetailCustom();
                objpkgdetailscustom.lstpackages = _service.PackageRepository.bindPackage();
                objpkgdetailscustom.LstActivity = _service.ActivityRepository.bindActivity();                
                return View(objpkgdetailscustom);
            }
        }

        public ActionResult AddNewPackageDetails()
        {
            CommonCls commonCls = new CommonCls();
            if (commonCls.getUserIdFromSession() == 0)
            {
                return Redirect("/Home/Login");
            }
            else
            {
                PackageDetailCustom objpkgdetailscustom = new PackageDetailCustom();
                objpkgdetailscustom.lstpackages = _service.PackageRepository.bindPackage();
              objpkgdetailscustom.LstActivity = _service.ActivityRepository.bindActivity();
                //ViewBag.activitylist = _service.ActivityRepository.bindActivity();


                return View(objpkgdetailscustom);
            }
        }

        [HttpPost]

        public ActionResult AddNewPackagedetails(string param = "")
        {
            PackageDetailModel packagedetailsmodel = JsonConvert.DeserializeObject<PackageDetailModel>(param);
            long result = 0;
            if (packagedetailsmodel.PackageDetailId!= null)
            {
                result = _service.PackageDetailsRepository.UpdatePackagesDetails(packagedetailsmodel);
            }
            else
            {
                result = _service.PackageDetailsRepository.InsertPackagesDetails(packagedetailsmodel);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]

        public string GetPackageDetailsList(string param = "")
        {
            PackageDetailCustom packagedetailcustom = JsonConvert.DeserializeObject<PackageDetailCustom>(param);
            packagedetailcustom = _service.PackageDetailsRepository.GetPackageDetailsList(packagedetailcustom);
            return JsonConvert.SerializeObject(packagedetailcustom);

        }

        [HttpGet]

        public string ViewPackageOfferDetails(string param = "")
        {
            PackageDetailModel packageDetailModel = JsonConvert.DeserializeObject<PackageDetailModel>(param);
            packageDetailModel = _service.PackageDetailsRepository.ViewPackageDetailsOffer(packageDetailModel);
            return JsonConvert.SerializeObject(packageDetailModel);

        }

        [HttpGet]

        public string GetPackageDetailsByPackageDetailsId(int PackageDetailId)
        {
            PackageDetailModel packageDetailModel = new PackageDetailModel();
            packageDetailModel = _service.PackageDetailsRepository.GetPackageDetailsById(PackageDetailId);
            return JsonConvert.SerializeObject(packageDetailModel);          

        }

        public ActionResult DeletePackageDetails(string param = "")
        {
            PackageDetailModel packagedetailmodel = JsonConvert.DeserializeObject<PackageDetailModel>(param);
            long result = 0;
            result = _service.PackageDetailsRepository.DeletePackagesDetails(packagedetailmodel);
            return Json(result, JsonRequestBehavior.AllowGet);

        }



    }
}