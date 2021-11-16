using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApplication.Models;
using WebApplication.Services;
using System.Linq;
using System;
using System.Net.Http;

namespace WebApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IRepository repository, IConfiguration configuration)
        {
            _logger = logger;
            _repository = repository;
            _configuration = configuration;
        }

        // GET
        public async Task<IActionResult> Index(string keyword)
        {
            var model = await _repository.GetStudentsAsync(keyword);
            return View(model);
        }

        public async Task<IActionResult> Weather()
        {
            var consulClient = new ConsulClient(c =>
            {
                c.Address = new System.Uri(_configuration["ConsulAddr"]);
                c.Datacenter = "dc1";
            });

            var services = await consulClient.Agent.Services();
            var service = services.Response.Values.Where(t => "WeatherForecast".Equals(t.Service, System.StringComparison.OrdinalIgnoreCase))
                .OrderBy(t => Guid.NewGuid())
                .FirstOrDefault();
            if (service == null)
            {
                return Json(new
                {
                    Message = "Agent Service Not Found."
                });
            }

            using(var httpClient = new HttpClient())
            {
                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"http://{service.Address}:{service.Port}/weatherforecast")
                };
                var respones = await httpClient.SendAsync(httpRequestMessage);
                var str = await respones.Content.ReadAsStringAsync();
                return Content(str);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new User { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(User user)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "The Email or password invalid");
                return View(user);
            }
            _logger.LogInformation("user.ReturnUrl: {0}", user.ReturnUrl);
            _logger.LogInformation("user.Email: {0}", user.Email);
            _logger.LogInformation("user.Password: {0}", user.Password);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Password),
                new Claim(ClaimTypes.Uri, user.ReturnUrl)
            };

            var principal = new ClaimsPrincipal(new[] { new ClaimsIdentity(claims, "Test") });

            HttpContext.SignInAsync(principal);
            return Redirect(user.ReturnUrl);
        }
    }
}