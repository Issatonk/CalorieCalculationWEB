using AutoMapper;
using CalorieCalculation.API.Auth;
using CalorieCalculation.API.Contracts;
using CalorieCalculation.Core;
using CalorieCalculation.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalorieCalculationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IMapper mapper, IJwtService jwtService)
        {
            _userService = userService;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AuthToken([FromBody]AuthRequest request)
        {
            var token = await _jwtService.GetTokenAsync(request);
            if (token == null)
                return Unauthorized();
            return Ok(new AuthResponse { Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateRequest user)
        {
            var request =_mapper.Map<User>(user);
            var result = await _userService.Create(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _userService.GetById(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(User category)
        {
            var result = await _userService.Update(category);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            return Ok(result);
        }
    }
}