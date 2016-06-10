using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;
using Laboratorio11_MVC_BD_Jiménez.Filters;

namespace Laboratorio11_MVC_BD_Jiménez.Controllers
{
    public class LoginController : Controller
    {

        private USUARIO usuario = new USUARIO();
        [NoLogin]

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Acceder(string Email, string Password)
        {
            var rm = usuario.Acceder(Email, Password);

            if (rm.response)
            {
                rm.href = Url.Content("~/Home");
            }
            return Redirect("~/");
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("~/");
        }
    }
}