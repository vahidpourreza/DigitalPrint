using DigitalPrint.Core.ApplicationServices.Product.CommandHandlers;
using DigitalPrint.Core.Domain.Products.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPrint.EndPoint.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddvertismentController : ControllerBase
    {


        [HttpPost]
        public IActionResult Post([FromServices] CreateHandler handler, Create request)
        {
            handler.Handle(request);
            return Ok();
        }

        [Route("title")]
        [HttpPut]
        public IActionResult Put([FromServices] SetTitleHandler handler, SetTitle request)
        {
            handler.Handle(request);
            return Ok();
        }

        [Route("description")]
        [HttpPut]
        public IActionResult Put([FromServices] UpdateDescriptionHandler handler, UpdateDescription request)
        {
            handler.Handle(request);
            return Ok();
        }

        [Route("price")]
        [HttpPut]
        public IActionResult Put([FromServices] UpdatePriceHandler handler, UpdatePrice request)
        {
            handler.Handle(request);
            return Ok();
        }

        [Route("publish")]
        [HttpPut]
        public IActionResult Put([FromServices]  SendToPublishHandler handler, SentForPublish request)
        {
            handler.Handle(request);
            return Ok();
        }
    }
}
