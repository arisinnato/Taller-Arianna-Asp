using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class UpdateRequest
    {
        public string UserName { get; set; } = "";
        public string NewPassword { get; set; } = "";
    }
}
