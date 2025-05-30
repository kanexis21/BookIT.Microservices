using MediatR;
using ResourceService.Api.Core.Domain.Model;

namespace ResourceService.Api.Application.AppData.Queries.GetResourceById
{
    public record GetResourceByIdQuery(Guid Id) : IRequest<Resource?>;
}
