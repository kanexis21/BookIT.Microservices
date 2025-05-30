using BookingService.Api.Core.Domain.Model;
using MediatR;
using System.Text.Json.Serialization;

namespace BookingService.Api.Application.AppData.Command.UpdateBooking.UpdateBookingStatus
{
    public class UpdateBookingStatusCommand : IRequest
    {
        public Guid BookingId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookingStatus Status { get; set; }
    }
}
