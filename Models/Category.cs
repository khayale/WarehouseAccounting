using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseAccounting.Models
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [Column("categ_num")]
        [Required]
        [StringLength(255)]
        public string CategoryId { get; set; } = string.Empty;

        [Column("categ_name")]
        [StringLength(255)]
        public string? CategoryName { get; set; }

        [NotMapped]
        public string Id
        {
            get => CategoryId;
            set => CategoryId = value;
        }

        [NotMapped]
        public string? Name
        {
            get => CategoryName;
            set => CategoryName = value;
        }
    }
}
