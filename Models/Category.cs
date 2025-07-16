using System.ComponentModel.DataAnnotations;

namespace Saas.Models
{
    public class Category
    {
        public Category()
        {
            
        }
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Product> products { get; set; }
    }

}
