using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;
using Laboratorio11_MVC_BD_Jiménez.Filters;

namespace Laboratorio11_MVC_BD_Jiménez.Controllers
{
    [Autenticado]
    public class HomeController : Controller
    {

        private CATEGORIA categoria = new CATEGORIA();
        private USUARIO usuario = new USUARIO();
        private PEDIDO pedido = new PEDIDO();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //CATEGORIA ---------------------------------------------------------------------------
        public ActionResult Categorias(string criterio)
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

        public ActionResult CategoriaProductos(int id)
        {
            return View(categoria.Obtener(id));
        }


        public ActionResult GestionarCategoria(int id = 0)
        {
            return View(
                id == 0 ? new CATEGORIA() //nueva categoria
                        : categoria.Obtener(id)
            );
        }

        public ActionResult GuardarCategoria(CATEGORIA model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Home/Categorias");
            }else
            {
                return View("~/Views/Home/GestionarCategoria.cshtml", model);
            }
            
        }

        public ActionResult EliminarCategoria(int id)
        {
            categoria.IDCATEGORIA = id;
            categoria.Eliminar();
            return Redirect("~/Home/Categorias");
        }

        public ActionResult CategoriaBuscar(string criterio)
        {
            return View(
                        criterio == null | criterio == "" 
                        ? categoria.Listar()
                        : categoria.Buscar(criterio)
                );
        }

        //USUARIO ------------------------------------------------------
        public ActionResult Usuarios()
        {
            return View(usuario.Listar());
        }
        
        public ActionResult UsuarioPerfil(int id)
        {
            return View(usuario.Obtener(id));
        }

        public ActionResult GestionarUsuario(int id = 0)
        {
            return View(
                id == 0 ? new CATEGORIA() //nueva categoria
                        : categoria.Obtener(id)
            );
        }

        public ActionResult GuardarUsuario(CATEGORIA model)
        {
            model.Guardar();
            return Redirect("~/Home/Categorias");
        }

        public ActionResult EliminarUsuario(int id)
        {
            categoria.IDCATEGORIA = id;
            categoria.Eliminar();
            return Redirect("~/Home/Categorias");
        }

        //PEDIDO ------------------------------------------------------
        public ActionResult Pedidos()
        {
            return View(usuario.Listar());
        }

        public ActionResult PedidoDetalle(int id)
        {
            return View(usuario.Obtener(id));
        }

        public ActionResult GestionarPedido(int id = 0)
        {
            return View(
                id == 0 ? new CATEGORIA() //nueva categoria
                        : categoria.Obtener(id)
            );
        }

        public ActionResult GuardarPedido(CATEGORIA model)
        {
            model.Guardar();
            return Redirect("~/Home/Categorias");
        }
    }
}