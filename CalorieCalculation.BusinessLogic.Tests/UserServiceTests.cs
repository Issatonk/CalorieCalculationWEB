using AutoFixture;
using CalorieCalculation.Core;
using CalorieCalculation.Core.Repositories;
using Moq;

namespace CalorieCalculation.BusinessLogic.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _service;
        private readonly Mock<IUserRepository> _repositoryMock;

        public UserServiceTests()
        {

            _repositoryMock = new Mock<IUserRepository>();
            _service = new UserService(_repositoryMock.Object);
        }

        [Fact]
        public async void Create_UserIsValid_ShouldReturnNewUserAsync()
        {
            //arrange
            Fixture fixture = new Fixture();
            var user = fixture.Build<User>().Without(x => x.Id).Create();

            //act
            var result = await _service.Create(user);
            //assert
            _repositoryMock.Verify(x=>x.Create(user), Times.Once);
        }
       
        [Fact]
        public async void Create_UserIsNull_ThrowArgumentNullException()
        {
            //arrange
            User user = null;

            //act
            _service.Create(user);
            //assert
            _repositoryMock.Verify(x => x.Create(user), Times.Never);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.Create(user));
        }

        [Theory]
        [MemberData(nameof(TestUserGenerator.GetUsersForCreate), MemberType = typeof(TestUserGenerator))]
        [MemberData(nameof(TestUserGenerator.GetTestUsers), MemberType = typeof(TestUserGenerator))]
        public async void Create_UserIsInvalid_ThrowArgumentException(User testUser)
        {
            //arrange

            //act
             _service.Create(testUser);
            //assert
            _repositoryMock.Verify(x => x.Create(testUser), Times.Never);
            await Assert.ThrowsAsync<ArgumentException>(async () => await _service.Create(testUser));
        }

        [Fact]
        public async void GetById_IdIsValid_ShouldReturnProduct()
        {
            //arrange
            Fixture fixture = new Fixture();
            var id = 1;
            var user = fixture.Create<User>();
            _repositoryMock.Setup(x => x.GetById(id)).ReturnsAsync(() => user);
            //act
            var result = await _service.GetById(id);
            //assert
            _repositoryMock.Verify(x => x.GetById(id), Times.Once());
            Assert.Equal(user, result);

        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async void GetById_IdIsNotValid_ThrowArgumentException(int id)
        {
            //arrange

            //act
            var result = _service.GetById(id);
            //assert
            _repositoryMock.Verify(x => x.GetById(id), Times.Never());
            await Assert.ThrowsAsync<ArgumentException>(async () => await _service.GetById(id));
        }

        [Fact]
        public async Task UpdateUser_IsValid_ShouldUpdateProductAsync()
        {
            //arrange
            var fixture = new Fixture();
            var user = fixture.Build<User>().Create();

            //act
            var newProduct = await _service.Update(user);
            //assert
            _repositoryMock.Verify(x => x.Update(user), Times.Once());
        }
        [Fact]
        public async void Update_ProductIsNull_ThrowArgumentNullException()
        {
            //arrange
            User user = null;
            //act
            var result = _service.Update(user);
            //assert
            _repositoryMock.Verify(x => x.Update(user), Times.Never());
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.Update(user));
        }

        [Theory]
        [MemberData(nameof(TestUserGenerator.GetUsersForUpdate), MemberType = typeof(TestUserGenerator))]
        [MemberData(nameof(TestUserGenerator.GetTestUsers), MemberType = typeof(TestUserGenerator))]
        public async void Update_ProductIsNotValid_ThrowArgumentInvalidException(User user)
        {
            //arrange
            //act
            var result = _service.Update(user);
            //assert
            _repositoryMock.Verify(x => x.Update(user), Times.Never());
            await Assert.ThrowsAsync<ArgumentException>(async () => await _service.Update(user));
        }
        [Fact]
        public async void Delete_IdIsValid_ShouldReturnTrueAsync()
        {
            //arrange
            var expected = true;
            var id = 1;
            _repositoryMock.Setup(x => x.Delete(id)).Returns(Task.FromResult(true));
            //act
            var result = await _service.Delete(id);
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
            var result = _service.Delete(id);
            //assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _service.Delete(id));
            _repositoryMock.Verify(x => x.Delete(id), Times.Never());
        }

    }

    public class TestUserGenerator
    {
        private static Fixture fixture = new Fixture();

        public static IEnumerable<object[]> GetUsersForCreate()
        {
            yield return new object[] 
            { 
                fixture.Build<User>().With(x => x.Id, "test").Create() 
            };
        }
        public static IEnumerable<object> GetUsersForUpdate()
        {
            yield return new object[] 
            { 
                fixture.Build<User>().Without(x => x.Id).Create() 
            };
            yield return new object[] 
            { 
                fixture.Build<User>().With(x => x.Id, " ").Create() 
            };
        }

        public static IEnumerable<object[]> GetTestUsers()
        {
            yield return new object[]
            {
                fixture.Build<User>().Without(x=>x.FirstName).Create()
            };
            yield return new object[]
            {
                fixture.Build<User>().Without(x=>x.LastName).Create()
            };
            yield return new object[]
            {
                fixture.Build<User>().Without(x=>x.Email).Create()
            };
            yield return new object[]
            {
                fixture.Build<User>().Without(x=>x.PasswordHash).Create()
            };
        }

    }
}