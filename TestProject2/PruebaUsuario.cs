using ProyectoTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Prueba_Nunit
{
    internal class PruebaUsuario:Controller
    {

        public ActionResult Producto()
        {
            Usuario user = new Usuario();
            user.IdUsuario = 1;
            user.Correo = "usuario@gmail.com";
            user.Contra = "1234";

            Session["Usuario"] = user;

            if (Session["Usuario"] == null)
                return View("Login");

            return View();
        }

        public ActionResult validar(Usuario user2)
        {
           
            if (user2.EsAdministrador == true)
            {
                return View("Home");
            }
            else
            {
                return View("Tienda");
            }
        }
       

    }
}


