using AutoMapper;
using CalorieCalculation.API.Auth;
using CalorieCalculation.API.Contracts;
using CalorieCalculation.Core;
using CalorieCalculation.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponse
                {
                    IsSuccess = false,
                    Reason = "UserName and Password must be provided."
                });
            }
            var authResponse = await _jwtService.GetTokenAsync(request, HttpContext.Connection.RemoteIpAddress.ToString());
            if (authResponse == null)
                return Unauthorized();
            return Ok(authResponse);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResponse
                {
                    IsSuccess = false,
                    Reason = "Tokens must be provided"
                });

            var token = _jwtService.GetJwtToken(request.ExpiredToken);
            var userRefreshToken = await _jwtService.GetUserRefreshTokenAsync(request, HttpContext.Connection.RemoteIpAddress.ToString());

            AuthResponse response = ValidateDetails(token, userRefreshToken);
            if(!response.IsSuccess)
                return BadRequest(response);

            userRefreshToken.IsInvalidated = true;
            _jwtService.UpdateRefreshToken(userRefreshToken);


            var userName = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId).Value;

            var authResponse = _jwtService.GetRefreshTokenAsync(
                HttpContext.Connection.RemoteIpAddress.ToString(),
                userRefreshToken.UserId,
                userName);

            return Ok(authResponse);
        }

        private AuthResponse ValidateDetails(JwtSecurityToken token, CalorieCalculation.DataAccess.Sqlite.Entities.UserRefreshToken userRefreshToken)
        {
            if (userRefreshToken == null)
                return new AuthResponse { IsSuccess = false, Reason = "Invalid Token Details" };
            if (token.ValidTo > DateTime.UtcNow)
                return new AuthResponse { IsSuccess = false, Reason = "Token not expired" };
            if (!userRefreshToken.IsActive)
            {
                return new AuthResponse { IsSuccess = false, Reason = "Refresh token Expired" };
            }
            return new AuthResponse { IsSuccess = true };
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