using System.ComponentModel.DataAnnotations;
namespace SmartBuyAPI.Models.DTO.Category
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public IFormFile Image { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
    }
}
