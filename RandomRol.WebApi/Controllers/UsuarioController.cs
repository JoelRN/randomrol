using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RandomRol.WebApi.Services;
using RandomRol.WebApi.Dtos;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using RandomRol.WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using RandomRol.WebApi.Entities;
using Microsoft.AspNetCore.Authorization;

namespace RandomRol.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private IUsuariosService _usuarioService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsuarioController(
            IUsuariosService usuarioService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UsuarioDto usuarioDto)
        {
            var user = _usuarioService.Authenticate(usuarioDto.Alias, usuarioDto.Password);

            if (user == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new {
                Id = user.Id,
                Alias = user.Alias,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Registro([FromBody]UsuarioDto usuarioDto)
        {
            // map dto to entity
            var usuario = _mapper.Map<Usuarios>(usuarioDto);

            try 
            {
                // save 
                _usuarioService.Create(usuario, usuarioDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var usuarios = _usuarioService.GetAll();
            var usuariosDtos = _mapper.Map<IList<UsuarioDto>>(usuarios);
            return Ok(usuariosDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = _usuarioService.GetById(id);
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            return Ok(usuarioDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UsuarioDto usuarioDto)
        {
            // map dto to entity and set id
            var user = _mapper.Map<Usuarios>(usuarioDto);
            user.Id = id;

            try 
            {
                // save 
                _usuarioService.Update(user, usuarioDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _usuarioService.Delete(id);
            return Ok();
        }
    }
}
