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
    class EspecialidadeConfiguracao : IEntityTypeConfiguration<Especialidade>
    {
        public void Configure(EntityTypeBuilder<Especialidade> builder)
        {
            builder.ToTable("Especialidade");
            builder.HasKey("EspecialidadeId");
            builder.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("Nome");
        }
    }
}
