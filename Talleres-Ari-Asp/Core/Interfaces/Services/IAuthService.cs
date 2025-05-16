using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Core.Entities;
using Core.Interfaces.Services;

namespace Talleres_Ari_Asp.Core.Interfaces
{
    public interface IAuthService {
        string GenerateToken(User user);
    }
}