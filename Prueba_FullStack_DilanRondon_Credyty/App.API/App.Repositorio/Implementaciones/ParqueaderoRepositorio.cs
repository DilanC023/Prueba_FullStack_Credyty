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
    public class ParqueaderoRepositorio : IParqueaderoRepositorio
    {
        private readonly string _ConexionBD;

        public ParqueaderoRepositorio(IConfiguration configuration)
        {
            _ConexionBD = Conexion.ObtenerConexionBD();
        }

        public async Task<bool> RegistrarIngreso(ParqueaderoEntidad parqueadero)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TParqueadero", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehiculoID", parqueadero.VehiculoID);
                    cmd.Parameters.AddWithValue("@TipoVehiculo", parqueadero.TipoVehiculo);
                    cmd.Parameters.AddWithValue("@Placa", parqueadero.Placa);
                    cmd.Parameters.AddWithValue("@DocumentoIdentidad", parqueadero.DocumentoIdentidad);
                    cmd.Parameters.AddWithValue("@Opcion", "A");

                    var mensajeError = new SqlParameter("@MensajeError", SqlDbType.VarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(mensajeError);

                    int result = await cmd.ExecuteNonQueryAsync();

                    return string.IsNullOrEmpty(mensajeError.Value.ToString());
                }
            }
        }
        public async Task<bool> ModificarIngreso(ParqueaderoEntidad parqueadero)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TParqueadero", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IngresoID", parqueadero.IngresoID);
                    cmd.Parameters.AddWithValue("@HoraSalida", parqueadero.HoraSalida ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ValorPagado", parqueadero.ValorPagado ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DescuentoAplicado", parqueadero.DescuentoAplicado ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Opcion", "M");

                    var mensajeError = new SqlParameter("@MensajeError", SqlDbType.VarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(mensajeError);

                    int result = await cmd.ExecuteNonQueryAsync();

                    return string.IsNullOrEmpty(mensajeError.Value.ToString());
                }
            }
        }
        public async Task<bool> EliminarIngreso(int ingresoID)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TParqueadero", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IngresoID", ingresoID);
                    cmd.Parameters.AddWithValue("@Opcion", "E");

                    var mensajeError = new SqlParameter("@MensajeError", SqlDbType.VarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(mensajeError);

                    int result = await cmd.ExecuteNonQueryAsync();

                    return string.IsNullOrEmpty(mensajeError.Value.ToString());
                }
            }
        }
        public async Task<List<ParqueaderoEntidad>> ConsultarIngresosPorID(int? ingresoID = null)
        {
            var entidades = new List<ParqueaderoEntidad>();
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TParqueadero", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IngresoID", (object)ingresoID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Opcion", "ID");

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            entidades.Add(new ParqueaderoEntidad
                            {
                                IngresoID = Convert.ToInt32(reader["IngresoID"]),
                                VehiculoID = Convert.ToInt32(reader["VehiculoID"]),
                                HoraIngreso = Convert.ToDateTime(reader["HoraIngreso"]),
                                HoraSalida = reader["HoraSalida"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["HoraSalida"]),
                                ValorPagado = reader["ValorPagado"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["ValorPagado"]),
                                DescuentoAplicado = reader["DescuentoAplicado"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["DescuentoAplicado"]),
                                TipoVehiculo = reader["TipoVehiculo"].ToString(),
                                Placa = reader["Placa"].ToString(),
                                DocumentoIdentidad = reader["DocumentoIdentidad"].ToString()
                            });
                        }
                    }
                }
            }
            return entidades;
        }
    }
}
