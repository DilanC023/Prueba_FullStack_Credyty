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
    public class TarifaRepositorio : ITarifaRepositorio
    {
        private readonly string _ConexionBD;

        public TarifaRepositorio(IConfiguration configuration)
        {
            _ConexionBD = Conexion.ObtenerConexionBD();
        }

        public async Task<bool> AdicionarTarifa(TarifaEntidad tarifa)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TTarifas", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoVehiculo", tarifa.TipoVehiculo);
                    cmd.Parameters.AddWithValue("@PrecioPorMinuto", tarifa.PrecioPorMinuto);
                    cmd.Parameters.AddWithValue("@Opcion", "A");

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<bool> ModificarTarifa(TarifaEntidad tarifa)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TTarifas", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TarifaID", tarifa.TarifaID);
                    cmd.Parameters.AddWithValue("@TipoVehiculo", tarifa.TipoVehiculo);
                    cmd.Parameters.AddWithValue("@PrecioPorMinuto", tarifa.PrecioPorMinuto);
                    cmd.Parameters.AddWithValue("@Opcion", "M");

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<bool> EliminarTarifa(int tarifaID)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TTarifas", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TarifaID", tarifaID);
                    cmd.Parameters.AddWithValue("@Opcion", "E");

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<List<TarifaEntidad>> ConsultarPorIDTarifa(int? tarifaID = null)
        {
            List<TarifaEntidad> tarifas = new List<TarifaEntidad>();

            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TTarifas", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TarifaID", (object)tarifaID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Opcion", "ID");

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tarifas.Add(new TarifaEntidad
                            {
                                TarifaID = Convert.ToInt32(reader["TarifaID"]),
                                TipoVehiculo = reader["TipoVehiculo"].ToString(),
                                PrecioPorMinuto = Convert.ToDecimal(reader["PrecioPorMinuto"])
                            });
                        }
                    }
                }
            }

            return tarifas;
        }
    }
}
