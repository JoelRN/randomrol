using System;
using System.Collections.Generic;

namespace RandomRol.WebApi.Entities
{
    public partial class RelUsuariosPartidas
    {
        public int IdUsuario { get; set; }
        public int IdPartida { get; set; }
        public bool EsMaster { get; set; }
    }
}
