using CalorieCalculation.Core;
using CalorieCalculation.Core.Repositories;
using CalorieCalculation.Core.Services;

namespace CalorieCalculation.BusinessLogic
{
    public class FoodConsumedService : IFoodConsumedService
    {
        private readonly IFoodConsumedRepository _foodConsumedRepository;

        public FoodConsumedService(IFoodConsumedRepository foodConsumedRepository)
        {
            _foodConsumedRepository = foodConsumedRepository;
        }
        public async Task<FoodConsumed> CreateFoodConsumed(FoodConsumed food)
        {
            if(food == null)
            {
                throw new ArgumentNullException(nameof(food));
            }
            if (food.Id != 0
                || food.Product == null
                || food.User == null
                || food.Portion <=0
                || food.Datetime == null)
            {
                throw new ArgumentException(nameof(food));
            }
            return await _foodConsumedRepository.CreateFoodConsumed(food);
        }

        public async Task<IEnumerable<FoodConsumed>> CreateManyFoodConsumed(IEnumerable<FoodConsumed> foods)
        {
            if(foods == null)
            {
                throw new ArgumentNullException(nameof(foods));
            }
            foreach (var food in foods)
            {
                if (food.Id != 0
                || food.Product == null
                || food.User == null
                || food.Portion <= 0
                || food.Datetime == null)
                {
                    throw new ArgumentException(nameof(food));
                }
            }
            return await _foodConsumedRepository.CreateManyFoodConsumed(foods);
        }

        public async Task<FoodConsumed> GetFoodConsumed(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException();
            }
            return await _foodConsumedRepository.GetFoodConsumed(id);
        }

        public async Task<IEnumerable<FoodConsumed>> GetAllFoodConsumed()
        {
            return await _foodConsumedRepository.GetAllFoodConsumed();
        }
        public async Task<FoodConsumed> UpdateFoodConsumed(FoodConsumed food)
        {
            if (food == null)
            {
                throw new ArgumentNullException(nameof(food));
            }
            if (food.Id <= 0
                || food.Product == null
                || food.User == null
                || food.Portion <= 0
                || food.Datetime == null)
            {
                throw new ArgumentException(nameof(food));
            }
            return await _foodConsumedRepository.UpdateFoodConsumed(food);
        }

        public async Task<bool> DeleteFoodConsumed(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException();
            }
            return await _foodConsumedRepository.DeleteFoodConsumed(id);
        }


    }
}