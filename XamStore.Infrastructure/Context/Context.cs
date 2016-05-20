using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Infrastructure.EntityTypeConfiguration;

namespace XamStore.Infrastructure.Context
{
    public class Context : DbContext
    {
        public Context() : base("XamStore")
        {
        }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioNivel> UsuarioNivel { get; set; }
        public DbSet<Slide> Slide { get; set; }
        public DbSet<Newsletter> Newsletter { get; set; }
        public DbSet<Imagem> Imagem { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        //public DbSet<Jogo> Jogo { get; set; }
        //public DbSet<Produto> Produto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new CidadeMap());
            modelBuilder.Configurations.Add(new EstadoMap());
            modelBuilder.Configurations.Add(new EnderecoMap());
            modelBuilder.Configurations.Add(new PessoaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new UsuarioNivelMap());
            modelBuilder.Configurations.Add(new SlideMap());
            modelBuilder.Configurations.Add(new NewsletterMap());
            modelBuilder.Configurations.Add(new ImagemMap());
            modelBuilder.Configurations.Add(new GeneroMap());
            modelBuilder.Configurations.Add(new CategoriaMap());
            //modelBuilder.Configurations.Add(new JogoMap());
            // modelBuilder.Configurations.Add(new ProdutoMap());
        }
    }
}
