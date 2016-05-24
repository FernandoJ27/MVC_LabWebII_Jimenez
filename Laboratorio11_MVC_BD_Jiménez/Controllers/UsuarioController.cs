using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Laboratorio11_MVC_BD_Jiménez.Controllers
{
    public class UsuarioController : Controller
    {
        private USUARIO usuario = new USUARIO(); 

        // GET: Usuario
        public ActionResult Index(string criterio)
        {
            if (criterio == null | criterio == "")
            {
                return View(usuario.Listar());
            }
            else
            {
                return View(usuario.Buscar(criterio));
            }
        }
        public ActionResult Perfil(int id)
        {
            return View(usuario.Obtener(id));
        }


        public ActionResult Gestionar(int id = 0)
        {
            return View(
                id == 0 ? new USUARIO() //nuevo usuario
                        : usuario.Obtener(id)
            );
        }

        public ActionResult Guardar(USUARIO model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Usuario/");
            }
            else
            {
                return View("~/Views/Usuario/Gestionar.cshtml", model);
            }

        }

        public ActionResult Eliminar(string id)
        {
            usuario.IDUSUARIO = id;
            usuario.Eliminar();
            return Redirect("~/Usuario/");
        }

        public ActionResult Buscar(string criterio)
        {
            return View(
                        criterio == null | criterio == ""
                        ? usuario.Listar()
                        : usuario.Buscar(criterio)
                );
        }
    }
}