using MediatR;
using RoomService.Api.Core.Domain.Model;

namespace RoomService.Api.Core.Application.AppData.Query.GetResourcesByIds
{
    public class GetRoomsByIdsQuery : IRequest<List<RoomShortViewModel>>
    {
        public List<Guid> Ids { get; set; }

        public GetRoomsByIdsQuery(List<Guid> ids)
        {
            Ids = ids;
        }
    }

}
