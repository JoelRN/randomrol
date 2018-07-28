using RandomRol.WebApi.Core;
using RandomRol.WebApi.Entities;
using RandomRol.WebApi.Core.Repositorios;
using RandomRol.WebApi.Persistencia.Repositorios;

namespace RandomRol.WebApi.Persistencia
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RandomRolContext _context;

        public UnitOfWork(RandomRolContext context)
        {
            _context = context;
            Usuarios = new RepositiorioUsuarios(_context);
        }

        public IRepositiorioUsuarios Usuarios { get; private set; }
        
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}