using System.Web;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Slide
    {
        public int Id { get; set; }
        public virtual HttpPostedFileBase File { get; set; }
        public string Imagem { get; set; }

        public int IdProduto { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
