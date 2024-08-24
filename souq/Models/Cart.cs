using System.ComponentModel.DataAnnotations.Schema;

namespace souq.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime ? date { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public string?  userId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }


    }
}
