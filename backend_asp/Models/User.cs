using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace backend_asp.Models;

[Table ("users")]
public class User : IdentityUser
{
    [Column("userName")]
    [Key]
    [Required]
    public override string? UserName { get; set; }
    
    [Column("email")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public override string? Email { get; set; }

    [Column("password")]
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    // [Column("userName")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
}
