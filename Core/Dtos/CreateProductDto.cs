using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class CreateProductDto
    {
        public decimal Price { get; set; }
        public decimal? cutPrice { get; set; }
        [Required]
        public IFormFile[] Files { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int mainPhotoIndex { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "nie wybrano brand")]
        public int BrandOfProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "nie wybrano Category")]
        public int CategoryOfProductId { get; set; }
    }
}
