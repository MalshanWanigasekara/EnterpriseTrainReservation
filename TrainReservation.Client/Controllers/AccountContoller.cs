using Microsoft.AspNetCore.Mvc;
using Shared.Requests;
using TrainReservation.Client.Interfaces;

namespace TrainReservation.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public AccountController(
            IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var response =
                await authenticationService.LoginAsync(request);

            if (response == null || !response.Success)
            {
                ViewBag.Error = response?.Message ?? "Login failed.";

                return View(request);
            }

            return RedirectToAction(
                "Index",
                "Home");
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var user =
                await authenticationService.RegisterAsync(request);

            if (user == null)
            {
                ViewBag.Error = "Registration failed.";

                return View(request);
            }

            TempData["Success"] =
                "Registration completed successfully.";

            return RedirectToAction(nameof(Login));
        }



        [HttpGet]
        public IActionResult Logout()
        {
            authenticationService.Logout();

            return RedirectToAction(nameof(Login));
        }
    }
}