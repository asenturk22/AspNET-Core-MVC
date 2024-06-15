namespace Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public String? CategoryName { get; set; } = String.Empty;
        
        //COllection navigaton property
        public ICollection<Product>  Products {get; set; }  
        
    }
}