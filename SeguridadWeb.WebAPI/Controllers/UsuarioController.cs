using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SeguridadWeb.WebAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using SeguridadWeb.LogicaDeNegocio;
using SeguridadWeb.EntidadesDeNegocio;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SeguridadWeb.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private UsuarioBL userBL = new UsuarioBL();
        //Codigo seguridad JWT
        private readonly IJwtAuthenticationService authService;
        public UsuarioController(IJwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }
        //******************************
        // GET: api/<UsuarioController>                                                                 
        [HttpGet]
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await userBL.ObtenerTodosAsync();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<Usuario> Get(int id)
        {
            Usuario user = new Usuario();
            user.Id = id;
            return await userBL.ObtenerPorIdAsync(user);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario user)
        {
            try
            {
                await userBL.CrearAsync(user);
                return Ok();

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] object pUsuario)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
            if (usuario.Id == id)
            {
                await userBL.ModificarAsync(usuario);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Usuario user = new Usuario();
                user.Id = id;
                await userBL.EliminarAsync(user);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpPost("Buscar")]
        public async Task<List<Usuario>> Buscar([FromBody] object pUsuario)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario user = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
            var usuarios = await userBL.BuscarIncluirRolesAsync(user);
            usuarios.ForEach(s => s.Rol.Usuario = null);
            
            return usuarios;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] object pUsuario)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario user = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
            //codigo para autorizar el usuario por JWT
            Usuario usuario_auth = await userBL.LoginAsync(user);
            if (usuario_auth != null && usuario_auth.Id > 0 && user.Login == usuario_auth.Login)
            {
                var token = authService.Authenticate(usuario_auth);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
            //********************************************
        }
        [HttpPost("CambiarPassword")]
        public async Task<ActionResult> CambiarPassword([FromBody] Object pUsuario)
        {
            try
            {
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                string strUsuario = JsonSerializer.Serialize(pUsuario);
                Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
                await userBL.CambiarPasswordAsync(usuario, usuario.ConfirmPassword_aux);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}