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
    public class SlotController : BaseController
    {
        public SlotController(IUnitOfWork _service) : base(_service)
        {
                
        }
        public ActionResult Slot()
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

        public ActionResult NewSlot(string param = "")
        {
            SlotModel Slotmodel = JsonConvert.DeserializeObject<SlotModel>(param);
            long result = 0;
            if (Slotmodel.slotid != null)
            {
                result = _service.SlotRepository.UpdateSlotsDetails(Slotmodel);
            }
            else
            {
                result = _service.SlotRepository.InsertSlotsDetails(Slotmodel);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public string SlotList(string param = "")
        {
            SlotCustom Slotcustom = JsonConvert.DeserializeObject<SlotCustom>(param);
            Slotcustom = _service.SlotRepository.GetSlotList(Slotcustom);
            return JsonConvert.SerializeObject(Slotcustom);

        }

        [HttpGet]

        public string GetSlotDetailsById(int Slotid)
        {
            SlotModel Slotmodel = new SlotModel();
            Slotmodel = _service.SlotRepository.GetSlotDetailsById(Slotid);
            return JsonConvert.SerializeObject(Slotmodel);

        }

        [HttpGet]

        public ActionResult DeleteSlot(string param = "")
        {
            SlotModel Slotmodel = JsonConvert.DeserializeObject<SlotModel>(param);
            long result = 0;
            result = _service.SlotRepository.DeleteSlotsDetails(Slotmodel);
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}