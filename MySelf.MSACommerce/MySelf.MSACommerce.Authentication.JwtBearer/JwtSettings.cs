using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.Authentication.JwtBearer
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Secret { get; set; } = null!;
        public int AccessTokenExpirationMinutes { get; set; }   


    }
}
