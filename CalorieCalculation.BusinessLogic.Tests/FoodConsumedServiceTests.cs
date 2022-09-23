using AutoFixture;
using CalorieCalculation.Core;
using CalorieCalculation.Core.Repositories;
using Moq;

namespace CalorieCalculation.BusinessLogic.Tests
{
    public class FoodConsumedServiceTests 
    {
        private readonly FoodConsumedService _foodConsumedService;
        private readonly Mock<IFoodConsumedRepository> _repositoryMock;
        public FoodConsumedServiceTests()
        {
            _repositoryMock = new Mock<IFoodConsumedRepository>();
            _foodConsumedService = new FoodConsumedService(_repositoryMock.Object);
        }
        [Fact]
        public async void Create_FoodConsumedIsValid_ShouldReturnNewFoodConsumed()
        {
            //arrange
            var fixture = new Fixture();
            var food = fixture.Build<FoodConsumed>().With(x => x.Id, 0).Create();

            //act
            var newProduct = await _foodConsumedService.CreateFoodConsumed(food);
            //assert
            _repositoryMock.Verify(x => x.CreateFoodConsumed(food), Times.Once());
        }
        [Fact]
        public async void Create_FoodConsumedIsNull_ThrowArgumentNullException()
        {
            //arrange
            FoodConsumed food = null;
            //act
            var result = _foodConsumedService.CreateFoodConsumed(food);
            //assert
            _repositoryMock.Verify(x => x.CreateFoodConsumed(food), Times.Never());
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _foodConsumedService.CreateFoodConsumed(food));
        }

        [Theory]
        [MemberData(nameof(TestFoodConsumedGenerator.GetTestFoodConsumedForCreateOnly), MemberType = typeof(TestFoodConsumedGenerator))]
        [MemberData(nameof(TestFoodConsumedGenerator.GetTestFoodConsumed), MemberType = typeof(TestFoodConsumedGenerator))]
        public async void Create_FoodConsumedIsNotValid_ThrowArgumentInvalidException(FoodConsumed food)
        {
            //arrange
            //act
            var result = _foodConsumedService.CreateFoodConsumed(food);
            //assert
            _repositoryMock.Verify(x => x.CreateFoodConsumed(food), Times.Never());
            await Assert.ThrowsAsync<ArgumentException>(async () => await _foodConsumedService.CreateFoodConsumed(food));
        }

        [Fact]
        public async void CreateManyFoodConsumed_AllFoodsIsValid_ReturnNewFoodsConsumed()
        {
            //arrange
            Fixture fixture = new Fixture();
            var consumed = fixture.Build<FoodConsumed>().Without(x => x.Id).CreateMany(20);
            _repositoryMock.Setup(x=>x.CreateManyFoodConsumed(consumed)).ReturnsAsync(consumed);
            //act
            var newConsumed = await _foodConsumedService.CreateManyFoodConsumed(consumed);
            //assert
            Assert.Equal(consumed, newConsumed);
            _repositoryMock.Verify(x=>x.CreateManyFoodConsumed(consumed), Times.Once());
        }

        [Fact]
        public async void CreateManyFoodConsumed_CollectionIsNull_ThrowArgumentNullException()
        {
            //arrange
            List<FoodConsumed> consumed = null;
            //act
            _foodConsumedService.CreateManyFoodConsumed(consumed);
            //assert
            _repositoryMock.Verify(x => x.CreateManyFoodConsumed(consumed), Times.Never());
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _foodConsumedService.CreateManyFoodConsumed(consumed));
        }


        [Theory]
        [MemberData(nameof(TestFoodConsumedGenerator.GetTestFoodConsumedForCreateOnly), MemberType = typeof(TestFoodConsumedGenerator))]
        [MemberData(nameof(TestFoodConsumedGenerator.GetTestFoodConsumed), MemberType = typeof(TestFoodConsumedGenerator))]
        public async void CreateManyFoodConsumed_ElementCollectionIsInvalid_ThrowArgumentException(FoodConsumed food)
        {
            //arrange
            var listFoods = new List<FoodConsumed>() { food };
            //act
            var result = _foodConsumedService.CreateManyFoodConsumed(listFoods);
            //assert
            _repositoryMock.Verify(x => x.CreateManyFoodConsumed(listFoods), Times.Never());
            await Assert.ThrowsAsync<ArgumentException>(async () => await _foodConsumedService.CreateManyFoodConsumed(listFoods));
        }

        [Fact]
        public async void GetFoodConsumed_IdIsValid_ShouldReturnProduct()
        {
            //arrange
            Fixture fixture = new Fixture();
            var id = 1;
            var food = fixture.Create<FoodConsumed>();
            _repositoryMock.Setup(x => x.GetFoodConsumed(id)).ReturnsAsync(() => food);
            //act
            var result = await _foodConsumedService.GetFoodConsumed(id);
            //assert
            _repositoryMock.Verify(x => x.GetFoodConsumed(id), Times.Once());
            Assert.Equal(food, result);

        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void GetFoodConsumed_IdIsNotValid_ThrowArgumentException(int id)
        {
            //arrange
            //act
            var result = _foodConsumedService.GetFoodConsumed(id);
            //assert
            _repositoryMock.Verify(x => x.GetFoodConsumed(id), Times.Never());
            await Assert.ThrowsAsync<ArgumentException>(async () => await _foodConsumedService.GetFoodConsumed(id));
        }

        [Fact]
        public void GetAllFoodConsumed_ShouldReturnManyProducts()
        {
            //arrange
            Fixture fixture = new Fixture();
            var list = fixture.CreateMany<FoodConsumed>().ToList();
            _repositoryMock.Setup(x => x.GetAllFoodConsumed()).ReturnsAsync(list as IEnumerable<FoodConsumed>);
            //act
            var result = _foodConsumedService.GetAllFoodConsumed().Result.ToList<FoodConsumed>();
            //assert
            _repositoryMock.Verify(x => x.GetAllFoodConsumed(), Times.Once());
            Assert.Equal(list, result);
        }

        [Fact]
        public async Task UpdateFoodConsumed_IsValid_ShouldUpdateProductAsync()
        {
            //arrange
            var fixture = new Fixture();
            var food = fixture.Build<FoodConsumed>().Create();

            //act
            var newProduct = await _foodConsumedService.UpdateFoodConsumed(food);
            //assert
            _repositoryMock.Verify(x => x.UpdateFoodConsumed(food), Times.Once());
        }
        [Fact]
        public async void UpdateFoodConsumed_FoodConsumedIsNull_ThrowArgumentNullException()
        {
            //arrange
            FoodConsumed food = null;
            //act
            var result = _foodConsumedService.UpdateFoodConsumed(food);
            //assert
            _repositoryMock.Verify(x => x.UpdateFoodConsumed(food), Times.Never());
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _foodConsumedService.UpdateFoodConsumed(food));
        }

        [Theory]
        [MemberData(nameof(TestFoodConsumedGenerator.GetTestFoodConsumedForUpdateOnly), MemberType = typeof(TestFoodConsumedGenerator))]
        [MemberData(nameof(TestFoodConsumedGenerator.GetTestFoodConsumed), MemberType = typeof(TestFoodConsumedGenerator))]
        public async void UpdateFoodConsumed_FoodConsumedIsNotValid_ThrowArgumentInvalidException(FoodConsumed food)
        {
            //arrange
            //act
            var result = _foodConsumedService.UpdateFoodConsumed(food);
            //assert
            _repositoryMock.Verify(x => x.UpdateFoodConsumed(food), Times.Never());
            await Assert.ThrowsAsync<ArgumentException>(async () => await _foodConsumedService.UpdateFoodConsumed(food));
        }
        [Fact]
        public async void DeleteFoodConsumed_IdIsValid_ShouldReturnTrueAsync()
        {
            //arrange
            var expected = true;
            var id = 1;
            _repositoryMock.Setup(x => x.DeleteFoodConsumed(id)).ReturnsAsync(true);
            //act
            var result = await _foodConsumedService.DeleteFoodConsumed(id);
            //assert
            Assert.Equal(expected, result);
            _repositoryMock.Verify(x => x.DeleteFoodConsumed(id), Times.Once());
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void DeleteFoodConsumed_IdIsInvalid_ThrowArgumentException(int id)
        {
            //arrange

            //act
            var result = _foodConsumedService.DeleteFoodConsumed(id);
            //assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _foodConsumedService.DeleteFoodConsumed(id));
            _repositoryMock.Verify(x => x.DeleteFoodConsumed(id), Times.Never());
        }
    }
    public class TestFoodConsumedGenerator
    {
        static Fixture fixture = new Fixture();

        public static IEnumerable<object[]> GetTestFoodConsumedForCreateOnly()
        {
            yield return new object[]
            {
                fixture.Build<FoodConsumed>().With(x=>x.Id, 1).Create()
            };
        }
        public static IEnumerable<object[]> GetTestFoodConsumedForUpdateOnly()
        {
            yield return new object[]
            {
                fixture.Build<FoodConsumed>().With(x=>x.Id, 0).Create()
            };
            yield return new object[]
            {
                fixture.Build<FoodConsumed>().With(x=>x.Id, -1).Create()
            };
        }
        public static IEnumerable<object[]> GetTestFoodConsumed()
        {
            yield return new object[]
            {
                fixture.Build<FoodConsumed>().Without(x=>x.User).Create()
            };
            yield return new object[]
            {
                fixture.Build<FoodConsumed>().Without(x=>x.Product).Create()
            };
            yield return new object[]
            {
                fixture.Build<FoodConsumed>().Without(x=>x.Portion).Create()
            };
            yield return new object[]
            {
                fixture.Build<FoodConsumed>().Without(x=>x.Datetime).Create()
            };
        }
    }
}