using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration
{
    public class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public ProdutoMap()
        {
            ToTable("Produto");
            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("Id");
            Property(p => p.Nome).HasColumnName("Nome").IsRequired();
            Property(p => p.Descricao).HasColumnName("Descricao");
            Property(p => p.Estoque).HasColumnName("Estoque").IsRequired();
            Property(p => p.Preco).HasColumnName("Preco").IsRequired();
        }
    }
}
