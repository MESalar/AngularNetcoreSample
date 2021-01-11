using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace my_new_app.Infrastructure
{
    public class AccessControlAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        private bool HasPermission;
        private Repositories.UserRepository _userRepo;

        public AccessControlAttribute(Repositories.UserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string IP = context.HttpContext.Connection.RemoteIpAddress.ToString();
            if (!context.HttpContext.Request.Headers.ContainsKey("csrf"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            Guid token = new Guid(context.HttpContext.Request.Headers["csrf"]);
            if (!_userRepo.TokenIsValid(token))
                context.Result = new UnauthorizedResult();
        }

    }
}
