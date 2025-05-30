using MediatR;

namespace ResourceService.Api.Application.AppData.Command.CreateResource
{
    public class CreateResourceCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
    }
}
