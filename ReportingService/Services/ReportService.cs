using ReportingService.Interfaces;
using Shared.DTOs;

namespace ReportingService.Services
{
    public class ReportService : IReportService
    {
        private readonly IBookingClient bookingClient;

        public ReportService(
            IBookingClient bookingClient)
        {
            this.bookingClient = bookingClient;
        }

        public async Task<WeeklySummaryDto> GetWeeklySummaryAsync(
            string nic,
            DateTime selectedDate)
        {
            var bookings =
                await bookingClient.GetBookingsByUserAsync(nic);

            // Selected day is treated as the end of the reporting period.
            DateTime weekEnd = selectedDate.Date;

            // Previous 6 days + selected day = 7-day report.
            DateTime weekStart = weekEnd.AddDays(-6);

            var weeklyBookings = bookings
                .Where(b =>
                    b.TravelDate.Date >= weekStart &&
                    b.TravelDate.Date <= weekEnd)
                .ToList();

            return new WeeklySummaryDto
            {
                WeekStart = weekStart,

                WeekEnd = weekEnd,

                TotalBookings = weeklyBookings.Count,

                ConfirmedBookings = weeklyBookings.Count(b =>
                    b.BookingStatus.Equals(
                        "CONFIRMED",
                        StringComparison.OrdinalIgnoreCase)),

                CancelledBookings = weeklyBookings.Count(b =>
                    b.BookingStatus.Equals(
                        "CANCELLED",
                        StringComparison.OrdinalIgnoreCase)),

                TotalSpent = weeklyBookings.Sum(b => b.TotalAmount)
            };
        }
    }
}