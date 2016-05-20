using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Infrastructure.Context;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Domain.Entities.Enums;

namespace XamStore.ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new Context();

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
                Nome = "Cuiabá",
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
