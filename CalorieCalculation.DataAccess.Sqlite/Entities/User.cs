using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalorieCalculation.DataAccess.Sqlite.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public List<UserRefreshToken> UserRefreshTokens { get; set; }
    }
    [Table("UserRefreshToken")]
    public class UserRefreshToken
    {
        [Key]
        public int UserRefreshTokenId { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        [NotMapped]
        public bool IsActive 
        { 
            get
            {
                return ExpirationDate > DateTime.UtcNow;
            }
        }
        public string IpAddress { get; set; }

        public bool IsInvalidated { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}