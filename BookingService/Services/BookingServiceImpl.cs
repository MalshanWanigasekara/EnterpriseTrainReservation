using Shared.DTOs;
using Shared.Requests;

using BookingService.Entities;
using BookingService.Interfaces;

namespace BookingService.Services
{
    public class BookingServiceImpl : IBookingService
    {
        private readonly IBookingRepository repository;

        public BookingServiceImpl(IBookingRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<TrainDto>> GetAllTrainsAsync()
        {
            var trains = await repository.GetAllTrainsAsync();

            return trains.Select(t => new TrainDto
            {
                TrainId = t.TrainId,
                TrainNumber = t.TrainNumber,
                StartStation = t.StartStation,
                EndStation = t.EndStation,
                DepartureTime = t.DepartureTime,
                ArrivalTime = t.ArrivalTime,
                BaseTicketPrice = t.BaseTicketPrice
            }).ToList();
        }

        public async Task<List<RequestDto>> GetAllRequestsAsync()
        {
            var requests = await repository.GetAllRequestsAsync();

            return requests.Select(r => new RequestDto
            {
                RequestId = r.RequestId,
                Description = r.Description,
                AdditionalPrice = r.AdditionalPrice
            }).ToList();
        }

        public async Task<List<SeatDto>> GetAvailableSeatsAsync(
            int trainId,
            DateTime travelDate)
        {
            var seats = await repository.GetAvailableSeatsAsync(
                trainId,
                travelDate);

            return seats.Select(s => new SeatDto
            {
                SeatId = s.SeatId,
                SeatNumber = s.SeatNumber
            }).ToList();
        }

        public async Task<List<BookingDto>> GetBookingsByUserAsync(
            string userNic)
        {
            var bookings =
                await repository.GetBookingsByUserAsync(userNic);

            return bookings.Select(ConvertBooking).ToList();
        }

        public async Task<BookingDto?> GetBookingAsync(
            int bookingId)
        {
            var booking =
                await repository.GetBookingAsync(bookingId);

            if (booking == null)
                return null;

            return ConvertBooking(booking);
        }

        public async Task<int> CreateBookingAsync(
            CreateBookingDto request)
        {
            var train =
                await repository.GetTrainByIdAsync(request.TrainId);

            if (train == null)
                throw new Exception("Train not found.");

            foreach (var seatId in request.SeatIds)
            {
                bool available =
                    await repository.IsSeatAvailableAsync(
                        request.TrainId,
                        seatId,
                        request.TravelDate);

                if (!available)
                    throw new Exception($"Seat {seatId} is already booked.");
            }

            decimal total =
                train.BaseTicketPrice * request.SeatIds.Count;

            var requests =
                await repository.GetAllRequestsAsync();

            foreach (var requestId in request.RequestIds)
            {
                var specialRequest =
                    requests.First(r => r.RequestId == requestId);

                total += specialRequest.AdditionalPrice;
            }

            Booking booking = new Booking
            {
                TravelDate = request.TravelDate,
                BookingStatus = "CONFIRMED",
                IsRecurring = request.IsRecurring,
                TrainId = request.TrainId,
                UserNic = request.UserNic,
                TotalAmount = total
            };

            foreach (var seatId in request.SeatIds)
            {
                booking.BookingSeats.Add(
                    new BookingSeat
                    {
                        SeatId = seatId
                    });
            }

            foreach (var requestId in request.RequestIds)
            {
                booking.BookingRequests.Add(
                    new BookingRequest
                    {
                        RequestId = requestId
                    });
            }

            await repository.AddBookingAsync(booking);

            await repository.SaveChangesAsync();

            return booking.BookingId;
        }

        public async Task UpdateBookingAsync(
            int bookingId,
            CreateBookingDto request)
        {
            var booking =
                await repository.GetBookingAsync(bookingId);

            if (booking == null)
                throw new Exception("Booking not found.");

            booking.TravelDate = request.TravelDate;
            booking.IsRecurring = request.IsRecurring;

            await repository.UpdateBookingAsync(booking);

            await repository.SaveChangesAsync();
        }

        public async Task CancelBookingAsync(
            int bookingId)
        {
            await repository.CancelBookingAsync(bookingId);

            await repository.SaveChangesAsync();
        }

        private BookingDto ConvertBooking(
            Booking booking)
        {
            return new BookingDto
            {
                BookingId = booking.BookingId,

                TravelDate = booking.TravelDate,

                BookingStatus = booking.BookingStatus,

                IsRecurring = booking.IsRecurring,

                TotalAmount = booking.TotalAmount,

                Train = new TrainDto
                {
                    TrainId = booking.Train!.TrainId,
                    TrainNumber = booking.Train.TrainNumber,
                    StartStation = booking.Train.StartStation,
                    EndStation = booking.Train.EndStation,
                    DepartureTime = booking.Train.DepartureTime,
                    ArrivalTime = booking.Train.ArrivalTime,
                    BaseTicketPrice = booking.Train.BaseTicketPrice
                },

                Seats = booking.BookingSeats
                    .Select(bs => new SeatDto
                    {
                        SeatId = bs.Seat!.SeatId,
                        SeatNumber = bs.Seat.SeatNumber
                    }).ToList(),

                Requests = booking.BookingRequests
                    .Select(br => new RequestDto
                    {
                        RequestId = br.Request!.RequestId,
                        Description = br.Request.Description,
                        AdditionalPrice = br.Request.AdditionalPrice
                    }).ToList()
            };
        }

        public async Task<TrainOccupancyDto> GetTrainOccupancyAsync(
    int trainId,
    DateTime travelDate)
        {
            return await repository.GetTrainOccupancyAsync(
                trainId,
                travelDate);
        }
    }
}