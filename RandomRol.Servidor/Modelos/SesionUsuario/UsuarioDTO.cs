using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomRol.Servidor.Modelos.SesionUsuario
{
    public class UsuarioDTO
    {
        public string IdUsuario { get; set; } 
        public string Nombre { get; set; }
        public string Contraseña { get; set; }        
    }
}
