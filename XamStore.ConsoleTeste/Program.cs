using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            var listaHostsServidor = new List<HostServidor>
            {   
                new HostServidor
                {
                    Ip = "10.0.16.180",
                    Endereco = "sdk.tjmt.jus.br"
                },

                new HostServidor
                {
                    Ip = "10.0.16.188",
                    Endereco = "cia.tjmt.jus.br"
                },

                new HostServidor
                {
                    Ip = "10.0.16.20",
                    Endereco = "tjmt.jus.br"
                }
            };


            var Iis = new List<HostIis>
            {
                new HostIis
                {
                    Ip = "10.0.16.20",
                    Endereco = "tjmt.jus.br"
                },

                 new HostIis
                {
                    Ip = "10.0.16.21",
                    Endereco = "tjmt.jus.br"
                }
            };


            foreach (var item in listaHostsServidor)
            {
                var result = Iis.Where(x => !x.Endereco.Contains(item.Endereco)).ToList();
                foreach (var host in result)
                {
                    Console.WriteLine(host.Endereco);
                    //listaHostsServidor.Add(new HostServidor {Ip = host.Ip, Endereco = host.Endereco});
                }
            }

            foreach (var hostServidor in listaHostsServidor)
            {
                Console.WriteLine(hostServidor.Endereco);
            }


            Console.Read();
        }
    }

    public class HostIis
    {
        public string Ip { get; set; }
        public string Endereco { get; set; }
    }

    public class HostServidor
    {
        public string Ip { get; set; }
        public string Endereco { get; set; }
    }
}
