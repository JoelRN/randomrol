using System;
using System.Collections.Generic;

namespace RandomRol.WebApi.Entities
{
    public partial class PlantillasPjs
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Descripcion { get; set; }
        public string Version { get; set; }
        public bool Publicada { get; set; }
        public string Fichero { get; set; }
    }
}
