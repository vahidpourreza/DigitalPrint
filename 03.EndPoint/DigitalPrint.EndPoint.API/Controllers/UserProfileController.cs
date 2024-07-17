using DigitalPrint.Core.ApplicationServices.UserProfiles.CommandHandlers;
using DigitalPrint.Core.Domain.UserProfiles.Commands;
using DigitalPrint.EndPoint.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPrint.EndPoint.API.Controllers
{
    [ApiController]
    [Route("/profile")]
    public class UserProfileController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromServices] RegisterUserHandler registerUserHandler, RegisterUser request) => RequestHandler.HandleRequest(request, registerUserHandler.Handle);

        [Route("name")]
        [HttpPut]
        public IActionResult Put([FromServices] UpdateUserNameHandler updateUserNameHandler, UpdateUserName request)=> RequestHandler.HandleRequest(request, updateUserNameHandler.Handle);
      
        [Route("displayname")]
        [HttpPut]
        public IActionResult Put([FromServices] UpdateUserDisplayNameHandler updateUserDisplayNameHandler, UpdateUserDisplayName request) => RequestHandler.HandleRequest(request, updateUserDisplayNameHandler.Handle);

        [Route("email")]
        [HttpPut]
        public IActionResult Put([FromServices] UpdateUserEmailHandler updateUserEmailHandler, UpdateUserEmail request) => RequestHandler.HandleRequest(request, updateUserEmailHandler.Handle);
    }
}
