using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_asp.Models;
[Table ("orders")]
public class Order
{
    [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }


    // [ForeignKey("StockID")]
    [Required]
    public int StockID { get; set; }

    public int? Quantity { get; set; }

    [Required]
    public string? BuyerName { get; set; }

    [Range(0.01, 99.99)]
    public decimal Price { get; set; }
}

