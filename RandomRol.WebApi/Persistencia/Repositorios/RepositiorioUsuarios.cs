using RandomRol.WebApi.Core.Repositorios;
using RandomRol.WebApi.Entities;
using RandomRol.WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomRol.WebApi.Persistencia.Repositorios
{
    public class RepositiorioUsuarios : Repositorio<Usuarios>, IRepositiorioUsuarios
    {
        public RepositiorioUsuarios(RandomRolContext context) : base(context)
        {
        }
     
        public Usuarios Crear(Usuarios user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Contraseña requerida");

            if (RandomRolContext.Usuarios.Any(x => x.Alias == user.Alias))
                throw new AppException("Alias '" + user.Alias + "' en uso");

            if (RandomRolContext.Usuarios.Any(x => x.Email == user.Email))
                throw new AppException("Email '" + user.Email + "' en uso");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            RandomRolContext.Usuarios.Add(user);
            RandomRolContext.SaveChanges();

            return user;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public Usuarios Autenticar(string alias, string password)
        {
            if (string.IsNullOrEmpty(alias) || string.IsNullOrEmpty(password))
                return null;

            var user = RandomRolContext.Usuarios.SingleOrDefault(x => x.Alias == alias);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public void Actualizar(Usuarios userParam, string password = null)
        {
            var user = RandomRolContext.Usuarios.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.Alias != user.Alias)
            {
                // username has changed so check if the new username is already taken
                if (RandomRolContext.Usuarios.Any(x => x.Alias == userParam.Alias))
                    throw new AppException("Alias " + userParam.Alias + " is already taken");
            }

            // update user properties
            user.Alias = userParam.Alias;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            RandomRolContext.Usuarios.Update(user);
            RandomRolContext.SaveChanges();
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public RandomRolContext RandomRolContext
        {
            get { return Context as RandomRolContext; }
        }
    }
}