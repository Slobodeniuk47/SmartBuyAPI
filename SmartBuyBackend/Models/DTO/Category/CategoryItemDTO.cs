using System.ComponentModel.DataAnnotations;
namespace SmartBuyAPI.Models.DTO.Category
{
    public class CategoryItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public string Image { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
    }
}
