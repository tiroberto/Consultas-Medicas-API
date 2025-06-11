using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dominio.Entidades;

namespace Repositorio.Configuracoes
{
    class ConsultaConfiguracao : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("Consulta");
            builder.HasKey("ConsultaId");
            builder.Property(c => c.DataHoraInicio)
                .HasColumnType("datetime")
                .HasColumnName("DataHoraInicio");
            builder.Property(c => c.DataHoraFim)
                .HasColumnType("datetime")
                .HasColumnName("DataHoraFim"); 
            builder.Property(c => c.Observacoes)
                .HasMaxLength(1000)
                .HasColumnName("Observacoes");


            builder.HasOne(c => c.StatusConsulta)
                .WithMany(c => c.Consultas)
                .HasForeignKey("StatusConsultaId");
                
            builder.HasOne(c => c.Paciente)
                .WithMany(p => p.Consultas)
                .HasForeignKey("PacienteId");
            builder.HasOne(c => c.Medico)
                .WithMany(m => m.Consultas)
                .HasForeignKey("MedicoId");
        }
    }
}
