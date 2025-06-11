using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositorio.Configuracoes
{
    class MedicoConfiguracao : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("Medico");
            builder.HasKey("MedicoId");
            builder.Property(m => m.Nome)
                .HasMaxLength(150)
                .HasColumnName("Nome");
            builder.Property(m => m.CRM)
                .HasMaxLength(15)
                .HasColumnName("CRM");
            builder.Property(m => m.Telefone)
                .HasMaxLength(15)
                .HasColumnName("Telefone");
            builder.Property(m => m.Email)
                .HasMaxLength(150)
                .HasColumnName("Email");
            builder.HasOne(m => m.Especialidade)
                .WithMany(e => e.Medicos)
                .HasForeignKey("EspecialidadeId");
        }
    }
}
