using GymDataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamoFitness.Controllers
{
    public class BaseController : Controller
    {
        public IUnitOfWork _service;
        public BaseController(IUnitOfWork service)
        {
            _service = service;
        }
    }
}