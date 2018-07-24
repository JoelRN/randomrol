using System;
using System.Collections.Generic;

namespace RandomRol.WebApi.Entities
{
    public partial class Usuarios
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
