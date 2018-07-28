using RandomRol.WebApi.Core.Repositorios;
using System;

namespace RandomRol.WebApi.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositiorioUsuarios Usuarios { get; }
        int Complete();
    }
}