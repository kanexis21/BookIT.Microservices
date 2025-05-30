using BookingService.Api.Core.Domain.Model.ViewModel;
using MediatR;

namespace BookingService.Api.Core.Application.AppData.Queries.GetFreeSlots
{
    public class GetFreeSlotsQuery : IRequest<List<TimeSlotViewModel>>
    {
        public Guid? RoomId { get; set; }
        public Guid? ResourceId { get; set; }
        public DateTime Date { get; set; } 
    }
}
