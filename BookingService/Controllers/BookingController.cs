using Shared.DTOs;
using Shared.Requests;

using BookingService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }


        [HttpGet("trains")]
        public async Task<IActionResult> GetTrains()
        {
            var result = await bookingService.GetAllTrainsAsync();

            return Ok(result);
        }


        [HttpGet("requests")]
        public async Task<IActionResult> GetRequests()
        {
            var result = await bookingService.GetAllRequestsAsync();

            return Ok(result);
        }


        [HttpGet("seats")]
        public async Task<IActionResult> GetAvailableSeats(
            int trainId,
            DateTime travelDate)
        {
            var result =
                await bookingService.GetAvailableSeatsAsync(
                    trainId,
                    travelDate);

            return Ok(result);
        }

        [HttpGet("user/{nic}")]
        public async Task<IActionResult> GetBookingsByUser(
            string nic)
        {
            var result =
                await bookingService.GetBookingsByUserAsync(nic);

            return Ok(result);
        }


        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBooking(
            int bookingId)
        {
            var booking =
                await bookingService.GetBookingAsync(bookingId);

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(
            [FromBody] CreateBookingDto request)
        {
            var bookingId =
                await bookingService.CreateBookingAsync(request);

            return CreatedAtAction(
                nameof(GetBooking),
                new { bookingId },
                bookingId);
        }


        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBooking(
            int bookingId,
            [FromBody] CreateBookingDto request)
        {
            await bookingService.UpdateBookingAsync(
                bookingId,
                request);

            return NoContent();
        }


        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> CancelBooking(
            int bookingId)
        {
            await bookingService.CancelBookingAsync(
                bookingId);

            return NoContent();
        }

        [HttpGet("occupancy")]
        public async Task<IActionResult> GetOccupancy(
    int trainId,
    DateTime travelDate)
        {
            var result = await bookingService.GetTrainOccupancyAsync(
                trainId,
                travelDate);

            return Ok(result);
        }
    }
}