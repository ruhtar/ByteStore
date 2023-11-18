using ByteStore.API.Attributes;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Application.Validator;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteStore.API.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserValidator _userValidator;

    public UserController(IUserService userService, IUserValidator userValidator)
    {
        _userService = userService;
        _userValidator = userValidator;
    }

    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<IActionResult> Signup([FromBody] SignupUserDto user)
    {
        var userValidatorStatus = await _userValidator.ValidateUser(user);
        switch (userValidatorStatus)
        {
            case UserValidatorStatus.Success:
                break;
            case UserValidatorStatus.UsernameAlreadyExists:
                return BadRequest("User already exists. Please, try other username.");
            case UserValidatorStatus.InvalidPassword:
                return BadRequest("Your password must have capital letters, numbers and special characters");
            case UserValidatorStatus.InvalidRole:
                return BadRequest("User must be have Seller or User role");
        }

        await _userService.RegisterUser(user);

        return Ok("User registered.");
    }

    [HttpPost("signin")]
    public async Task<ActionResult<string>> Signin([FromBody] LoginDto user)
    {
        var token = await _userService.AuthenticateUser(
            new User
            {
                Username = user.Username,
                Password = user.Password
            });
        if (string.IsNullOrEmpty(token)) return Unauthorized("Username or password incorrect.");
        return Ok(new { token });
    }

    [Authorize]
    [TokenValidation]
    [HttpPut("{userId}/address")]
    public async Task<ActionResult> EditUserAddress([FromRoute] int userId, [FromBody] Address address)
    {
        await _userService.EditUserAddress(address, userId);
        return Ok();
    }

    [HttpGet("{userId}/address")]
    [Authorize]
    [TokenValidation]
    public async Task<ActionResult> GetUserAddress([FromRoute] int userId)
    {
        var address = await _userService.GetUserAddress(userId);
        if (address != null) return Ok(address);
        return NotFound();
    }


    [HttpPut("change-password")]
    [Authorize]
    [TokenValidation]
    public async Task<IActionResult?> ChangePassword(int userId, [FromBody] ChangePasswordRequestDto passwordDto)
    {
        if (passwordDto.Password != passwordDto.Repassword) return BadRequest("Please, insert matching passwords");
        var response = await _userService.ChangePassword(userId, passwordDto.Password, passwordDto.Repassword);
        switch (response)
        {
            case ChangePasswordStatusResponse.Sucess:
                return Ok();
            case ChangePasswordStatusResponse.NotMatching:
                return BadRequest("'Passwords do not match.");
            case ChangePasswordStatusResponse.InvalidPassword:
                return BadRequest("Invalid password.");
            case ChangePasswordStatusResponse.UserNotFound:
                return Problem("User not found.");
            default: return Problem();
        }
    }

    [Authorize]
    [TokenValidation]
    [HttpGet("purchase-history")]
    public async Task<IActionResult> GetUserPurchaseHistory(int userId)
    {
        var userPurchaseHistory = await _userService.GetUserPurchaseHistory(userId);
        if (userPurchaseHistory == null) return NotFound();
        return Ok(userPurchaseHistory);
    }
    
    //TODO: PORQUE VC FEZ ESSE ENDPOINT?????????????
    [Authorize]
    [TokenValidation]
    [HttpGet("purchase-history/check")]
    public async Task<IActionResult> CheckIfUserHasBoughtAProduct(int userId, int productId)
    {
        var hasBought = await _userService.CheckIfUserHasBoughtAProduct(userId, productId);
        if (hasBought) return Ok();
        return Unauthorized();
    }

}