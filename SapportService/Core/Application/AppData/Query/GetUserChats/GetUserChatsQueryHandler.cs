using MediatR;
using Microsoft.EntityFrameworkCore;
using SupportService.Core.Application.Interfaces;
using SupportService.Core.Application.Services.Clients;
using SupportService.Core.Domain.Models.Dto;

namespace SupportService.Core.Application.AppData.Query.GetUserChats
{
    public class GetUserChatsQueryHandler : IRequestHandler<GetUserChatsQuery, List<UserDto>>
    {
        private readonly IMessageDbContext _context;
        private readonly Guid SupportUserId = Guid.Parse("f0b910ff-7740-4361-b700-4f6038cac663");
        private readonly IUserClient _userClient;
        public GetUserChatsQueryHandler(IMessageDbContext context, IUserClient userClient)
        {
            _context = context;
            _userClient = userClient;
        }

        public async Task<List<UserDto>> Handle(GetUserChatsQuery request, CancellationToken cancellationToken)
        {
            var userIds = await _context.Messages
                .Where(m => m.ReceiverId == SupportUserId)
                .Select(m => m.SenderId)
                .Distinct()
                .ToListAsync(cancellationToken);

            var users = new List<UserDto>();

            foreach (var userId in userIds)
            {
                var userName = await _userClient.GetUserNameAsync(userId);
                users.Add(new UserDto
                {
                    Id = userId,
                    Name = userName
                });
            }

            return users;
        }
    }

}
