using System.Collections.Generic;
using XamStore.Domain.Entities.Cadastro;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
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
            context.Pessoa.AddOrUpdate(c => c.Nome,
                new Pessoa()
                {
                    Nome = "Rodrigo",
                    Sobrenome = "Maciel",
                    Rg = "20102911",
                    Cpf = "05015961197",
                    DataNascimento = DateTime.Parse("21-04-1995"),
                    Email = "rodrigojbm@hotmail.com",
                    Senha = "1234",
                    PessoaTipo = PessoaTipo.Fisica,
                    SexoTipo = SexoTipo.Masculino
                });
          
            context.SaveChanges();
        }
    }
}
