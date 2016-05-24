using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Laboratorio11_MVC_BD_Jiménez.Controllers
{
    public class CategoriaController : Controller
    {
        private CATEGORIA categoria = new CATEGORIA();

        // GET: Categoria
        public ActionResult Index(string criterio)
        {
            if (criterio == null | criterio == "")
            {
                return View(categoria.Listar());
            }
            else
            {
                return View(categoria.Buscar(criterio));
            }
        }

        public ActionResult Productos(int id)
        {
            return View(categoria.Obtener(id));
        }


        public ActionResult Gestionar(int id = 0)
        {
            return View(
                id == 0 ? new CATEGORIA() //nueva categoria
                        : categoria.Obtener(id)
            );
        }

        public ActionResult Guardar(CATEGORIA model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Categoria/");
            }
            else
            {
                return View("~/Views/Categoria/Gestionar.cshtml", model);
            }

        }

        public ActionResult Eliminar(int id)
        {
            categoria.IDCATEGORIA = id;
            categoria.Eliminar();
            return Redirect("~/Categoria/");
        }

        public ActionResult Buscar(string criterio)
        {
            return View(
                        criterio == null | criterio == ""
                        ? categoria.Listar()
                        : categoria.Buscar(criterio)
                );
        }
    }
}