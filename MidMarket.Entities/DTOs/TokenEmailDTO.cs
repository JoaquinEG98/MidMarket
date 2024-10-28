using System;

namespace MidMarket.Entities.DTOs
{
    public class TokenEmailDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
