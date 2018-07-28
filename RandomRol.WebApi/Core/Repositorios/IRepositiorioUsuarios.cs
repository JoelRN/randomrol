using RandomRol.WebApi.Entities;

namespace RandomRol.WebApi.Core.Repositorios
{
    public interface IRepositiorioUsuarios : IRepositorio<Usuarios>
    {
        Usuarios Crear(Usuarios user, string password);

        Usuarios Autenticar(string alias, string password);

        void Actualizar(Usuarios userParam, string password = null);
    }
}
