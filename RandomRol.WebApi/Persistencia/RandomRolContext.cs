using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RandomRol.WebApi.Entities;

using RandomRol.WebApi.Helpers;

namespace RandomRol.WebApi.Persistencia
{
    public partial class RandomRolContext : DbContext
    {
        private readonly AppSettings _appSettings;

        public RandomRolContext(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public RandomRolContext(DbContextOptions<RandomRolContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<LogPartidas> LogPartidas { get; set; }
        public virtual DbSet<Partidas> Partidas { get; set; }
        public virtual DbSet<Pjs> Pjs { get; set; }
        public virtual DbSet<PlantillasPjs> PlantillasPjs { get; set; }
        public virtual DbSet<RelUsuariosPartidas> RelUsuariosPartidas { get; set; }
        public virtual DbSet<RelUsuariosPjs> RelUsuariosPjs { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<VotosPjs> VotosPjs { get; set; }
        public virtual DbSet<VotosPlantillasPjs> VotosPlantillasPjs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(_appSettings.DefaultConnection);                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogPartidas>(entity =>
            {
                entity.ToTable("LOG_Partidas");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdPartida).HasColumnType("int(11)");

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Partidas>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Cerrada).HasColumnType("bit(1)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.FechaInicio).HasColumnType("date");
            });

            modelBuilder.Entity<Pjs>(entity =>
            {
                entity.ToTable("PJs");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Fichero)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IdPartida).HasColumnType("int(11)");

                entity.Property(e => e.IdPlantillaPj)
                    .HasColumnName("IdPlantillaPJ")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Publicado).HasColumnType("bit(1)");

                entity.Property(e => e.SoloCopia).HasColumnType("bit(1)");
            });

            modelBuilder.Entity<PlantillasPjs>(entity =>
            {
                entity.ToTable("PlantillasPJs");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Fichero)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IdUsuario).HasColumnType("int(11)");

                entity.Property(e => e.Publicada).HasColumnType("bit(1)");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<RelUsuariosPartidas>(entity =>
            {
                entity.HasKey(e => new { e.IdUsuario, e.IdPartida });

                entity.ToTable("REL_Usuarios_Partidas");

                entity.Property(e => e.IdUsuario).HasColumnType("int(11)");

                entity.Property(e => e.IdPartida).HasColumnType("int(11)");

                entity.Property(e => e.EsMaster).HasColumnType("bit(1)");
            });

            modelBuilder.Entity<RelUsuariosPjs>(entity =>
            {
                entity.HasKey(e => new { e.IdUsuario, e.IdPersonaje });

                entity.ToTable("REL_Usuarios_PJs");

                entity.Property(e => e.IdUsuario).HasColumnType("int(11)");

                entity.Property(e => e.IdPersonaje).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<VotosPjs>(entity =>
            {
                entity.HasKey(e => new { e.IdUsuario, e.IdPj });

                entity.ToTable("VOTOS_PJs");

                entity.Property(e => e.IdUsuario).HasColumnType("int(11)");

                entity.Property(e => e.IdPj)
                    .HasColumnName("IdPJ")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Puntuacion).HasColumnType("int(11)");
            });

            modelBuilder.Entity<VotosPlantillasPjs>(entity =>
            {
                entity.HasKey(e => new { e.IdUsuario, e.IdPlantillaPj });

                entity.ToTable("VOTOS_PlantillasPJs");

                entity.Property(e => e.IdUsuario).HasColumnType("int(11)");

                entity.Property(e => e.IdPlantillaPj)
                    .HasColumnName("IdPlantillaPJ")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Puntuacion).HasColumnType("int(11)");
            });
        }
    }
}
