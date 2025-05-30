using MediatR;
using ResourceService.Api.Core.Domain.Model;

namespace ResourceService.Api.Application.AppData.Command.UpdateResource
{
    public record UpdateResourceCommand(Guid Id, string Name, string? Location, string? Description, ResourceStatus Status) : IRequest;
}
