using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController, Route("/api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected Guid UserId => User.Identity.IsAuthenticated
            ? Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty)
            : Guid.Empty;
    }
}