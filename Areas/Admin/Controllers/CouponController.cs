using DynamoFitness.Controllers;
using GymDataAccess.Common;
using GymDataAccess.Entities.Model;
using GymDataAccess.Entities.Custom;
using GymDataAccess.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamoFitness.Areas.Admin.Controllers
{
    public class CouponController : BaseController
        {

        public CouponController(IUnitOfWork _service) : base(_service)
        {

        }
       
        public ActionResult Coupon()
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

        public ActionResult NewCoupon(string param = "")
        {
            CouponModel couponModel = JsonConvert.DeserializeObject<CouponModel>(param);

            long result = 0;

            if (couponModel.CouponId != null)
            {
                result = _service.CouponRepository.UpdateCoupon(couponModel);
            }
            else
            {
                result = _service.CouponRepository.InsertCoupon(couponModel);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string CouponList(string param = "")
        {
            CouponCustom couponcustom = JsonConvert.DeserializeObject<CouponCustom>(param);
            couponcustom = _service.CouponRepository.GetCouponList(couponcustom);
            return JsonConvert.SerializeObject(couponcustom);

        }

        [HttpGet]
        public ActionResult DeleteCoupon(string param = "")
        {
            CouponModel couponModel= JsonConvert.DeserializeObject<CouponModel>(param);
            long result = 0;
            result = _service.CouponRepository.DeleteCouponDetails(couponModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string GetCouponDetailsById(int Id)
        {
            CouponModel couponModel = new CouponModel();
            couponModel = _service.CouponRepository.GetCouponDetailsById(Id);
            return JsonConvert.SerializeObject(couponModel);           
        }

    }
}