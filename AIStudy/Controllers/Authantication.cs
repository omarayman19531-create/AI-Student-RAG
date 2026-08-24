using Application.Features.Authantication.Command.ForgetPassowerd;
using Application.Features.Authantication.Command.Login;
using Application.Features.Authantication.Command.Register;
using Application.Features.Authantication.Command.ReviveToken;
using Application.Features.Authantication.Command.SetPasowerd;
using Application.Features.Authantication.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace AIStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authantication(IMediator mediator) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand)
        {
        var result=await mediator.Send(registerCommand);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
        [HttpPost("Login")]
        [EnableRateLimiting("AuthPolicy")]

        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var result = await mediator.Send(loginCommand);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
          [HttpPost("ForgetPassowerd")]
        [EnableRateLimiting("AuthPolicy")]

        public async Task<IActionResult> ForgetPassowerd(ForgetPassowerdCommand forgetPassowerd)
        {
            var result = await mediator.Send(forgetPassowerd);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpGet("checkuser")]
        [EnableRateLimiting("AuthPolicy")]
        public async Task<IActionResult> checkuser([FromQuery] CheckEmailQuery checkEmail )
        {
            var result = await mediator.Send(checkEmail);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("ReviveToken")]
        public async Task<IActionResult> ReviveToken([FromBody] ReviveTokenCommand reviveToken)
        {
            var result = await mediator.Send(reviveToken);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SetPassowerd")]
        public async Task<IActionResult> SetPassowerd([FromBody] SetPassowerdComand setPassowerdComand)
        {
            var result = await mediator.Send(setPassowerdComand);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
