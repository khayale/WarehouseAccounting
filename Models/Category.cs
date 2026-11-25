using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseAccounting.Models
{
    public class Category
    {
        [Key]
        [Column("categ_num")]
        [StringLength(255)]
        public string Id { get; set; } = string.Empty;

        [Column("categ_name")]
        [StringLength(255)]
        public string? Name { get; set; }

        // Used by the existing Views only; the SQL table has no Description column.
        [NotMapped]
        public string? Description { get; set; }
    }
}
