using System;
using XamStore.Domain.Entities.Enums;

namespace XamStore.Domain.Entities.Cadastro
{
    public class Newsletter
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Email { get; set; }
        public StatusEnum Status { get; set; }
    }
}
