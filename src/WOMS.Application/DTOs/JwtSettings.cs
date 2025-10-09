using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOMS.Application.DTOs
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string EXPIRATION_MINUTES { get; set; } = string.Empty;
        public string RefreshToken_EXPIRATION_MINUTES { get; set; } = string.Empty;
    }
}
