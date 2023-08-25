using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InnoClinic.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async Task<User?> GetUserFromContextAsync(CancellationToken cancellationToken)
        {
            var idClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim is null)
            {
                return null;
            }

            var userId = int.Parse(idClaim.Value);
            var user = await _unitOfWork.Users.GetAsync(userId, cancellationToken);

            return user;
        }
    }
}
