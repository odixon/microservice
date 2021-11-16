using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Test.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login([FromQuery(Name = "returnUrl")]string url)
        {
            _logger.LogInformation($"Enter {nameof(Login)}");

            var claimName = new Claim(ClaimTypes.Name, "Aiden");
            var claimEmail = new Claim(ClaimTypes.Email, "Aiden.Wang1@cn.ey.com");

            var identity = new ClaimsIdentity(new[] { claimName, claimEmail }, "Cookie");
            HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            _logger.LogInformation("Redirect to {0}", url);
            return Redirect(url);
        }
    }
}