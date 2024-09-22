using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTest.Models
{
    public class listarCompra
    {
        public int IdCompra { get; set; }
        //public int IdUsuario { get; set; }
        public int TotalProducto { get; set; }
        public decimal Total { get; set; }
        public string FechaTexto { get; set; }
        public string usuario { get; set; }

    }
}