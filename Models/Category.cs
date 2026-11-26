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
        public string categ_num { get; set; } = string.Empty;

        [Column("categ_name")]
        [StringLength(255)]
        public string? categ_name { get; set; }

        [Column("price")]
        public decimal? price { get; set; }

        [Column("qntt")]
        [StringLength(255)]
        public string? qntt { get; set; }

        [Column("qnty")]
        [StringLength(255)]
        public string? qnty { get; set; }

        [Column("qntytype")]
        [StringLength(255)]
        public string? qntytype { get; set; }

        [Column("size")]
        [StringLength(255)]
        public string? size { get; set; }

        [Column("color")]
        [StringLength(255)]
        public string? color { get; set; }

        [Column("weight")]
        [StringLength(255)]
        public string? weight { get; set; }

        [Column("model")]
        [StringLength(255)]
        public string? model { get; set; }

        [Column("company")]
        [StringLength(255)]
        public string? company { get; set; }

        [Column("production_date")]
        public DateTime? production_date { get; set; }

        [Column("expiry_date")]
        public DateTime? expiry_date { get; set; }

        [Column("group_name")]
        [StringLength(255)]
        public string? group_name { get; set; }

        [Column("group_num")]
        public int? group_num { get; set; }
    }
}