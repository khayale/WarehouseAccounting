using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseAccounting.Models
{
    // Maps to the actual table: storesdata.categories
    [Table("categories", Schema = "storesdata")]
    public class Category
    {
        [Key]
        [Column("categ_num")]
        [StringLength(255)]
        public string Id { get; set; } = string.Empty;

        [Column("categ_name")]
        [StringLength(255)]
        public string? Name { get; set; }

        // Compatibility with existing Create/Edit/Delete views.
        // This property is not stored in SQL because the table has no Description column.
        [NotMapped]
        public string? Description { get; set; }
    }
}
