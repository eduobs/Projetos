using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValidaCartoes.Models;

namespace ValidaCartoes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CartaoCredito model)
        {
            model.bandeiraCartao = bandeira.VISA;
            model.valido = true;

            return View(model);
        }
    }
}