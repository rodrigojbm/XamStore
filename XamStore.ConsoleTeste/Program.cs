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

            context.Endereco.AddOrUpdate(c => c.Logradouro,
               new Endereco()
               {
                   Logradouro = "Rua Três Mil e Cem",
                   Numero = "16",
                   Bairro = "Jardim Imperial",
                   Cep = "78075735",
                   Complemento = "Proximo a alguma coisa",
                   IdCidade = 1,
                   IdPessoa = 1
               });

            context.SaveChanges();
        }
    }
}
