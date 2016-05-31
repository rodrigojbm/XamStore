namespace XamStore.Domain.Entities.Cadastro
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }

        public int IdCidade { get; set; }
        public virtual Cidade Cidade { get; set; }

        public int IdPessoa { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        //public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
