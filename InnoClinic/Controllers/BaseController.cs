using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using InnoClinic.Domain.Exceptions;

namespace InnoClinic.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private readonly ILogger _logger;

        protected BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected int GetUserId()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userId, out var id))
            {
                return id;
            }

            _logger.LogError("Couldn't identify the current user from the context.");
            throw new BadRequestException("Couldn't identify the current user.");
        }
    }
}
