using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class ProdutoEstoqueMap : EntityTypeConfiguration<ProdutoEstoque>
    {
        public ProdutoEstoqueMap()
        {
            ToTable("ProdutoEstoque");
            HasKey(p => p.Id);
            Property(p => p.Quantidade).HasColumnName("Quantidade").IsRequired();

            HasRequired(p => p.Produto)
                .WithMany(p => p.ProdutoEstoques)
                .HasForeignKey(p => p.IdProduto);
        }
    }
}
