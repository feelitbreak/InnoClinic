﻿using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InnoClinic.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected int? GetUserIdFromContext()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userId, out var id))
            {
                return id;
            }
            else
            {
                return null;
            }
        }
    }
}
