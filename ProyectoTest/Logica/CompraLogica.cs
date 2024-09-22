using ProyectoTest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoTest.Logica
{
    public class CompraLogica
    {
        private static CompraLogica _instancia = null;

        public CompraLogica()
        {

        }

        public static CompraLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CompraLogica();
                }

                return _instancia;
            }
        }

        public List<graficaCompra> grafVentas()
        {
            List<graficaCompra> objLista = new List<graficaCompra>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_graficacompra", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        objLista.Add(new graficaCompra()
                        {
                            Fecha = dr["Mes"].ToString(),
                            Total = Convert.ToInt32(dr["Total"].ToString())
                        });
                    }
                    dr.Close();

                    return objLista;

                }
                catch (Exception)
                {

                    objLista = null;
                    return objLista;
                }
               
            }
        }

        public List<listarCompra> Listar()
        {
            List<listarCompra> rptListaCompra = new List<listarCompra>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_listacompra", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaCompra.Add(new listarCompra()
                        {
                            IdCompra = Convert.ToInt32(dr["IdCompra"].ToString()),
                            TotalProducto = Convert.ToInt32(dr["TotalProducto"].ToString()),
                            Total = Convert.ToDecimal(dr["Total"].ToString(), new CultureInfo("en-US")),
                            FechaTexto = dr["FechaCompra"].ToString(),
                            usuario = dr["Correo"].ToString()
                        });
                    }
                    dr.Close();

                    return rptListaCompra;

                }
                catch (Exception ex)
                {
                    rptListaCompra = null;
                    return rptListaCompra;
                }
            }
        }

        public bool Registrar(Compra oCompra)
        {

            bool respuesta = false;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    foreach (DetalleCompra dc in oCompra.oDetalleCompra)
                    {
                        query.AppendLine("insert into detalle_compra(IdCompra,IdProducto,Cantidad,Total) values (¡idcompra!," + dc.IdProducto + "," + dc.Cantidad + "," + dc.Total + ")");
                        query.AppendLine("update PRODUCTO set Stock = Stock - " + dc.Cantidad + " where IdProducto = " + dc.IdProducto);
                    }

                    SqlCommand cmd = new SqlCommand("sp_registrarCompra", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", oCompra.IdUsuario);
                    cmd.Parameters.AddWithValue("TotalProducto", oCompra.TotalProducto);
                    cmd.Parameters.AddWithValue("Total", oCompra.Total);
                    cmd.Parameters.AddWithValue("Direccion", oCompra.Direccion);
                    cmd.Parameters.AddWithValue("@QueryDetalleCompra", query.ToString());
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

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