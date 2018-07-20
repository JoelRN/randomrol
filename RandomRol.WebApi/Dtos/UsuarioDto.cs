namespace RandomRol.WebApi.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }        
        public string Password { get; set; }
    }
}