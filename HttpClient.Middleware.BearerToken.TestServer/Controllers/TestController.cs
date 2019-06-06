using Microsoft.AspNetCore.Mvc;

namespace HttpClient.Middleware.BearerToken.TestServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        private readonly ICaptureAuthorizationHeader _captureAuthorizationHeader;

        public Test(ICaptureAuthorizationHeader captureAuthorizationHeader)
        {
            _captureAuthorizationHeader = captureAuthorizationHeader;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var headerValue = this.Request.Headers["Authorization"];
            _captureAuthorizationHeader.AuthorizationHeader(headerValue);
            return "Test Response";
        }
    }
}
