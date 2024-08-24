using System.ComponentModel.DataAnnotations.Schema;

namespace souq.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public string? userId { get; set; }

      
    }
}
