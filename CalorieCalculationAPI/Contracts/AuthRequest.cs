using System.ComponentModel.DataAnnotations;

namespace CalorieCalculation.API.Contracts
{
    public class AuthRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public bool IsSuccess { get; set; }

        public string Reason { get; set; }
    }

    public class RefreshTokenRequest
    {
        [Required]
        public string ExpiredToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
