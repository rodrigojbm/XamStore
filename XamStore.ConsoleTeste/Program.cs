using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Security.Policy;
using Microsoft.Web.Administration;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Infrastructure.Context;
using Console = System.Console;

namespace XamStore.ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            var valoresEncontrados = new List<string>();

            try
            {
                var lst = File.ReadAllLines(@"C:\Users\29502\Documents\cidades.txt");

                foreach (var item in lst)
                {
                    var algo = item.Replace(",0", string.Empty);
                    TextWriter valor = new StreamWriter(@"C:\Users\29502\Documents\novo.txt", true);
                    valor.WriteLine(algo);
                    valor.Close();
                    valor.Dispose();
                    Console.WriteLine(algo);    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Read();
        }
    }
}
