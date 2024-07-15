using Microsoft.AspNetCore.Mvc;

namespace DigitalPrint.EndPoint.API.Services;

public static class RequestHandler
{
    public static IActionResult HandleRequest<T>(T request, Action<T> handler)
    {
        try
        {
            handler(request);
            return new OkResult();
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(new
            {
                error = e.Message,
                stackTrace = e.StackTrace
            });
        }
    }
}