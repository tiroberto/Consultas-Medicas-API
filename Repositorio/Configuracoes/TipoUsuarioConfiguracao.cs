using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Configuracoes
{
    class TipoUsuarioConfiguracao : IEntityTypeConfiguration<TipoUsuario>
    {
        public void Configure(EntityTypeBuilder<TipoUsuario> builder)
        {
            builder.ToTable("TipoUsuario");
            builder.HasKey(t => t.TipoUsuarioId);
            builder.Property(t => t.Nome)
                .HasMaxLength(150)
                .HasColumnName("Nome");
        }
    }
}
