using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Talleres_Ari_Asp.Web.Models
{
    public class LoginRequest
    {
         public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}