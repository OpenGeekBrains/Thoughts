using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Identity
{
    public sealed class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
