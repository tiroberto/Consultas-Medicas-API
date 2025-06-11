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
    class UsuarioConfiguracao : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey("UsuarioId");
            builder.Property(u => u.Nome)
                .HasMaxLength(150)
                .HasColumnName("Nome");
            builder.Property(u => u.Email)
                .HasMaxLength(150)
                .HasColumnName("Email");
            builder.Property(u => u.SenhaHash)
                .HasMaxLength(150)
                .HasColumnName("SenhaHash");
            builder.HasOne(u => u.TipoUsuario)
                .WithMany(u => u.Usuarios)
                .HasForeignKey("TipoUsuarioId");                
        }
    }
}
