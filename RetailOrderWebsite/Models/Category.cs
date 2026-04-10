using System.ComponentModel.DataAnnotations;

namespace RetailOrderWebsite.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }   // Pizza, Drinks, etc. 
        public string Description { get; set; }
        // Navigation Property 
        public List<Product> Products { get; set; }

    }
}
