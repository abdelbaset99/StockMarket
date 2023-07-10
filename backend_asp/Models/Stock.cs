using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_asp.Models;
public class Stock
{
    [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Range(0.01, 99.99)]
    public decimal Price { get; set; }
}

