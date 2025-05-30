using MediatR;
using ResourceService.Api.Core.Domain.Model;

namespace ResourceService.Api.Application.AppData.Queries.GetAllResources
{
    public class GetAllResourcesQuery : IRequest<List<Resource>> { }
}
