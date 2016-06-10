using System.Data.Entity.Migrations;
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

            var sites = server.Sites;
            foreach (var site in sites)
            {
                var defaults = site.ApplicationDefaults;

                var appPoolName = defaults.ApplicationPoolName;

                var attributes = defaults.Attributes;
                foreach (var configAttribute in attributes)
                {
                    Console.WriteLine(configAttribute.Name);
                    Console.WriteLine(appPoolName);
                }
            }
            Console.Read();
        }
    }
}
