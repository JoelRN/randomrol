using System;
using System.Collections.Generic;
using System.Linq;
using RandomRol.WebApi.Entities;
using RandomRol.WebApi.Helpers;

namespace RandomRol.WebApi.Services
{
    public interface IUsuariosService
    {
        Usuarios Authenticate(string alias, string password);
        IEnumerable<Usuarios> GetAll();
        Usuarios GetById(int id);
        Usuarios Create(Usuarios user, string password);
        void Update(Usuarios user, string password = null);
        void Delete(int id);
    }

    public class UsuarioService : IUsuariosService
    {
        private DataContext _context;

        public UsuarioService(DataContext context)
        {
            _context = context;
        }

        public Usuarios Authenticate(string alias, string password)
        {
            if (string.IsNullOrEmpty(alias) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Usuarios.SingleOrDefault(x => x.Alias == alias);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<Usuarios> GetAll()
        {
            return _context.Usuarios;
        }

        public Usuarios GetById(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuarios Create(Usuarios user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Usuarios.Any(x => x.Alias == user.Alias))
                throw new AppException("Alias " + user.Alias + " en uso");

            if (_context.Usuarios.Any(x => x.Email == user.Email))
                throw new AppException("Email " + user.Email + " en uso");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(Usuarios userParam, string password = null)
        {
            var user = _context.Usuarios.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.Alias != user.Alias)
            {
                // username has changed so check if the new username is already taken
                if (_context.Usuarios.Any(x => x.Alias == userParam.Alias))
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

            _context.Usuarios.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Usuarios.Find(id);
            if (user != null)
            {
                _context.Usuarios.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

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
    }
}