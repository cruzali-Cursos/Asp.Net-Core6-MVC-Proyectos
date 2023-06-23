using Aprende_ASPNETCoreMVC6.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Aprende_ASPNETCoreMVC6
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        

        // Se indica que Tarea es una entidad
        public DbSet<Tarea> Tareas { get; set; }
    }
}
