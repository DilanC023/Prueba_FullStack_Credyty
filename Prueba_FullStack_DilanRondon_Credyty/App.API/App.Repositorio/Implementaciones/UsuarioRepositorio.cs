using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositorio.Interfaces;
using App.Repositorio.Entidades;
using App.Repositorio.ConexionDB;

namespace App.Repositorio.Implementaciones
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string _ConexionBD;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            _ConexionBD = Conexion.ObtenerConexionBD();
        }
        public async Task<bool> AdicionarUsuario(UsuarioEntidad usuario)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TUsuarios", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@DocumentoIdentidad", usuario.DocumentoIdentidad);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("@Opcion", "A");

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<bool> ModificarUsuario(UsuarioEntidad usuario)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TUsuarios", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Opcion", "M");

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<bool> EliminarUsuario(int usuarioID)
        {
            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TUsuarios", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    cmd.Parameters.AddWithValue("@Opcion", "E");

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<List<UsuarioEntidad>> ConsultarUsuarioPorID(int? usuarioID = null)
        {
            List<UsuarioEntidad> usuarios = new List<UsuarioEntidad>();

            using (var conn = new SqlConnection(_ConexionBD))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("sp_TUsuarios", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", (object)usuarioID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Opcion", "ID");

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usuarios.Add(new UsuarioEntidad
                            {
                                UsuarioID = reader.GetInt32(reader.GetOrdinal("UsuarioID")),
                                Nombre = reader["Nombre"].ToString()!,
                                DocumentoIdentidad = reader["DocumentoIdentidad"].ToString()!,
                                Email = reader["Email"].ToString()!,
                                Clave = reader["Clave"].ToString()!,
                                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion"))
                            });
                        }
                    }
                }
            }

            return usuarios;
        }
    }
}
