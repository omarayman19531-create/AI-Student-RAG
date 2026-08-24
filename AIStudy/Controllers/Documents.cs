using Application.Features.File;
using Application.Features.question;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Documents(IMediator mediator) : ControllerBase
    {
        [HttpPost("FileUpliad")]
        [Authorize]
        public async Task<IActionResult> FileUpliad([FromForm] UploadFileCommand uploadFile)
        {
          var result=  await mediator.Send(uploadFile);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("UserQuestion")]
        [Authorize]
        public async Task<IActionResult> UserQuestion(UserQuestionCommand userQuestion)
        {
            var result = await mediator.Send(userQuestion);
            if (result==null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
