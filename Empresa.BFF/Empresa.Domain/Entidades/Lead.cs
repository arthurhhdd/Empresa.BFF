using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Domain.Entidades
{
    public class Lead
    {
        [Key]
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StatusId { get; set; }
        public int IdClient { get; set; }
        public DateTime CreationDate { get; set; }
        public Client Client { get; set; }
    }
}
