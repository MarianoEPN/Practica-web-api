using Practica_web_api.Models;
using Microsoft.EntityFrameworkCore;


namespace Practica_web_api.Data

{
    public class EjercicioDbContext : DbContext
    {
        public EjercicioDbContext(DbContextOptions<EjercicioDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuario => Set<Usuario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(p => p.Nombre).HasMaxLength(100);
            });
        }

    }
}
