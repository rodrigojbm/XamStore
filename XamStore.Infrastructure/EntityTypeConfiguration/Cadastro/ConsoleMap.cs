using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = XamStore.Domain.Entities.Cadastro.Console;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class ConsoleMap : EntityTypeConfiguration<Console>
    {
        public ConsoleMap()
        {
            ToTable("Console");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nome).HasColumnName("Nome").IsRequired();

            HasRequired(c => c.Fabricante)
                .WithMany()
                .HasForeignKey(c => c.IdFabricante);
        }
    }
}
