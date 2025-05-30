using FluentValidation;

namespace RoomService.Api.Core.Application.AppData.Command.UpdateRoom.UpdateRoomStatus
{
    public class UpdateRoomStatusCommandValidator : AbstractValidator<UpdateRoomStatusCommand>
    {
        public UpdateRoomStatusCommandValidator()
        {
            RuleFor(x => x.RoomId).NotEmpty();
            RuleFor(x => x.Status).IsInEnum();
        }
    }
}
