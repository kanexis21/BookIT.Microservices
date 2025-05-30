using MediatR;
using ResourceService.Api.Core.Domain.Model;

namespace ResourceService.Api.Core.Application.AppData.Query.GetResourcesByIds
{
    public class GetResourcesByIdsQuery : IRequest<List<ResourceShortViewModel>>
    {
        public List<Guid> Ids { get; set; }

        public GetResourcesByIdsQuery(List<Guid> ids)
        {
            Ids = ids;
        }
    }

}
