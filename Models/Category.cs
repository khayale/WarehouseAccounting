using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseAccounting.Models
{
    [Table("categories", Schema = "dbo")]
    public class Category
    {
        [Key]
        [Column("categ_num")]
        [StringLength(255)]
        public string Id { get; set; } = string.Empty;

        [Column("categ_name")]
        [StringLength(255)]
        public string? Name { get; set; }

        [NotMapped]
        public string? Description { get; set; }
    }
}
