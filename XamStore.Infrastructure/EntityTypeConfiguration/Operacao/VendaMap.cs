using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Operacao;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Operacao
{
    public class VendaMap : EntityTypeConfiguration<Venda>
    {
        public VendaMap()
        {
            ToTable("Venda");
            HasKey(v => v.Id);
            Property(v => v.Id).HasColumnName("Id");
            Property(v => v.Data).HasColumnName("Data").IsRequired();
            Property(v => v.CodigoRastreioCorreios).HasColumnName("CodigoRastreioCorreios");
            Property(v => v.IsNova).HasColumnName("IsNova").IsRequired();

            HasRequired(v => v.Pedido)
                .WithMany(v => v.Vendas)
                .HasForeignKey(v => v.IdPedido);
        }
    }
}
