using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYMFIND.ViewModel;
using GYMFIND.Models;

namespace GYMFIND.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login(){


            return View();

        }

        public ActionResult dashboard() {


            return View();
        }
        [HttpPost]
        public ActionResult registrarUsuario(VmLogin vmLogin)
        {
            try
            {
                //Cliente cliente = 



                return View();
            }
            catch (Exception ex)
            {

                throw;
            }

            
        }
    }
}