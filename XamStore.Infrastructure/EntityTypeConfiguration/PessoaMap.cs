using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;

namespace XamStore.Infrastructure.EntityTypeConfiguration
{
    public class PessoaMap : EntityTypeConfiguration<Pessoa>
    {
        public PessoaMap()
        {
            ToTable("Pessoa");
            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("Id");
            Property(p => p.FacebookId).HasColumnName("FacebookId");
            Property(p => p.GoogleId).HasColumnName("GoogleId");
            Property(p => p.Nome).HasColumnName("Nome").IsRequired();
            Property(p => p.Sobrenome).HasColumnName("Sobrenome").IsRequired();
            Property(p => p.Rg).HasColumnName("Rg");
            Property(p => p.Cpf).HasColumnName("Cpf").IsRequired();
            Property(p => p.DataNascimento).HasColumnName("DataNascimento").IsRequired();
            Property(p => p.Email).HasColumnName("Email").IsRequired();
            Property(p => p.SexoTipo).HasColumnName("Sexo").IsRequired();
            Property(p => p.Senha).HasColumnName("Senha").IsRequired();
            Property(p => p.PessoaTipo).HasColumnName("Tipo").IsRequired();
        }
    }
}
