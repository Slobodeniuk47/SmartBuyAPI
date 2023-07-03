using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBuyAPI.Models
{
    [Table("tbl_Categories")]
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastEdit { get; set; }
        [Required(ErrorMessage = "Please enter category name."), StringLength(255)]
        public string Name { get; set; }
        public int Priority { get; set; }
        [Required]
        [DisplayName("Display Order")]
        [Range(1, int.MaxValue, ErrorMessage = "Order must be more then 0")]
        public int DisplayOrder { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
    }
}
