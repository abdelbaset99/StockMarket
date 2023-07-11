using System.ComponentModel.DataAnnotations;

namespace backend_asp.Models
{
    public class BuyRequest
    {
        [Required]
        public string StockName { get; set; } = default!;
        public int Quantity { get; set; } = 1;
        [Required]
        public string BuyerName { get; set; } = default!;
    }
}

