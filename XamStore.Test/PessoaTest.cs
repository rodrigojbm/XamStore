using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Domain.Entities.Enums;
using XamStore.Infrastructure.Context;
using Xunit;

namespace XamStore.Test
{
    public class PessoaTest
    {
        [Fact]
        public void Deve_Inserir_Pessoa_no_banco()
        {
            var context = new Context();
            var pessoa = new Pessoa
            {
                Nome = "Rodrigo",
                Sobrenome = "Maciel",
                Rg = "231321321",
                Cpf = "0501241233",
                DataNascimento = DateTime.Parse("21/04/1995"),
                Email = "rodrigojbm@hotmail.com",
                SexoTipo = SexoTipoEnum.Masculino,
                Senha = "1234",
                PessoaTipo = PessoaTipoEnum.Fisica
            };

            context.Pessoa.AddOrUpdate(pessoa);
            var result = context.Pessoa.Find("Rodrigo");

            Assert.Equal(pessoa, result);
        }
    }
}
