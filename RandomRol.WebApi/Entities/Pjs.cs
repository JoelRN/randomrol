using System;
using System.Collections.Generic;

namespace RandomRol.WebApi.Entities
{
    public partial class Pjs
    {
        public int Id { get; set; }
        public int IdPartida { get; set; }
        public int IdPlantillaPj { get; set; }
        public bool SoloCopia { get; set; }
        public bool Publicado { get; set; }
        public string Fichero { get; set; }
    }
}
