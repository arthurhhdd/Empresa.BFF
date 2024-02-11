using System.ComponentModel.DataAnnotations;

namespace Empresa.Domain.Entidades
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }
        public string NameClient { get; set; }
        public string Suburb { get; set; }
        public string Phone { get; set; }
        public string email { get; set; }
    }
}