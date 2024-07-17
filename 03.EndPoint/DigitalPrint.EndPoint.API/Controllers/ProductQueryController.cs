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

        [HttpGet("active-product")]
        public IActionResult Get([FromQuery] GetActiveProduct request)
        {
            var response = _productQueryService.Query(request);
            return new OkObjectResult(response);
        }

        [HttpGet("active-product-list")]
        public IActionResult Get([FromQuery] GetActiveProductList request)
        {
            var response = _productQueryService.Query(request);
            return new OkObjectResult(response);
        }

        [HttpGet("product-for-specific-creator")]
        public IActionResult Get([FromQuery] GetProductForSpecificCreator request)
        {
            var response = _productQueryService.Query(request);
            return new OkObjectResult(response);
        }
    }
}