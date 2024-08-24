namespace souq.Models
{
    public class indexMV
    {
        public indexMV()
        {
            Categories= new List<Category>();
            Products= new List<Product>();
        }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set;}
    }
}
