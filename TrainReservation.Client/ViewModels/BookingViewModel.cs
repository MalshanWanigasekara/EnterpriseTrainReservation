using Shared.DTOs;
using Shared.Requests;

namespace TrainReservation.Client.ViewModels
{
    public class BookingViewModel
    {
        public CreateBookingDto Booking { get; set; } = new();

        public List<TrainDto> Trains { get; set; } = new();

        public List<RequestDto> Requests { get; set; } = new();

        public List<SeatDto> Seats { get; set; } = new();

        public PredictionNotificationDto? Prediction { get; set; }

        public bool SeatsLoaded { get; set; }
    }
}