using Microsoft.AspNetCore.Mvc;
using TrainReservation.Client.Interfaces;
using TrainReservation.Client.Services;

namespace TrainReservation.Client.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportApiService reportService;
        private readonly IAuthenticationService authenticationService;

        public ReportController(
            IReportApiService reportService,
            IAuthenticationService authenticationService)
        {
            this.reportService = reportService;
            this.authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> Weekly(DateTime? selectedDate)
        {
            var nic = authenticationService.GetLoggedInNic();

            if (string.IsNullOrEmpty(nic))
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            DateTime date =
                selectedDate ?? DateTime.Today;

            var report =
                await reportService.GetWeeklySummaryAsync(
                    nic,
                    date);

            ViewBag.SelectedDate = date;

            return View(report);
        }
    }
}