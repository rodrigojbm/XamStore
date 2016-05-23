using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Operacao;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Operacao
{
    public class PedidoItemMap : EntityTypeConfiguration<PedidoItem>
    {
        public PedidoItemMap()
        {
            ToTable("PedidoItem");
            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("Id");
            Property(p => p.Quantidade).HasColumnName("Quantidade").IsRequired();

            HasRequired(p => p.Pedido)
                .WithMany()
                .HasForeignKey(p => p.IdPedido);

            HasRequired(p => p.Produto)
                .WithMany(p => p.PedidoItens)
                .HasForeignKey(p => p.IdProduto);
        }
    }
}
