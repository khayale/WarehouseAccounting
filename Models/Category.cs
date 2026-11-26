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

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("qntt")]
        [StringLength(255)]
        public string? Qntt { get; set; }

        [Column("qnty")]
        [StringLength(255)]
        public string? Qnty { get; set; }

        [Column("qntytype")]
        [StringLength(255)]
        public string? QntyType { get; set; }

        [Column("size")]
        [StringLength(255)]
        public string? Size { get; set; }

        [Column("color")]
        [StringLength(255)]
        public string? Color { get; set; }

        [Column("weight")]
        [StringLength(255)]
        public string? Weight { get; set; }

        [Column("model")]
        [StringLength(255)]
        public string? Model { get; set; }

        [Column("company")]
        [StringLength(255)]
        public string? Company { get; set; }

        [Column("production_date")]
        public DateTime? ProductionDate { get; set; }

        [Column("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        [Column("group_name")]
        [StringLength(255)]
        public string? GroupName { get; set; }

        [Column("group_num")]
        public int? GroupNum { get; set; }
    }
}