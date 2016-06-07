using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using XamStore.Domain.Entities.Cadastro;
using XamStore.Domain.Entities.Operacao;
using XamStore.Domain.Entities.Sistema;
using XamStore.Infrastructure.EntityTypeConfiguration.Cadastro;
using XamStore.Infrastructure.EntityTypeConfiguration.Operacao;
using XamStore.Infrastructure.EntityTypeConfiguration.Sistema;

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
        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ProdutoImagem> ProdutoImagem { get; set; }
        public DbSet<ProdutoEstoque> ProdutoEstoque { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoItem> PedidoItem { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuAdmin> MenuAdmin { get; set; }
        public DbSet<Fabricante> Fabricante { get; set; }
        public DbSet<Console> Console { get; set; }
        public DbSet<Plataforma> Plataforma { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new CidadeMap().Ignore(c => c.Enderecos));
            modelBuilder.Configurations.Add(new EstadoMap().Ignore(e => e.Cidades));
            modelBuilder.Configurations.Add(new EnderecoMap());
            modelBuilder.Configurations.Add(new PessoaMap().Ignore(p => p.EmailAutenticacao).Ignore(p => p.ConfirmaSenha).Ignore(p => p.Pedidos).Ignore(p => p.Endereco));
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new UsuarioNivelMap());
            modelBuilder.Configurations.Add(new SlideMap().Ignore(s => s.File));
            modelBuilder.Configurations.Add(new NewsletterMap());
            modelBuilder.Configurations.Add(new ImagemMap().Ignore(i => i.ProdutoImagem));
            modelBuilder.Configurations.Add(new GeneroMap());
            modelBuilder.Configurations.Add(new CategoriaMap().Ignore(c => c.Produto));
            modelBuilder.Configurations.Add(new JogoMap());
            modelBuilder.Configurations.Add(new ProdutoMap().Ignore(p => p.ProdutoImagens).Ignore(p => p.PesoString).Ignore(p => p.PrecoString));
            modelBuilder.Configurations.Add(new ProdutoImagemMap());
            modelBuilder.Configurations.Add(new ProdutoEstoqueMap());
            modelBuilder.Configurations.Add(new ContatoMap());
            modelBuilder.Configurations.Add(new PedidoMap());
            modelBuilder.Configurations.Add(new PedidoItemMap());
            modelBuilder.Configurations.Add(new VendaMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new MenuAdminMap());
            modelBuilder.Configurations.Add(new FabricanteMap());
            modelBuilder.Configurations.Add(new ConsoleMap());
            modelBuilder.Configurations.Add(new PlataformaMap());

            Database.SetInitializer<Context>(null);
        }
    }
}
