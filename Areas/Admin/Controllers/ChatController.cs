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
    public class ChatController : BaseController
    {
        public ChatController(IUnitOfWork _service) : base(_service)
        {

        }
        public ActionResult RecentChat()
        {
            CommonCls commonCls = new CommonCls();
            if (commonCls.getUserIdFromSession() == 0)
            {
                return Redirect("/Home/Login");
            }
            else
            {
                ChatCustom chatCustom = new ChatCustom();
                chatCustom.LstUsers = _service.TrainerRepository.bindTrainers();
                chatCustom.lstemp = _service.EmployeeRepository.bindEmployees();
                return View(chatCustom);
            }
        }

        

        [HttpGet]

        public string RecentChatList(string param = "")
        {
            ChatCustom chatcustom = JsonConvert.DeserializeObject<ChatCustom>(param);
            chatcustom = _service.ChatRepository.RecentChats(chatcustom);
            return JsonConvert.SerializeObject(chatcustom);

        }

        [HttpPost]

        public ActionResult Message(string param = "")
        {
            ChatModel Chatmodel = JsonConvert.DeserializeObject<ChatModel>(param);
            long result = _service.ChatRepository.SaveMessage(Chatmodel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}