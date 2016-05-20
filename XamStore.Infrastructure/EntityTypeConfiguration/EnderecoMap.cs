using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration
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
                .WithMany(p => p.Enderecos)
                .HasForeignKey(p => p.IdCidade);

            HasRequired(p => p.Pessoa)
                .WithMany(p => p.Endereco)
                .HasForeignKey(p => p.IdPessoa);
        }
    }
}
