using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Talleres_Ari_Asp.Core.DTO
{
    public class LoginRequest
    {
          public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}