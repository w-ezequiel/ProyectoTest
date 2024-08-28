using NUnit.Framework;
using ProyectoTest.Controllers;
using ProyectoTest.Logica;
using ProyectoTest.Models;
using Prueba_Nunit;
using System.Web.Mvc;

using System.Web.Security;

namespace TestProject2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

            

        }

        [Test]
        public void Test1()
        {
            
            var login = new LoginController();

            var result = login.Index() as ActionResult;

            Assert.That(result,Is.Not.Null);
        }

        [Test]
        public void Test2()
        {
            Usuario user2 = new Usuario();
            user2.IdUsuario = 1;
            user2.Correo = "usuario@gmail.com";
            user2.Contra = "1234";
            user2.EsAdministrador = false;

            var login = new PruebaUsuario();

            var result = login.validar(user2) as ViewResult;

            Assert.AreEqual("Tienda", result.ViewName);
        }


        //[Test]
        //public void Test2()
        //{

        //    var prueba = new PruebaProductoController();

        //    var result = prueba.EliminarProducto(1) as ViewResult;
        //    //var result = prueba.EliminarProducto(4) ;

        //    Assert.AreEqual("Index", result.ViewName);
        //}
    }
}