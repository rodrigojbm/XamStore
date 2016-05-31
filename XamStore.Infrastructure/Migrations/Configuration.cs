using XamStore.Domain.Entities.Cadastro;
using System;
using System.Data.Entity.Migrations;
using XamStore.Domain.Entities.Enums;

namespace XamStore.Infrastructure.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context.Context context)
        {
            context.Pessoa.AddOrUpdate(c => c.Nome, new Pessoa()
            {
                Nome = "Rodrigo",
                Sobrenome = "Maciel",
                Rg = "20102911",
                Cpf = "05015961197",
                DataNascimento = DateTime.Parse("21-04-1995"),
                Email = "rodrigojbm@hotmail.com",
                Senha = "1234",
                PessoaTipo = PessoaTipoEnum.Fisica,
                SexoTipo = SexoTipoEnum.Masculino
            });

            context.SaveChanges();

            context.Pessoa.AddOrUpdate(c => c.Nome, new Pessoa()
            {
                Nome = "Jamal",
                Sobrenome = "Malik",
                Cpf = "1231244112",
                DataNascimento = DateTime.Parse("12-04-1990"),
                Email = "jamal@hotmail.com",
                SexoTipo = SexoTipoEnum.Masculino,
                Senha = "1234",
                PessoaTipo = PessoaTipoEnum.Juridica
            });

            context.SaveChanges();

            context.Estado.AddOrUpdate(c => c.Nome,
                new Estado()
                {
                    Nome = "Mato Grosso",
                    Abreviacao = "MT"
                });

            context.SaveChanges();

            context.Cidade.AddOrUpdate(c => c.Nome, new Cidade()
            {
                Nome = "Cuiab�",
                IdEstado = 1,
            });

            context.SaveChanges();

            context.Endereco.AddOrUpdate(c => c.Logradouro, new Endereco()
            {
                Logradouro = "Rua 3.100",
                Numero = "20",
                Cep = "78075735",
                Bairro = "Jardim imperial",
                IdCidade = 1,
                IdPessoa = 1
            });

            context.SaveChanges();
        }
    }
}
