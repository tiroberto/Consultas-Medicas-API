using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Repositorio.Configuracoes
{
    class PacienteConfiguracao : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Paciente");
            builder.HasKey("PacienteId");
            builder.Property(p => p.Nome)
                .HasMaxLength(150)
                .HasColumnName("Nome");
            builder.Property(p => p.CPF)
                .HasMaxLength(11)
                .HasColumnName("CPF");
            builder.Property(p => p.DataNascimento)
                .HasColumnType("date")
                .HasColumnName("DataNascimento");                
            builder.Property(p => p.Telefone)
                .HasMaxLength(15)
                .HasColumnName("Telefone");
            builder.Property(p => p.Email)
                .HasMaxLength(150)
                .HasColumnName("Email");
        }
    }
}
