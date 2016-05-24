using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Operacao;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Operacao
{
    public class PedidoMap : EntityTypeConfiguration<Pedido>
    {
        public PedidoMap()
        {
            ToTable("Pedido");
            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("Id");
            Property(p => p.Status).HasColumnName("Status").IsRequired();
            Property(p => p.PagSeguroId).HasColumnName("PagseguroId").IsRequired();
            Property(p => p.Valor).HasColumnName("Valor").IsRequired();
            Property(p => p.Frete).HasColumnName("Frete");
            Property(p => p.Data).HasColumnName("Data").IsRequired();
            Property(p => p.IsNovo).HasColumnName("IsNovo").IsRequired();

            HasRequired(p => p.Pessoa)
                .WithMany()
                .HasForeignKey(p => p.IdPessoa);

            HasRequired(p => p.Endereco)
                .WithMany()
                .HasForeignKey(p => p.IdEndereco);
        }
    }
}
