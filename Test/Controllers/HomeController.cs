using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Test.Repositories;

namespace Test.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _repository;
        private readonly string _connectionString;

        public HomeController(IMapper mapper, IConfiguration configuration, IMovieRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _repository.GetAllAsync();
            return View(movies);
        }
    }
}