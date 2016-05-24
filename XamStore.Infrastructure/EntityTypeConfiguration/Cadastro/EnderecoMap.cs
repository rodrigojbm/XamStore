using System.Data.Entity.ModelConfiguration;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration.Cadastro
{
    public class EnderecoMap : EntityTypeConfiguration<Endereco>
    {
        public EnderecoMap()
        {
            ToTable("Endereco");
            HasKey(e => e.Id);
            Property(e => e.Id).HasColumnName("Id");
            Property(e => e.Logradouro).HasColumnName("Logradouro").IsRequired();
            Property(e => e.Complemento).HasColumnName("Complemento");
            Property(e => e.Numero).HasColumnName("Numero").IsRequired();
            Property(e => e.Cep).HasColumnName("Cep").IsRequired();
            Property(e => e.Bairro).HasColumnName("Bairro").IsRequired();


            HasRequired(p => p.Cidade)
                .WithMany()
                .HasForeignKey(p => p.IdCidade);

            HasRequired(p => p.Pessoa)
                .WithMany()
                .HasForeignKey(p => p.IdPessoa).WillCascadeOnDelete(false);
        }
    }
}
