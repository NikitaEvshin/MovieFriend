using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieFriend.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieFriend.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(ILogger<AuthorizationController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult ErrorRegistration()
        {
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult ErrorAccess()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginAuthorization(AuthorizationUser user)
        {
            NpgsqlConnection connection = new NpgsqlConnection("User ID=user1;Password=changeme;Host=178.154.198.196;Port=5432;Database=tododb;");

            try 
            {
                List<AuthorizationUser> LoginUser = connection.Query<AuthorizationUser>($"SELECT * FROM \"Userregistration\" WHERE \"Login\" = '{user.Login}' AND \"Password\" = '{user.Password}'").ToList();

                if (LoginUser.Count == 0)
                {
                    return Redirect("ErrorRegistration");
                }
                else
                {

                    List<Claim> claims = new List<Claim> { new Claim(user.Login, user.Password) };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(new[] { identity});
                    HttpContext.SignInAsync(principal).Wait();

                    return Redirect("Welcome");
                }
            }
            finally
            {
                connection.Dispose();
            }

           

        }

        [HttpPost]
        public IActionResult SendRegisterForm(CreateNewUser newUser)
        {
            if (ModelState.IsValid)
            {
                NpgsqlConnection connection = new NpgsqlConnection("User ID=user1;Password=changeme;Host=178.154.198.196;Port=5432;Database=tododb;");
                try
                {
                    List <CreateNewUser> LoginUser = connection.Query<CreateNewUser>($"SELECT * FROM \"Userregistration\" WHERE \"Login\" = '{newUser.Login}' AND \"Email\" = '{newUser.Email}'").ToList();
                    if (LoginUser.Count == 0)
                    {
                        connection.Query($"Insert into \"Userregistration\" (\"Email\", \"Login\", \"Password\") values ('{newUser.Email}', '{newUser.Login}', '{newUser.Password}')");
                    }
                    else
                    {
                        return Redirect("ErrorRegistration");
                    }
                }
                finally
                {
                    connection.Dispose();
                }
            }
            return Redirect("Login");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
