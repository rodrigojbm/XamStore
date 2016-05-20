using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration
{
    public class JogoMap : EntityTypeConfiguration<Jogo>
    {
        public JogoMap()
        {
            ToTable("Jogo");
            HasKey(j => j.Id);
            Property(j => j.Id).HasColumnName("Id");
            Property(j => j.Nome).HasColumnName("Nome");
            Property(j => j.Multiplayer).HasColumnName("Multiplayer").IsRequired();
            Property(j => j.Jogadores).HasColumnName("Jogadores").IsRequired();
        }
    }
}
