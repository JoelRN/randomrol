namespace RandomRol.WebApi.Entities
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}