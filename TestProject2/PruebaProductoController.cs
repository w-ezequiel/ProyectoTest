using ProyectoTest.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TestProject2
{
    internal class PruebaProductoController:Controller
    {
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            respuesta = PruebaProdcuctoLogic.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}
