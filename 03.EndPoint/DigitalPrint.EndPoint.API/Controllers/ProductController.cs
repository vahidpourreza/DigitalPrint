using DigitalPrint.Core.ApplicationServices.Product.CommandHandlers;
using DigitalPrint.Core.ApplicationServices.UserProfiles.CommandHandlers;
using DigitalPrint.Core.Domain.Products.Commands;
using DigitalPrint.EndPoint.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPrint.EndPoint.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromServices] CreateHandler createHandler, Create request) => RequestHandler.HandleRequest(request, createHandler.Handle);

        [Route("publish")]
        [HttpPut]
        public IActionResult Put([FromServices]  SendToPublishHandler sendToPublishHandler, SentForPublish request) => RequestHandler.HandleRequest(request, sendToPublishHandler.Handle);
    }
}
