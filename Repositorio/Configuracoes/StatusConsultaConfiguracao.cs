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
    class StatusConsultaConfiguracao : IEntityTypeConfiguration<StatusConsulta>
    {
        public void Configure(EntityTypeBuilder<StatusConsulta> builder) 
        {
            builder.ToTable("StatusConsulta");
            builder.HasKey("StatusConsultaId");
            builder.Property("Nome")
                .HasMaxLength(150)
                .HasColumnName("Nome");
        }
    }
}
