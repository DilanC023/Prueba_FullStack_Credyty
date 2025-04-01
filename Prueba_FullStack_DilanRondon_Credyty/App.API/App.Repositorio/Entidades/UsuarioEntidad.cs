using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositorio.Entidades
{
    public class UsuarioEntidad
    {
        public int UsuarioID { get; set; }
        public string? Nombre { get; set; } 
        public string? DocumentoIdentidad { get; set; } 
        public string? Email { get; set; } 
        public string? Clave { get; set; } 
        public DateTime? FechaCreacion { get; set; }
    }
}
