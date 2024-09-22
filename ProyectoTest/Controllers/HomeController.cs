using ProyectoTest.Models;
using ProyectoTest.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Collections;

namespace ProyectoTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Producto()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }
        public ActionResult Compra()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Tienda()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }



        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> oLista = new List<Producto>();

            oLista = ProductoLogica.Instancia.Listar();
            oLista = (from o in oLista
                      select new Producto()
                      {
                          IdProducto = o.IdProducto,
                          Nombre = o.Nombre,
                          Descripcion = o.Descripcion,
                          Precio = o.Precio,
                          Stock = o.Stock,
                          RutaImagen = o.RutaImagen,
                          base64 = utilidades.convertirBase64(Server.MapPath(o.RutaImagen)),
                          extension = Path.GetExtension(o.RutaImagen).Replace(".", ""),
                          Activo = o.Activo
                      }).ToList();
            var json = Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        //[HttpGet]
        //public JsonResult ReporteVentas()
        //{
        //    DT_Reporte objDT_Reporte = new DT_Reporte();

        //    List<ReporteVenta> objLista = objDT_Reporte.RetornarVentas();

        //    return Json(objLista, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult GraficoCompra()
        {
            List<graficaCompra> oLista = new List<graficaCompra>();

            oLista = CompraLogica.Instancia.grafVentas();
            //oLista = (from o in oLista
            //          select new graficaCompra()
            //          {
            //              Fecha = o.Fecha,
            //              Total = o.Total
            //          }).ToList();
            //var json = Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            //json.MaxJsonLength = 500000000;

            //List<ReporteVenta> objLista = objDT_Reporte.RetornarVentas();

            return Json(oLista, JsonRequestBehavior.AllowGet);
            //return json;
        }

        [HttpGet]
        public JsonResult ListarCompra()
        {
            List<listarCompra> oLista = new List<listarCompra>();

            oLista = CompraLogica.Instancia.Listar();
            oLista = (from o in oLista
                      select new listarCompra()
                      {
                          IdCompra = o.IdCompra,
                          TotalProducto = o.TotalProducto,
                          Total = o.Total,
                          FechaTexto = o.FechaTexto,
                          usuario = o.usuario
                      }).ToList();
            var json = Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }



        [HttpPost]
        public JsonResult GuardarProducto(string objeto, HttpPostedFileBase imagenArchivo)
        {

            Response oresponse = new Response() { resultado = true, mensaje = "" };

            try
            {
                Producto oProducto = new Producto();
                oProducto = JsonConvert.DeserializeObject<Producto>(objeto);

                string GuardarEnRuta = "~/Imagenes/Productos/";
                string physicalPath = Server.MapPath("~/Imagenes/Productos");

                if (!Directory.Exists(physicalPath))
                    Directory.CreateDirectory(physicalPath);

                if (oProducto.IdProducto == 0)
                {
                    int id = ProductoLogica.Instancia.Registrar(oProducto);
                    oProducto.IdProducto = id;
                    oresponse.resultado = oProducto.IdProducto == 0 ? false : true;

                }
                else
                {

                    oresponse.resultado = ProductoLogica.Instancia.Modificar(oProducto);

                }


                if (imagenArchivo != null && oProducto.IdProducto != 0)
                {
                    string extension = Path.GetExtension(imagenArchivo.FileName);
                    GuardarEnRuta = GuardarEnRuta + oProducto.IdProducto.ToString() + extension;
                    oProducto.RutaImagen = GuardarEnRuta;

                    imagenArchivo.SaveAs(physicalPath + "/" + oProducto.IdProducto.ToString() + extension);

                    oresponse.resultado = ProductoLogica.Instancia.ActualizarRutaImagen(oProducto);
                }

            }
            catch (Exception e)
            {
                oresponse.resultado = false;
                oresponse.mensaje = e.Message;
            }

            return Json(oresponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            respuesta = ProductoLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }

    public class Response
    {

        public bool resultado { get; set; }
        public string mensaje { get; set; }
    }

}