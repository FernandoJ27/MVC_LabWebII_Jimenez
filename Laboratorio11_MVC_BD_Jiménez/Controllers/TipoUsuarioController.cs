using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Laboratorio11_MVC_BD_Jiménez.Controllers
{
    public class TipoUsuarioController : Controller
    {
        // GET: TipoUsuario
        private TIPO_USUARIO tipousuario = new TIPO_USUARIO();
        public ActionResult Index(string criterio)
        {
            if (criterio == null | criterio == "")
            {
                return View(tipousuario.Listar());
            }
            else
            {
                return View(tipousuario.Buscar(criterio));
            }
        }

        //Nuevo Anexgrid
        public JsonResult ListarAnexGrid(Modelo.AnexGRID grid)
        {
            return Json(tipousuario.ListarAnexGRID(grid));
        }

        public ActionResult Gestionar(int id = 0)
        {
            return View(
                id == 0 ? new TIPO_USUARIO() //nuevo usuario
                        : tipousuario.Obtener(id)
            );
        }

        public ActionResult Usuarios(int id)
        {
            return View(tipousuario.Obtener(id));
        }

        public ActionResult Guardar(TIPO_USUARIO model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/TipoUsuario/");
            }
            else
            {
                return View("~/Views/TipoUsuario/Gestionar.cshtml", model);
            }

        }

        public ActionResult Eliminar(int id)
        {
            tipousuario.IDTIPOUSUARIO = id;
            tipousuario.Eliminar();
            return Redirect("~/TipoUsuario/");
        }

        public ActionResult Buscar(string criterio)
        {
            return View(
                        criterio == null | criterio == ""
                        ? tipousuario.Listar()
                        : tipousuario.Buscar(criterio)
                );
        }
    }
}