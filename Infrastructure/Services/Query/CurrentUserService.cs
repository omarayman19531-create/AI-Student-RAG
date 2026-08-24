using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Query
{
    public class CurrentUserService(IHttpContextAccessor httpContext) : ICurrentUserService
    {
        public string? GetUserId()
        {
            var userid = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userid;
        }
    }
}
