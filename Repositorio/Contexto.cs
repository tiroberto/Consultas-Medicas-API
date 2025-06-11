using Comum.NotificationPattern;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio.Configuracoes;
using System;

namespace Repositorio
{
    public class Contexto : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoUsuario> TiposUsuarios { get; set; }
        public DbSet<StatusConsulta> StatusConsulta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging(true);
                optionsBuilder.UseSqlite("Data Source=database.dat");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<NotificationError>();
            modelBuilder.Ignore<NotificationResult>();
            modelBuilder.Ignore<NotificationResultException>();
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracao());
            modelBuilder.ApplyConfiguration(new PacienteConfiguracao());
            modelBuilder.ApplyConfiguration(new MedicoConfiguracao());
            modelBuilder.ApplyConfiguration(new ConsultaConfiguracao());
            modelBuilder.ApplyConfiguration(new EspecialidadeConfiguracao());
            modelBuilder.ApplyConfiguration(new TipoUsuarioConfiguracao());
            modelBuilder.ApplyConfiguration(new StatusConsultaConfiguracao());
        }

    }
}
