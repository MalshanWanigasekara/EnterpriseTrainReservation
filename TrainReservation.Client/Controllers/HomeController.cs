using Microsoft.AspNetCore.Mvc;
using TrainReservation.Client.Interfaces;

namespace TrainReservation.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public HomeController(
            IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            if (!authenticationService.IsLoggedIn())
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            ViewBag.User =
                authenticationService.GetLoggedInNic();

            return View();
        }
    }
}