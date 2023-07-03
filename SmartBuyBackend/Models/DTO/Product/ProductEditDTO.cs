using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartBuyAPI.Models.DTO.Product
{
    public class ProductEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public IFormFile ImageUpload { get; set; }
        public int? CategoryId { get; set; }
        
    }
}
