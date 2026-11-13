using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica_web_api.Models;
using Practica_web_api.Repositories;
using Practica_web_api.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Practica_web_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _repo;

        public UsuariosController(UsuarioRepository repo) {
            _repo = repo;
        }

        /*[HttpGet]
        public  List<Usuario> obtenerUsuarios() {
            return _repo.GetUsuarios();
        }*/
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> obtenerUsuario()
        {
            var usuarios = await _repo.GetUsuarios();
            return Ok(usuarios);
        }


        /*[HttpGet("{id}")]
        public ActionResult<Usuario> obtenerPorId(int id) {
            var usuario = _repo.BuscarPorId(id);
            if (usuario == null) {
                return NotFound();
            }
            return Ok(usuario);
        }*/
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> ObtenerPorId(int id)
        {
            var usuario = await _repo.GetUsuarioAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        /*[HttpPost]
        public ActionResult agregar([FromBody] Usuario usu) {
            var usuario = new Usuario()
            {
                Nombre = usu.Nombre,
                Email = usu.Email,
                Contra = usu.Contra
            };

            _repo.SetUsuario(usuario);
            return CreatedAtAction(nameof(obtenerPorId), new { id = usuario.Id}, usuario);
        }*/

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Agregar([FromBody] UsuarioUpdateDTO dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Contra = dto.Contra
            };
            await _repo.AgregarUsuarioAsync(usuario);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = usuario.Id },
           usuario);
        }

        /*[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var usuario = _repo.BuscarPorId(id);

            if (usuario == null)
                return NotFound();

            _repo.Remove(id);

            return NoContent();
        }*/
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Eliminar(int id)
        {
            bool eliminado = await _repo.EliminarUsuarioPorIdAsync(id);
            if (!eliminado)
            {
                return NotFound();
            }
            return NoContent();
        }


        /*[HttpPatch("{id}")]
        public ActionResult Actualizar(int id, [FromBody] UsuarioUpdateDTO request)
        {
            var usuario = _repo.BuscarPorId(id);

            if (usuario == null)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            if (request.Nombre == null &&
                request.Email == null &&
                request.Contra == null)
            {
                return BadRequest(new
                {
                    mensaje = "Debe proporcionar al menos un campo"
                });
            }

            if (request.Nombre != null)
                usuario.Nombre = request.Nombre;

            if (request.Email != null)
                usuario.Email = request.Email;

            if (request.Contra != null)
                usuario.Contra = request.Contra;

            _repo.Update(usuario);

            return Ok(new
            {
                mensaje = "Usuario actualizado correctamente"
            });
        }*/
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] UsuarioUpdateDTO dto)
        {
            var usuario = new Usuario
            {
                Id = id,
                Nombre = dto.Nombre,
                Email = dto.Email,
                Contra = dto.Contra
            };
            bool actualizado = await _repo.UpdateUsuarioAsync(usuario);
            if (!actualizado)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}
