using Jean_P2_AP1.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace DAL
{
	public class Contexto : DbContext
	{
        public DbSet<Proyectos> Proyectos { get; set; }
        public DbSet<TiposTarea> TiposTarea { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(@"Data Source=Data/Proyectos.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TiposTarea>().HasData(new List<TiposTarea>()
                {
                    new TiposTarea{ TipoId = 1, Descripcion = "Análisis"},
                    new TiposTarea{ TipoId = 2, Descripcion = "Diseño"},
                    new TiposTarea{ TipoId = 3, Descripcion = "Programación"}
                }
            );
        }
    }
}
