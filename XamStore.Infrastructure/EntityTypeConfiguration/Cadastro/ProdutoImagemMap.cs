using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class ProdutoImagemMap : EntityTypeConfiguration<ProdutoImagem>
    {
        public ProdutoImagemMap()
        {
            ToTable("ProdutoImagem");
            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("Id");

            HasRequired(p => p.Produto)
                .WithMany(p => p.ProdutoImagens)
                .HasForeignKey(p => p.IdProduto);

            HasRequired(p => p.Imagem)
                .WithMany()
                .HasForeignKey(p => p.IdImagem);
        }
    }
}
