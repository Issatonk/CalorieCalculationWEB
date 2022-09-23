namespace CalorieCalculation.Core
{
    public class FoodConsumed
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public double Portion { get; set; }
        public DateTime? Datetime { get; set; }


    }
}