using AutoFixture;
using CalorieCalculation.Core;
using CalorieCalculation.Core.Repositories;
using Moq;

namespace CalorieCalculation.BusinessLogic.Tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _service;
        private readonly Mock<IProductRepository> _repositoryMock;


        public ProductServiceTests(ProductService service = null)
        {
            _repositoryMock = new Mock<IProductRepository>();
            _service = new ProductService(_repositoryMock.Object);
        }

        [Fact]
        public async void Create_ProductIsValid_ShouldCreateNewProduct()
        {
            //arrange
            var fixture = new Fixture();
            var product = fixture.Build<Product>().With(x=>x.Id,0).Create();

            //act
            var newProduct = await _service.CreateProduct(product);
            //assert
            _repositoryMock.Verify(x=>x.CreateProduct(product), Times.Once());
        }
        [Fact]
        public async void Create_ProductIsNull_ThrowArgumentNullException()
        {
            //arrange
            Product product = null;
            //act
            var result = _service.CreateProduct(product);
            //assert
            _repositoryMock.Verify(x => x.CreateProduct(product), Times.Never());
            await Assert.ThrowsAsync<ArgumentNullException>( async() => await _service.CreateProduct(product));
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetTestProductForCreateOnly), MemberType = typeof(TestDataGenerator))]
        [MemberData(nameof(TestDataGenerator.GetTestProducts), MemberType = typeof(TestDataGenerator))]
        public async void Create_ProductIsNotValid_ThrowArgumentInvalidException(Product test)
        {
            //arrange
            Product product = test;
            //act
            var result =  _service.CreateProduct(product);
            //assert
            _repositoryMock.Verify(x => x.CreateProduct(product), Times.Never());
            await Assert.ThrowsAsync<ArgumentException>(async () => await _service.CreateProduct(product));
        }

        [Fact]
        public async void GetProduct_IdIsValid_ShouldReturnProduct()
        {
            //arrange
            Fixture fixture = new Fixture();
            var id = 1;
            var product = fixture.Create<Product>();
            _repositoryMock.Setup(x => x.GetProduct(id)).ReturnsAsync(() => product);
            //act
            var result = await _service.GetProduct(id);
            //assert
            _repositoryMock.Verify(x => x.GetProduct(id), Times.Once());
            Assert.Equal(product, result);

        }
        [Fact]
        public async void GetProduct_IdIsNotValid_ThrowArgumentException()
        {
            //arrange
            var id = 0;
            //act
            var result = _service.GetProduct(id);
            //assert
            _repositoryMock.Verify(x => x.GetProduct(id), Times.Never());
            await Assert.ThrowsAsync<ArgumentException>(async () => await _service.GetProduct(id));
        }

        [Fact]
        public void GetAllProducts_ShouldReturnManyProducts()
        {
            //arrange
            Fixture fixture = new Fixture();
            var list = fixture.CreateMany<Product>().ToList();
            _repositoryMock.Setup(x => x.GetAllProducts()).Returns(Task.FromResult(list  as IEnumerable<Product>));
            //act
            var result = _service.GetAllProducts().Result.ToList<Product>();
            //assert
            _repositoryMock.Verify(x => x.GetAllProducts(), Times.Once());
            Assert.Equal(list, result);
        }

        [Fact]
        public async Task UpdateProducts_IsValid_ShouldUpdateProductAsync()
        {
            //arrange
            var fixture = new Fixture();
            var product = fixture.Build<Product>().Create();

            //act
            var newProduct = await _service.UpdateProduct(product);
            //assert
            _repositoryMock.Verify(x => x.UpdateProduct(product), Times.Once());
        }
        [Fact]
        public async void Update_ProductIsNull_ThrowArgumentNullException()
        {
            //arrange
            Product product = null;
            //act
            var result = _service.UpdateProduct(product);
            //assert
            _repositoryMock.Verify(x => x.UpdateProduct(product), Times.Never());
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.UpdateProduct(product));
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetTestProductForUpdateOnly), MemberType = typeof(TestDataGenerator))]
        [MemberData(nameof(TestDataGenerator.GetTestProducts), MemberType = typeof(TestDataGenerator))]
        public async void Update_ProductIsNotValid_ThrowArgumentInvalidException(Product test)
        {
            //arrange
            Product product = test;
            //act
            var result = _service.UpdateProduct(product);
            //assert
            _repositoryMock.Verify(x => x.UpdateProduct(product), Times.Never());
            await Assert.ThrowsAsync<ArgumentException>(async () => await _service.UpdateProduct(product));
        }
        [Fact]
        public async void Delete_IdIsValid_ShouldReturnTrueAsync()
        {
            //arrange
            var expected = true;
            var id = 1;
            _repositoryMock.Setup(x => x.DeleteProduct(id)).Returns(Task.FromResult(true));
            //act
            var result = await _service.DeleteProduct(id);
            //assert
            Assert.Equal(expected, result);
            _repositoryMock.Verify(x=>x.DeleteProduct(id),Times.Once());
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void Delete_IdIsInvalid_ThrowArgumentException(int id)
        {
            //arrange

            //act
            var result =  _service.DeleteProduct(id);
            //assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _service.DeleteProduct(id));
            _repositoryMock.Verify(x => x.DeleteProduct(id), Times.Never());
        }
    }

    public class TestDataGenerator 
    {
        static Fixture fixture = new Fixture();
        
        public static IEnumerable<object[]> GetTestProductForCreateOnly()
        {
            yield return new object[]
            {
                fixture.Build<Product>().With(x=>x.Id, 1).Create()
            };
        }
        public static IEnumerable<object[]> GetTestProductForUpdateOnly()
        {
            yield return new object[]
            {
                fixture.Build<Product>().With(x=>x.Id, 0).Create()
            };
            yield return new object[]
            {
                fixture.Build<Product>().With(x=>x.Id, -1).Create()
            };
        }
        public static IEnumerable<object[]> GetTestProducts()
        {
            
            yield return new object[]
            {
                fixture.Build<Product>().Without(x=>x.Name).Create()
            };
            yield return new object[]
            {
                fixture.Build<Product>().Without(x=>x.Category).Create()
            };
            yield return new object[]
            {
                fixture.Build<Product>().With(x=>x.Calories,-1).Create()
            };
            yield return new object[]
            {
                fixture.Build<Product>().With(x=>x.Proteins,-1).Create()
            };
            yield return new object[]
            {
                fixture.Build<Product>().With(x=>x.Fats,-1).Create()
            };
            yield return new object[]
            {
                fixture.Build<Product>().With(x=>x.Carbohydrates,-1).Create()
            };
            yield return new object[]
            {
                fixture.Build<Product>().Without(x=>x.Picture).Create()
            };

        }
    }
}