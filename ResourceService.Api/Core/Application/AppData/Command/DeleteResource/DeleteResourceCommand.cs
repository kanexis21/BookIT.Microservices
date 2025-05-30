using MediatR;

namespace ResourceService.Api.Application.AppData.Command.DeleteResource
{
    public record DeleteResourceCommand(Guid Id) : IRequest;
}
