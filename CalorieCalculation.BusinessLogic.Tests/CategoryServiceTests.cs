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

        [Fact]
        public async void GetById_IdIsValid_ShouldReturnProduct()
        {
            //arrange
            Fixture fixture = new Fixture();
            var id = 1;
            var category = fixture.Create<Category>();
            _repositoryMock.Setup(x => x.GetById(id)).ReturnsAsync(() => category);
            //act
            var result = await _categoryService.GetById(id);
            //assert
            _repositoryMock.Verify(x => x.GetById(id), Times.Once());
            Assert.Equal(category, result);

        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void GetById_IdIsNotValid_ThrowArgumentException(int id)
        {
            //arrange

            //act
            var result = _categoryService.GetById(id);
            //assert
            _repositoryMock.Verify(x => x.GetById(id), Times.Never());
            await Assert.ThrowsAsync<ArgumentException>(async () => await _categoryService.GetById(id));
        }

        [Fact]
        public async Task UpdateUser_IsValid_ShouldUpdateProductAsync()
        {
            //arrange
            var fixture = new Fixture();
            var category = fixture.Build<Category>().Create();
            _repositoryMock.Setup(x => x.Update(category)).Returns(Task.FromResult(category));
            //act
            var result = await _categoryService.Update(category);
            //assert
            _repositoryMock.Verify(x => x.Update(category), Times.Once());
        }
        [Fact]
        public async void Update_ProductIsNull_ThrowArgumentNullException()
        {
            //arrange
            Category category = null;
            //act
            var result = _categoryService.Update(category);
            //assert
            _repositoryMock.Verify(x => x.Update(category), Times.Never());
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _categoryService.Update(category));
        }

        [Theory]
        [MemberData(nameof(TestCategoryGenerator.GetCategoryForUpdate), MemberType = typeof(TestCategoryGenerator))]
        public async void Update_ProductIsNotValid_ThrowArgumentInvalidException(Category category)
        {
            //arrange
            //act
            var result = _categoryService.Update(category);
            //assert
            _repositoryMock.Verify(x => x.Update(category), Times.Never());
            await Assert.ThrowsAsync<ArgumentException>(async () => await _categoryService.Update(category));
        }
        [Fact]
        public async void Delete_IdIsValid_ShouldReturnTrueAsync()
        {
            //arrange
            var expected = true;
            var id = 1;
            _repositoryMock.Setup(x => x.Delete(id)).Returns(Task.FromResult(true));
            //act
            var result = await _categoryService.Delete(id);
            //assert
            Assert.Equal(expected, result);
            _repositoryMock.Verify(x => x.Delete(id), Times.Once());
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void Delete_IdIsInvalid_ThrowArgumentException(int id)
        {
            //arrange

            //act
            var result = _categoryService.Delete(id);
            //assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _categoryService.Delete(id));
            _repositoryMock.Verify(x => x.Delete(id), Times.Never());
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
        public static IEnumerable<object[]> GetCategoryForUpdate()
        {
            yield return new object[]
            {
                fixture.Build<Category>().With(x=>x.Id, 0).Create()
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