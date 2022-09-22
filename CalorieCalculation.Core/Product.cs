namespace CalorieCalculation.Core
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public Picture Picture { get; set; }
    }
}