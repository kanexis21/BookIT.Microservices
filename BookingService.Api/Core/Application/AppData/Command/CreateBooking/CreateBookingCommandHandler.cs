using System.Text;
using BookingService.Api.Core.Application.Interfaces;
using BookingService.Api.Core.Domain.Model;
using MediatR;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BookingService.Api.Application.AppData.Command.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
    {
        private readonly IBookingDbContext _bookingRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateBookingCommandHandler(
            IBookingDbContext bookingRepository,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _bookingRepository = bookingRepository;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("sub")
                  ?? _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("Не удалось определить пользователя.");

            bool overlapping;
            if (request.ResourceId != null)
            {
                overlapping = _bookingRepository.Bookings.Any(b =>
                    b.ResourceId == request.ResourceId &&
                    b.Status == BookingStatus.Забронировано &&
                    (
                        (request.StartTime < b.EndTime && request.EndTime > b.StartTime)
                    )
                );
            }
            else
            {
                overlapping = _bookingRepository.Bookings.Any(b =>
                    b.RoomId == request.RoomId &&
                    b.Status == BookingStatus.Забронировано &&
                    (
                        (request.StartTime < b.EndTime && request.EndTime > b.StartTime)
                    )
                );

            }


            if (overlapping)
            {
                throw new InvalidOperationException("На выбранное время ресурс уже забронирован.");
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var client = _httpClientFactory.CreateClient("ApiGateway");

            Booking booking;
            if (request.ResourceId != null)
            {
                booking = new Booking
                {
                    Id = Guid.NewGuid(),
                    ResourceId = request.ResourceId,
                    StartTime = request.StartTime,
                    UserId = userId,
                    EndTime = request.EndTime,
                    Description = request.Description,
                    Status = BookingStatus.Забронировано
                };

                var patchContent = new StringContent(
                JsonConvert.SerializeObject(new { Status = 1, ResourceId = request.ResourceId }),
                Encoding.UTF8,
                "application/json");

                var response = await client.PatchAsync($"resource/api/resource/{request.ResourceId}/status", patchContent, cancellationToken);
                response.EnsureSuccessStatusCode();
            }
            else
            {
                booking = new Booking
                {
                    Id = Guid.NewGuid(),
                    RoomId = request.RoomId,
                    StartTime = request.StartTime,
                    UserId = userId,
                    EndTime = request.EndTime,
                    Description = request.Description,
                    Status = BookingStatus.Забронировано
                };

            }

            _bookingRepository.Bookings.Add(booking);
            await _bookingRepository.SaveChangesAsync(cancellationToken);

            return booking.Id;
        }
    }

}

