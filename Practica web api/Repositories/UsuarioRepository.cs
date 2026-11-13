using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Practica_web_api.Data;
using Practica_web_api.Models;

namespace Practica_web_api.Repositories
{
    public class UsuarioRepository
    {
        private readonly EjercicioDbContext _context;

        public UsuarioRepository(EjercicioDbContext context) {

            _context = context;
    
        }

        /*public List<Usuario> GetUsuarios() { 
            return _usuarios;
        }*/
        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.Usuario.AsNoTracking().ToListAsync();
        }
        /*public Usuario? BuscarPorId(int id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }*/
        public async Task<Usuario?> GetUsuarioAsync(int id)
        {
            return await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        /*public void SetUsuario(Usuario usuario) {
            usuario.Id = _usuarios.Any() ? _usuarios.Max(u => u.Id) +1 : 1;
            _usuarios.Add(usuario);
        }*/
        public async Task<Usuario> AgregarUsuarioAsync(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

       

        /*public eliminarPorId(int id) {
            Usuario? usuario = buscarPorId(id);
            if (usuario != null) {
                _usuarios.Remove(usuario);
                return true;
            }
            
        }*/
        // Actualizar usuario
        /*public bool Update(Usuario usuario)
        {
            int indice = _usuarios.FindIndex(u => u.Id == usuario.Id);

            if (indice == -1)
                return false;

            _usuarios[indice] = usuario;

            return true;
        }*/
        public async Task<bool> UpdateUsuarioAsync(Usuario usuario)
        {
            var existente = await _context.Usuario.FindAsync(usuario.Id);
            if (existente == null)
            {
                return false;
            }
            existente.Nombre = usuario.Nombre;
            existente.Email = usuario.Email;
            existente.Contra = usuario.Contra;

            await _context.SaveChangesAsync();
            return true;

        }

        // Eliminar usuario
        /*public bool Remove(int id)
        {
            var usuario = BuscarPorId(id);

            if (usuario == null)
                return false;

            return _usuarios.Remove(usuario);
        }*/
        public async Task<bool> EliminarUsuarioPorIdAsync(int id)
        {
            var existente = await _context.Usuario.FindAsync(id);
            if (existente == null) return false;

            _context.Usuario.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
