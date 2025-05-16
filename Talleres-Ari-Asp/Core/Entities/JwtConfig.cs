using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Core.Entities
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int ExpiryInMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}