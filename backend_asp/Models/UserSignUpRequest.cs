using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_asp.Models
{
    public class UserSignUpRequest
    {
        [Required]
        public string? UserName { get; set; } = null!;
        [Required]
        public string? Email { get; set; } = null!;
        [Required]
        public string? Password { get; set; } = null!;
    }
}