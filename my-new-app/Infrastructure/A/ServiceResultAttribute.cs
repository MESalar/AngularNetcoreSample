using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace my_new_app.Infrastructure
{
    public class ServiceResultAttribute : ActionFilterAttribute
    {

//         public override void OnActionExecuted(ActionExecutedContext context)
//         {
// Models.ResponseModel serviceres
//             //context.Result;
//             base.OnActionExecuted(context);
//         }

//         public void OnAuthorization(AuthorizationFilterContext context)
//         {
//             string IP = context.HttpContext.Connection.RemoteIpAddress.ToString();
//             Guid Token = new Guid(context.HttpContext.Request.Headers["csrf"]);
//             var userId = _userRepo.GetUserByToken(Token);

//             if (userId > 0)
//                 context.Result = new UnauthorizedResult();
//         }

    }
}
