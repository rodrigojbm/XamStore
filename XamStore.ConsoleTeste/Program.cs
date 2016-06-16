using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
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
            var server = new ServerManager();
            const string b = @"C:\Users\Gerson\Desktop\GErson\Desenvolvimento\hosts.txt";
            var c = File.CreateText(b);

            foreach (var bindings in server.Sites.Select(site => site.Bindings))
            {
                try
                {
                    if (bindings == null) continue;
                    foreach (var binding in bindings)
                    {
                        Console.Write(binding.EndPoint.Address);
                        Console.Write("  ");
                        Console.WriteLine(binding.Host);

                        c.Write(binding.EndPoint.Address);
                        c.Write("    ");
                        c.WriteLine(binding.Host);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            c.Close();

            Console.Read();
        }
    }
}
