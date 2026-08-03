using BookingService.Data;
using BookingService.Entities;
using BookingService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;

namespace BookingService.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext context;

        public BookingRepository(BookingDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Train>> GetAllTrainsAsync()
        {
            return await context.Trains
                .OrderBy(t => t.TrainNumber)
                .ToListAsync();
        }

        public async Task<Train?> GetTrainByIdAsync(int trainId)
        {
            return await context.Trains
                .FirstOrDefaultAsync(t => t.TrainId == trainId);
        }

        public async Task<List<Request>> GetAllRequestsAsync()
        {
            return await context.Requests
                .OrderBy(r => r.Description)
                .ToListAsync();
        }

        public async Task<List<Seat>> GetSeatsByTrainAsync(int trainId)
        {
            return await context.Seats
                .Where(s => s.TrainId == trainId)
                .OrderBy(s => s.SeatNumber)
                .ToListAsync();
        }

        public async Task<List<Seat>> GetAvailableSeatsAsync(
            int trainId,
            DateTime travelDate)
        {
            var bookedSeatIds = await context.BookingSeats

                .Include(bs => bs.Booking)

                .Include(bs => bs.Seat)

                .Where(bs =>
                    bs.Seat.TrainId == trainId &&
                    bs.Booking.TravelDate.Date == travelDate.Date &&
                    bs.Booking.BookingStatus == "CONFIRMED")

                .Select(bs => bs.SeatId)

                .ToListAsync();

            return await context.Seats

                .Where(s =>
                    s.TrainId == trainId &&
                    !bookedSeatIds.Contains(s.SeatId))

                .OrderBy(s => s.SeatNumber)

                .ToListAsync();
        }

        public async Task<bool> IsSeatAvailableAsync(
            int trainId,
            int seatId,
            DateTime travelDate)
        {
            return !await context.BookingSeats

                .Include(bs => bs.Booking)

                .Include(bs => bs.Seat)

                .AnyAsync(bs =>
                    bs.SeatId == seatId &&
                    bs.Seat.TrainId == trainId &&
                    bs.Booking.TravelDate.Date == travelDate.Date &&
                    bs.Booking.BookingStatus == "CONFIRMED");
        }

        public async Task<List<Booking>> GetBookingsByUserAsync(
            string userNic)
        {
            return await context.Bookings

                .Include(b => b.Train)

                .Include(b => b.BookingSeats)
                    .ThenInclude(bs => bs.Seat)

                .Include(b => b.BookingRequests)
                    .ThenInclude(br => br.Request)

                .Where(b => b.UserNic == userNic)

                .OrderByDescending(b => b.TravelDate)

                .ToListAsync();
        }

        public async Task<Booking?> GetBookingAsync(
            int bookingId)
        {
            return await context.Bookings

                .Include(b => b.Train)

                .Include(b => b.BookingSeats)
                    .ThenInclude(bs => bs.Seat)

                .Include(b => b.BookingRequests)
                    .ThenInclude(br => br.Request)

                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task AddBookingAsync(
            Booking booking)
        {
            await context.Bookings.AddAsync(booking);
        }

        public Task UpdateBookingAsync(
            Booking booking)
        {
            context.Bookings.Update(booking);

            return Task.CompletedTask;
        }

        public async Task CancelBookingAsync(
            int bookingId)
        {
            var booking = await context.Bookings
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
                throw new Exception("Booking not found.");

            booking.BookingStatus = "CANCELLED";
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<TrainOccupancyDto> GetTrainOccupancyAsync(
    int trainId,
    DateTime travelDate)
        {
            var train = await context.Trains
                .FirstOrDefaultAsync(t => t.TrainId == trainId);

            if (train == null)
            {
                throw new Exception("Train not found.");
            }

            int bookedSeats = await context.BookingSeats
                .Include(bs => bs.Booking)
                .Where(bs =>
                    bs.Booking!.TrainId == trainId &&
                    bs.Booking.TravelDate.Date == travelDate.Date &&
                    bs.Booking.BookingStatus == "CONFIRMED")
                .CountAsync();

            return new TrainOccupancyDto
            {
                TrainId = train.TrainId,
                TrainNumber = train.TrainNumber,
                TravelDate = travelDate,
                TotalSeats = train.TotalSeatCount,
                BookedSeats = bookedSeats,
                CurrentTicketPrice = train.BaseTicketPrice
            };
        }
    }
}