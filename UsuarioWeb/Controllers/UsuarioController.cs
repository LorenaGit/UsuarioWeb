using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsuarioWeb.Models;
using UsuarioWeb.Repository;

namespace UsuarioWeb.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            UsuarioRepository ur = new UsuarioRepository();

            List<Usuario> usuarios = ur.getUsuariosAll();

            ViewBag.resultadoParaLaVista = usuarios;

           
            return View();
        }


        public ActionResult FormularioNuevo()
        {
            return View();
        }



        public void Insertar()
        {

            Usuario x = new Usuario();

            x.Nombre = Request["Nombre"].ToString();
            x.Telefono = Request["Telefono"].ToString();
            x.Email = Request["Email"].ToString();

            UsuarioRepository ur = new UsuarioRepository();
            int filasAfectadas = ur.insertUsuario(x);

            Response.Redirect("/Usuario/Index");
        }


        public ActionResult FormularioUpdate()
        {

            int id = Convert.ToInt32(Request["id"].ToString());

            UsuarioRepository ur = new UsuarioRepository();

            Usuario x = ur.getUsuarioById(id);

            ViewBag.usuario = x;
            return View();
        }


        public void Actualizar()
        {
            Usuario x = new Usuario();
            x.Id = Convert.ToInt32(Request["Id"].ToString());
            x.Nombre = Request["Nombre"].ToString();
            x.Telefono = Request["Telefono"].ToString();
            x.Email = Request["Email"].ToString();

            UsuarioRepository ur = new UsuarioRepository();

            ur.updateUsuario(x);


            Response.Redirect("/Usuario/Index");

        }

        public void Borrar()
        {
            int id = Convert.ToInt32(Request["id"].ToString());

            UsuarioRepository ur = new UsuarioRepository();

            ur.deleteUsuario(id);


            Response.Redirect("/Usuario/Index");

        }
    }
}