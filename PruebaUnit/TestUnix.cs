using ProyectoTest.Controllers;
using ProyectoTest.Logica;
using ProyectoTest.Models;
using System.Web.Mvc;


namespace PruebaUnit
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Usuario user = new Usuario();
          // string direccion= "c:/local";
          
            var login = new LoginController();

            var result = login.Index() as ActionResult;
            Assert.Empty(result);

            //var controller = new ProductController();
            //var result = controller.Details(2) as ViewResult;
            //Assert.AreEqual("Details", result.);

        }
    }
}