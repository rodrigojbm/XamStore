using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
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
            Property(p => p.Preco).HasColumnName("Preco").IsRequired();
            Property(p => p.Garantia).HasColumnName("Garantia").IsRequired();
            Property(p => p.Peso).HasColumnName("Peso").IsRequired();
            Property(p => p.Estoque).HasColumnName("Estoque").IsRequired();

            HasRequired(p => p.Categoria)
                .WithMany()
                .HasForeignKey(p => p.IdCategoria);

            HasRequired(p => p.Jogo)
                .WithMany()
                .HasForeignKey(p => p.IdJogo).WillCascadeOnDelete(false);
        }
    }
}