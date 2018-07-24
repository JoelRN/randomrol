using System;
using System.Collections.Generic;

namespace RandomRol.WebApi.Entities
{
    public partial class Partidas
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public bool Cerrada { get; set; }
    }
}
