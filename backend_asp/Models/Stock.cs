using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_asp.Models;
[Table("stocks")]
public class Stock
{
    [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    public string? Name { get; set; }

    [Column("ArName")]
    public string? ArName { get; set; }

    [Range(0.01, 99.99)]
    public decimal Price { get; set; }
}

