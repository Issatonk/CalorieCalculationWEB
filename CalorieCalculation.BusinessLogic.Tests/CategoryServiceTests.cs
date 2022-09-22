using AutoFixture;
using CalorieCalculation.Core;
using CalorieCalculation.Core.Repositories;
using Moq;

namespace CalorieCalculation.BusinessLogic.Tests
{
    public class CategoryServiceTests
    {
        private readonly CategoryService _categoryService;
        private readonly Mock<ICategoryRepository> _repositoryMock;

        public CategoryServiceTests()
        {
            _repositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_repositoryMock.Object);           
        }

        [Fact]
        public async Task Create_CategoryIsValid_ShouldReturnCategoryAsync()
        {

            Fixture fixture = new Fixture();
            var category = fixture.Build<Category>().Without(x => x.Id).Create();
            var expected = new Category
            {
                Id = 1,
                Name = category.Name
            };
            _repositoryMock.Setup(x => x.Create(category)).Returns(Task.FromResult(expected));
            //act
            var result = await _categoryService.Create(category);
            //assert
            _repositoryMock.Verify(x => x.Create(category), Times.Once);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void Create_UserIsNull_ThrowArgumentNullException()
        {
            //arrange
            Category category = null;

            //act
            _categoryService.Create(category);
            //assert
            _repositoryMock.Verify(x => x.Create(category), Times.Never);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _categoryService.Create(category));
        }

        [Theory]
        [MemberData(nameof(TestCategoryGenerator.GetCategoryForCreate), MemberType = typeof(TestCategoryGenerator))]
        public async void Create_UserIsInvalid_ThrowArgumentException(Category category)
        {
            //arrange

            //act
            _categoryService.Create(category);
            //assert
            _repositoryMock.Verify(x => x.Create(category), Times.Never);
            await Assert.ThrowsAsync<ArgumentException>(async () => await _categoryService.Create(category));
        }
    }
    public static class TestCategoryGenerator
    {
        static Fixture fixture = new Fixture();
        public static IEnumerable<object[]> GetCategoryForCreate()
        {
            yield return new object[]
            {
                fixture.Build<Category>().With(x=>x.Id, 1).Create()
            };
            yield return new object[]
            {
                fixture.Build<Category>().With(x=>x.Id, -1).Create()
            };
            yield return new object[]
            {
                fixture.Build<Category>().With(x=>x.Name, "   ").Create()
            };
            yield return new object[]
            {
                fixture.Build<Category>().Without(x=>x.Name).Create()
            };
        }
    }
}