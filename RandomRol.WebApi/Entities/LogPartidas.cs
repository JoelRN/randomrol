using System;
using System.Collections.Generic;

namespace RandomRol.WebApi.Entities
{
    public partial class LogPartidas
    {
        public int Id { get; set; }
        public int IdPartida { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
    }
}
