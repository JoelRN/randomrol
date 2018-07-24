using System;
using System.Collections.Generic;

namespace RandomRol.WebApi.Entities
{
    public partial class VotosPjs
    {
        public int IdUsuario { get; set; }
        public int IdPj { get; set; }
        public int Puntuacion { get; set; }
    }
}
