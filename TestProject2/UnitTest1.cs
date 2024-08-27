using NUnit.Framework;
using ProyectoTest.Controllers;
using ProyectoTest.Logica;
using ProyectoTest.Models;
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
           
            var prueba = new PruebaProductoController();

            var result = prueba.EliminarProducto(1) as ActionResult;

            Assert.That(result, Is.Not.Null);
        }
    }
}