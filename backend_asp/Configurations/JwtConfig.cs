using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace backend_asp.Configurations
{
    public class JwtConfig
    {
        public string Secret { get; set; } = null!;

        public JwtConfig()
        {
        }
        // public JwtConfig(IConfiguration configuration)
        // {
        //     Secret = configuration.GetValue<string>("JwtConfig:Secret");
        // }
    }
}