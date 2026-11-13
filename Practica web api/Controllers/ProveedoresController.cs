using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Practica_web_api.Data;
using Practica_web_api.Models;

namespace Practica_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly EjercicioDbContext _context;

        public ProveedoresController(EjercicioDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Proveedor>> CrearProveedor(
            [FromBody] Proveedor proveedor)
        {
            _context.Proveedor.Add(proveedor);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(CrearProveedor),
                new { id = proveedor.Id },
                proveedor
            );
        }
    }
}
