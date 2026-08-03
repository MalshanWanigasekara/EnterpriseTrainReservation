using BookingService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(
            DbContextOptions<BookingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings => Set<Booking>();

        public DbSet<Train> Trains => Set<Train>();

        public DbSet<Seat> Seats => Set<Seat>();

        public DbSet<Request> Requests => Set<Request>();

        public DbSet<BookingSeat> BookingSeats => Set<BookingSeat>();

        public DbSet<BookingRequest> BookingRequests => Set<BookingRequest>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            configureBookingSeat(modelBuilder);

            configureBookingRequest(modelBuilder);

            seedTrains(modelBuilder);

            seedRequests(modelBuilder);

            seedSeats(modelBuilder);
        }

        private void configureBookingSeat(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingSeat>()
                .HasKey(x => new
                {
                    x.BookingId,
                    x.SeatId
                });

            modelBuilder.Entity<BookingSeat>()
                .HasOne(x => x.Booking)
                .WithMany(x => x.BookingSeats)
                .HasForeignKey(x => x.BookingId);

            modelBuilder.Entity<BookingSeat>()
                .HasOne(x => x.Seat)
                .WithMany(x => x.BookingSeats)
                .HasForeignKey(x => x.SeatId);
        }

        private void configureBookingRequest(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingRequest>()
                .HasKey(x => new
                {
                    x.BookingId,
                    x.RequestId
                });

            modelBuilder.Entity<BookingRequest>()
                .HasOne(x => x.Booking)
                .WithMany(x => x.BookingRequests)
                .HasForeignKey(x => x.BookingId);

            modelBuilder.Entity<BookingRequest>()
                .HasOne(x => x.Request)
                .WithMany(x => x.BookingRequests)
                .HasForeignKey(x => x.RequestId);
        }

        private void seedTrains(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Train>().HasData(

                new Train
                {
                    TrainId = 1,
                    TrainNumber = "IC101",
                    StartStation = "Colombo",
                    EndStation = "Kandy",
                    DepartureTime = DateTime.Parse("2026-01-01 07:00"),
                    ArrivalTime = DateTime.Parse("2026-01-01 09:30"),
                    BaseTicketPrice = 120,
                    TotalSeatCount = 20
                },

                new Train
                {
                    TrainId = 2,
                    TrainNumber = "IC205",
                    StartStation = "Colombo",
                    EndStation = "Galle",
                    DepartureTime = DateTime.Parse("2026-01-01 08:00"),
                    ArrivalTime = DateTime.Parse("2026-01-01 10:00"),
                    BaseTicketPrice = 90,
                    TotalSeatCount = 20
                }

            );
        }

        private void seedRequests(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>().HasData(

                new Request
                {
                    RequestId = 1,
                    Description = "Window Seat",
                    AdditionalPrice = 10
                },

                new Request
                {
                    RequestId = 2,
                    Description = "Wheelchair Assistance",
                    AdditionalPrice = 0
                },

                new Request
                {
                    RequestId = 3,
                    Description = "Meal",
                    AdditionalPrice = 25
                }

            );
        }

        private void seedSeats(ModelBuilder modelBuilder)
        {
            int id = 1;

            for (int train = 1; train <= 2; train++)
            {
                for (int i = 1; i <= 20; i++)
                {
                    modelBuilder.Entity<Seat>().HasData(

                        new Seat
                        {
                            SeatId = id++,
                            SeatNumber = "S" + i,
                            TrainId = train
                        }

                    );
                }
            }
        }
    }
}