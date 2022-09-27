using System.ComponentModel.DataAnnotations;

namespace CalorieCalculation.API.Contracts
{
    public class UserCreateRequest
    {
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
