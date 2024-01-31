using Hospital.Application.Auth.Queries.HardPasswordChange;
using Hospital.Application.Auth.Queries.Login;
using Hospital.Application.Auth.Queries.Login.Dtos;
using Hospital.Application.Auth.Queries.RefreshToken;
using Hospital.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<BaseResponseModel<LoginDto>>> Login(LoginCommand loginModel)
    {
        BaseResponseModel<LoginDto> loginResponse = await Mediator.Send(loginModel);
        return Ok(loginResponse);
    }

    [HttpGet]
    [Route("refreshtoken")]
    public async Task<ActionResult<BaseResponseModel<LoginDto>>> RefreshToken(string refreshToken)
    {
        return Ok(await Mediator.Send(new RefreshTokenCommand { RefreshToken = refreshToken }));
    }
    
    [HttpPost]
    [Route("hard-password-change")]
    public async Task<IActionResult> HardPasswordChange(HardPasswordChangeCommand request)
    {
        await Mediator.Send(request);
        return Ok();
    }
}