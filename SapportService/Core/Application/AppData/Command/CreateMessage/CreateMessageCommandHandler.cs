using MediatR;
using SapportService.Core.Domain.Models;
using SupportService.Core.Application.Interfaces;
using SupportService.Core.Application.Services.Clients;

namespace SupportService.Core.Application.AppData.Command.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Guid>
    {
        private readonly IMessageDbContext _dbContext;
        private readonly IUserClient _userClient;

        public CreateMessageCommandHandler(IMessageDbContext dbContext, IUserClient userClient)
        {
            _dbContext = dbContext;
            _userClient = userClient;
        }

        public async Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var senderName = await _userClient.GetUserNameAsync(request.SenderId);

            var message = new Message
            {
                Id = Guid.NewGuid(),
                SenderId = request.SenderId,
                Text = request.Text,
                SenderName = senderName
            };

            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return message.Id;
        }
    }


}
