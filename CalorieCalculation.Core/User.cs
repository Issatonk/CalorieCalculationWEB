using Microsoft.AspNetCore.Identity;

namespace CalorieCalculation.Core
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}