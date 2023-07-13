using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace backend_asp.Models;

[Table ("users")]
public class User
{
    [Column("userName")]
    [Key]
    [Required]
    public string? UserName { get; set; }
    
    [Column("email")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string? Email { get; set; }

    [Column("password")]
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
