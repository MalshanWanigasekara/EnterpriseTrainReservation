using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Requests;
using TrainReservation.Client.Interfaces;
using TrainReservation.Client.ViewModels;

namespace TrainReservation.Client.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingApiService bookingService;
        private readonly IAuthenticationService authenticationService;
        private readonly IPredictionApiService predictionService;

        public BookingController(
            IBookingApiService bookingService,
            IAuthenticationService authenticationService,
            IPredictionApiService predictionService)
        {
            this.bookingService = bookingService;
            this.authenticationService = authenticationService;
            this.predictionService = predictionService;
        }


        public async Task<IActionResult> Index()
        {
            if (!authenticationService.IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var nic = authenticationService.GetLoggedInNic();

            var bookings =
                await bookingService.GetMyBookingsAsync(nic!);

            return View(bookings);
        }


        public async Task<IActionResult> Details(int id)
        {
            var booking =
                await bookingService.GetBookingAsync(id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            BookingViewModel model = new()
            {
                Trains = await bookingService.GetAllTrainsAsync() ?? new(),
                Requests = await bookingService.GetAllRequestsAsync() ?? new(),
                Seats = new(),
                SeatsLoaded = false
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoadSeats(
    BookingViewModel model)
        {

            Console.WriteLine($"TrainId = {model.Booking.TrainId}");
            Console.WriteLine($"ModelState Valid = {ModelState.IsValid}");

            foreach (var key in Request.Form.Keys)
            {
                Console.WriteLine($"{key} = {Request.Form[key]}");
            }

            model.Trains =
                await bookingService.GetAllTrainsAsync() ?? new();

            model.Requests =
                await bookingService.GetAllRequestsAsync() ?? new();

            model.Seats =
                await bookingService.GetAvailableSeatsAsync(
                    model.Booking.TrainId,
                    model.Booking.TravelDate) ?? new();

            model.Prediction =
                await predictionService.GetPredictionAsync(
                    model.Booking.TrainId,
                    model.Booking.TravelDate);

            model.SeatsLoaded = true;

            return View("Create", model);
        }



        [HttpPost]
        public async Task<IActionResult> Create(
    BookingViewModel model)
        {
            if (!authenticationService.IsLoggedIn())
                return RedirectToAction("Login", "Account");

            model.Booking.UserNic =
                authenticationService.GetLoggedInNic()!;

            await bookingService.CreateBookingAsync(
                model.Booking);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var booking =
                await bookingService.GetBookingAsync(id);

            if (booking == null)
                return NotFound();

            BookingViewModel model = new()
            {
                Booking = new CreateBookingDto
                {
                    UserNic = authenticationService.GetLoggedInNic()!,
                    TrainId = booking.Train!.TrainId,
                    TravelDate = booking.TravelDate,
                    IsRecurring = booking.IsRecurring,
                    SeatIds = booking.Seats.Select(x => x.SeatId).ToList(),
                    RequestIds = booking.Requests.Select(x => x.RequestId).ToList()
                },

                Trains =
                    await bookingService.GetAllTrainsAsync() ?? new(),

                Requests =
                    await bookingService.GetAllRequestsAsync() ?? new(),

                Seats =
                    await bookingService.GetAvailableSeatsAsync(
                        booking.Train.TrainId,
                        booking.TravelDate) ?? new(),

                SeatsLoaded = true
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            int id,
            CreateBookingDto request)
        {
            request.UserNic =
                authenticationService.GetLoggedInNic()!;

            await bookingService.UpdateBookingAsync(
                id,
                request);

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            await bookingService.CancelBookingAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}