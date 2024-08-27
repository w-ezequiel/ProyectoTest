using ProyectoTest.Logica;
using ProyectoTest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;

namespace TestProject2
{
    internal class PruebaProdcuctoLogic
    {

        private static PruebaProdcuctoLogic _instancia = null;

        public PruebaProdcuctoLogic()
        {

        }

        public static PruebaProdcuctoLogic Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new PruebaProdcuctoLogic();
                }

                return _instancia;
            }
        }
        public bool Eliminar(int id)
        {
            bool respuesta = true;
            //using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            List<Producto> produ = new List<Producto>();
            produ.Add(new Producto() { IdProducto = 1, Nombre = "Play 5" });
            produ.Add(new Producto() { IdProducto = 2, Nombre = "Televisor" });
            produ.Add(new Producto() { IdProducto = 3, Nombre = "Laptop" });



            {
                try
                {
                    
                    respuesta = produ.Remove(new Producto() { IdProducto = id }); ;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
    }
}
