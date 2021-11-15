using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieFriend.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FilmController.Controllers
{
    public class FilmController : Controller
    {
        private readonly ILogger<FilmController> _logger;

        public FilmController(ILogger<FilmController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FilmList()
        {
            List<Film> Films = new List<Film> { };
            NpgsqlConnection connection = new NpgsqlConnection("User ID=user1;Password=changeme;Host=178.154.198.196;Port=5432;Database=tododb;");
            try
            {
                Films = connection.Query<Film>("SELECT * FROM \"Films\"").ToList();
            }
            finally
            {
                connection.Dispose();
            }
            return View(Films);
        }
        [Authorize]
        [HttpGet]
        public IActionResult SeriesList()
        {
            List<Series> Series = new List<Series> { };
            NpgsqlConnection connection = new NpgsqlConnection("User ID=user1;Password=changeme;Host=178.154.198.196;Port=5432;Database=tododb;");
            try
            {
                Series = connection.Query<Series>("SELECT * FROM \"Series\"").ToList();
            }
            finally
            {
                connection.Dispose();
            }
            return View(Series);
        }
        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddSeries()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult AddFilm(AddMovie addMovie)
        {
            addMovie.NameAuthor = "Тест";
            if (ModelState.IsValid)
            {
                NpgsqlConnection connection = new NpgsqlConnection("User ID=user1;Password=changeme;Host=178.154.198.196;Port=5432;Database=tododb;");
                try
                {
                    connection.Query($"Insert into \"Films\" (\"Images\", \"Called\", \"Trailer\", \"Description\", \"Reviews\", \"Rating\", \"NameAuthor\") values ('{addMovie.Images}', '{addMovie.Called}', '{addMovie.Trailer}', '{addMovie.Description}', '{addMovie.Reviews}', '{addMovie.Rating}', '{addMovie.NameAuthor}')");


                }
                finally
                {
                    connection.Dispose();
                }
            }
            return Redirect("FilmList");
        }
        [HttpPost]
        public IActionResult AddSeries(AddMovie addMovie)
        {
            addMovie.NameAuthor = "Тест";
            if (ModelState.IsValid)
            {
                NpgsqlConnection connection = new NpgsqlConnection("User ID=user1;Password=changeme;Host=178.154.198.196;Port=5432;Database=tododb;");
                try
                {
                    connection.Query($"Insert into \"Series\" (\"Images\", \"Called\", \"Trailer\", \"Description\", \"Reviews\", \"Rating\", \"NameAuthor\") values ('{addMovie.Images}', '{addMovie.Called}', '{addMovie.Trailer}', '{addMovie.Description}', '{addMovie.Reviews}', '{addMovie.Rating}', '{addMovie.NameAuthor}')");


                }
                finally
                {
                    connection.Dispose();
                }
            }
            return Redirect("SeriesList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
