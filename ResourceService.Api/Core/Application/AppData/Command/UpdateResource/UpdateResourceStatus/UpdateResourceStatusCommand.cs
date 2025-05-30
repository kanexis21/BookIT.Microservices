using System.Text.Json.Serialization;
using MediatR;
using ResourceService.Api.Core.Domain.Model;

namespace ResourceService.Api.Application.AppData.Command.UpdateResource.UpdateResourceStatus
{
    public class UpdateResourceStatusCommand : IRequest
    {
        public Guid ResourceId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResourceStatus Status { get; set; }
    }

}
