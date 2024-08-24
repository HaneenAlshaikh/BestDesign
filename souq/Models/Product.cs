using System.ComponentModel.DataAnnotations.Schema;

namespace souq.Models
{
    public class Product
    {
        public int id { get; set; }
        public string? subject { get; set; }

        public string? description { get; set; }

        public float price { get; set; }

        public string? image { get; set; }

        public DateOnly? date { get; set; }
        [NotMapped]
        public IFormFile? imagefile { get; set; }
        [ForeignKey("category")]
        public int ? categoryId { get; set; }
        public Category? category { get; set; }
    }
}
