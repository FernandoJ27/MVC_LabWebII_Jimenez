using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Laboratorio11_MVC_BD_Jiménez.Controllers
{
    public class ProductoController : Controller
    {
        private PRODUCTO producto = new PRODUCTO();
        private CATEGORIA categoria = new CATEGORIA();

        // GET: Producto
        public ActionResult Index(string criterio)
        {
            ViewBag.CboxCategoria = categoria.Listar();

            if (criterio == "" || criterio == null)
            {
                return View(producto.Listar());
            }
            else
            {
                var criterioINT = Convert.ToInt16(criterio);

                return View(producto.Buscar(criterioINT));
            }
        }
    }
}