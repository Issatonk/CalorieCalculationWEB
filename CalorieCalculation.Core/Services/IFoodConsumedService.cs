namespace CalorieCalculation.Core.Services
{
    public interface IFoodConsumedService 
    {
        Task<FoodConsumed> CreateFoodConsumed(FoodConsumed food);

        Task<IEnumerable<FoodConsumed>> CreateManyFoodConsumed(IEnumerable<FoodConsumed> foods);

        Task<FoodConsumed> GetFoodConsumed(int id);
        Task<IEnumerable<FoodConsumed>> GetAllFoodConsumed();
        Task<FoodConsumed> UpdateFoodConsumed(FoodConsumed food);
        Task<bool> DeleteFoodConsumed(int id);
    }
}
