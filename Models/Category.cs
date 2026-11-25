using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseAccounting.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        // Compatibility properties used by the existing Views.
        // These are not separate database columns.
        [NotMapped]
        public int Id
        {
            get => CategoryId;
            set => CategoryId = value;
        }

        [NotMapped]
        public string Name
        {
            get => CategoryName;
            set => CategoryName = value;
        }
    }
}
