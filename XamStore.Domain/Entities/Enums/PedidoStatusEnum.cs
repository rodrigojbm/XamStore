using System.ComponentModel;

namespace XamStore.Domain.Entities.Enums
{
    public enum PedidoStatusEnum
    {
        [Description("Aguardando pagamento")]
        WaitingPayment = 1,

        [Description("Em análise")] 
        InAnalysis = 2,

        [Description("Paga, em fase de envio")]
        Paid = 3,

        [Description("Disponível")]
        Avaliable = 4,

        [Description("Em disputa")]
        InDispute = 5,

        [Description("Devolvida")]
        Refunded = 6,

        [Description("Cancelada")]
        Cancelled = 7,

        [Description("Em fase de entrega")]
        Delivering = 8,

        [Description("Entregue")]
        Delivered = 9
    }
}