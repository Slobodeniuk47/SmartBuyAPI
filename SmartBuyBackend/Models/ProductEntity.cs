using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartBuyAPI.Models
{
    [Table("tbl_Products")]
    public class ProductEntity
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [StringLength(500)]
        public string ShortDescription { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [Range(1, int.MaxValue)]
        public int Price { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastEdit { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
        [DisplayName("Category")]
        //[ForeignKey("Category")]
        public int? CategoryId { get; set; } //public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryEntity Category { get; set; }
        //public virtual ICollection<ProductImageEntity> ProductImages { get; set; }
    }
}
