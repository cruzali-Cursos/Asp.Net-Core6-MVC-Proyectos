﻿using Aprende_ASPNETCoreMVC6.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Aprende_ASPNETCoreMVC6
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        // API FLUENT
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Tarea>().Property(t => t.Titulo).HasMaxLength(250).IsRequired();
        }
        

        // Se indica que Tarea es una entidad
        public DbSet<Tarea> Tareas { get; set; }
    }
}
