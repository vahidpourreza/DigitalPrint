using DigitalPrint.Core.Domain.Products.Data;
using DigitalPrint.Core.Domain.Products.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPrint.EndPoint.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductQueryController : ControllerBase
    {
        private readonly IProductQueryService _productQueryService;

        public ProductQueryController(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] GetActiveProduct request)
        {
            return new OkObjectResult(_productQueryService.Query(request));
        }

    }
}
