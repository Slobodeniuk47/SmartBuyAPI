using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartBuyAPI.Models.DTO.Product
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public IFormFile Image { get; set; }
        public int? CategoryId { get; set; }
    }
}
