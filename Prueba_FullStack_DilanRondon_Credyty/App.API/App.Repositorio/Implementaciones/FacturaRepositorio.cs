using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositorio.ConexionDB;
using App.Repositorio.Entidades;
using App.Repositorio.Interfaces;

namespace App.Repositorio.Implementaciones
{
    public class FacturaRepositorio : IFacturaRepositorio
    {
        private readonly string _ConexionBD;

        public FacturaRepositorio(IConfiguration configuration)
        {
            _ConexionBD = Conexion.ObtenerConexionBD();
        }

        public async Task<bool> AdicionarFactura(int ingresoID, string numeroFactura)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_FacturasDescuento", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IngresoID", ingresoID);
                    cmd.Parameters.AddWithValue("@NumeroFactura", numeroFactura);
                    cmd.Parameters.AddWithValue("@Opcion", "A");

                    var mensajeError = new SqlParameter("@MensajeError", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(mensajeError);

                    int result = await cmd.ExecuteNonQueryAsync();

                    return result > 0 && string.IsNullOrEmpty(mensajeError.Value.ToString());
                }
            }
        }

        public async Task<List<FacturaEntidad>> ConsultarFacturas(int? facturaID = null)
        {
            var facturas = new List<FacturaEntidad>();

            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_FacturasDescuento", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FacturaID", (object)facturaID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Opcion", "C");

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            facturas.Add(new FacturaEntidad
                            {
                                FacturaID = Convert.ToInt32(reader["FacturaID"]),
                                IngresoID = Convert.ToInt32(reader["IngresoID"]),
                                NumeroFactura = reader["NumeroFactura"].ToString(),
                            });
                        }
                    }
                }
            }
            return facturas;
        }
    }
}
