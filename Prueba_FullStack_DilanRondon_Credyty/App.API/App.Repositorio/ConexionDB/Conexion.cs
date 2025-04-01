using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositorio.ConexionDB
{
    public static class Conexion
    {
        private static string? _cadenaConexionBD;
        public static void Inicializar(IConfiguration configuration)
        {
            _cadenaConexionBD = configuration.GetConnectionString("DefaultConnection");
        }

        public static string ObtenerConexionBD()
        {
            if (string.IsNullOrEmpty(_cadenaConexionBD))
                throw new InvalidOperationException("La cadena de conexion no ha sido inicializada.");

            return _cadenaConexionBD;
        }
    }
}
