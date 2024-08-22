using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    internal class datosIniciales
    {
        public static void Inicializar(CaducaContext contexto)
        {
            //Si no es base de datos en memoria no se agrega nada
            if (contexto.Database.ProviderName
                          != "Microsoft.EntityFrameworkCore.InMemory")
                return;
            //Te aseguras que la base de datos haya sido creada
            contexto.Database.EnsureCreated();

            var categorias = new Categoria[]
            {
            /*01*/ new Categoria { Clave = 1, Nombre = "Analgésicos"},
            };
            foreach (Categoria registro in categorias)
            {
                contexto.Categoria.Add(registro);
            }
        }
    }
}
