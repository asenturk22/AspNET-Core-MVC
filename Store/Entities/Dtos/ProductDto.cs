using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    /* 
    record ifadeleri immutable'dır. Constructor gibi ifadeleri kullanma imaknı verirler. class' dan çok fazla bir farkı yoktur. Sadece derleyicinin arka planda record taglar ile arka planda alan yapılarının oluşması ile ilgili davranışları değiştiren bir yapısı var. 

    Bu ifade immutable olacaksa  prop ifadelerinde set; kullanılmaz. 
    ancak init; ifadesi kullanılır nesne oluşturulurken set eder ve artik degistirilemez olur. 
     */
    public record ProductDto
    {
        public int ProductId { get; init; }

        [Required(ErrorMessage = "ProductName is required.")]

        public String? ProductName { get; init; } = String.Empty;

        [Required(ErrorMessage = "Price is required.")]

        public decimal Price { get; init; }

        public String? Summary { get; init; } = String.Empty;

        public String? ImageUrl { get; set; }

        public int? CategoryId { get; init; }        
    }
}