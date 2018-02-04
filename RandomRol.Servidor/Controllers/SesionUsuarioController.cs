using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomRol.Servidor.Modelos.SesionUsuario;

namespace RandomRol.Servidor.Controllers
{
    [Route("api/[controller]")]
    public class SesionUsuarioController : Controller
    {
        // POST api/values
        [HttpPost]
        public ActionResult AutenticaUsuario([FromBody] AutenticaUsuarioDTO autenticaUsuario)
        {
            try
            {
                return Ok(autenticaUsuario.Nombre.Equals("admin") && autenticaUsuario.Pwd.Equals("admin"));
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno de servidor. Causa: " + e.Message.ToString());
            }
        }

    }
}
