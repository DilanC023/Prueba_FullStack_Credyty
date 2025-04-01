using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositorio.ConexionDB;
using App.Repositorio.Interfaces;
using App.Repositorio.Entidades;

namespace App.Repositorio.Implementaciones
{
    public class VehiculoRepositorio : IVehiculoRepositorio
    {
        private readonly string _ConexionBD;

        public VehiculoRepositorio(IConfiguration configuration)
        {
            _ConexionBD = Conexion.ObtenerConexionBD();
        }
        public async Task<int> AdicionarVehiculo(VehiculoEntidad vehiculo)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TVehiculos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoVehiculo", vehiculo.TipoVehiculo);
                    cmd.Parameters.AddWithValue("@Placa", vehiculo.Placa);
                    cmd.Parameters.AddWithValue("@DocumentoIdentidad", vehiculo.DocumentoIdentidad);
                    cmd.Parameters.AddWithValue("@Opcion", "A");

                    SqlParameter outputIdParam = new SqlParameter("@VehiculoID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputIdParam);

                    await cmd.ExecuteNonQueryAsync();
                    return (int)outputIdParam.Value;
                }
            }
        }
        public async Task<bool> ModificarVehiculo(VehiculoEntidad vehiculo)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TVehiculos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehiculoID", vehiculo.VehiculoID);
                    cmd.Parameters.AddWithValue("@TipoVehiculo", vehiculo.TipoVehiculo);
                    cmd.Parameters.AddWithValue("@Placa", vehiculo.Placa);
                    cmd.Parameters.AddWithValue("@Opcion", "M");

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<bool> EliminarVehiculo(int vehiculoID)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TVehiculos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehiculoID", vehiculoID);
                    cmd.Parameters.AddWithValue("@Opcion", "E");

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<List<VehiculoEntidad>> ConsultarVehiculoPorPlaca(string placa)
        {
            List<VehiculoEntidad> vehiculos = new List<VehiculoEntidad>();

            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TVehiculos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Placa", placa);
                    cmd.Parameters.AddWithValue("@Opcion", "CP");

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            vehiculos.Add(new VehiculoEntidad
                            {
                                VehiculoID = reader.GetInt32(reader.GetOrdinal("VehiculoID")),
                                TipoVehiculo = reader["TipoVehiculo"].ToString()!,
                                Placa = reader["Placa"].ToString()!,
                                DocumentoIdentidad = reader["DocumentoIdentidad"].ToString()!
                            });
                        }
                    }
                }
            }

            return vehiculos;
        }
        public async Task<List<VehiculoEntidad>> ConsultarVehiculoPorID(int? vehiculoID = null)
        {
            List<VehiculoEntidad> vehiculos = new List<VehiculoEntidad>();

            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TVehiculos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehiculoID", (object)vehiculoID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Opcion", "ID");

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            vehiculos.Add(new VehiculoEntidad
                            {
                                VehiculoID = reader.GetInt32(reader.GetOrdinal("VehiculoID")),
                                TipoVehiculo = reader["TipoVehiculo"].ToString()!,
                                Placa = reader["Placa"].ToString()!,
                                DocumentoIdentidad = reader["DocumentoIdentidad"].ToString()!
                            });
                        }
                    }
                }
            }

            return vehiculos;
        }
    }
}
