namespace SupportService.Core.Application.Services.Clients
{
    public interface IUserClient
    {
        Task<string> GetUserNameAsync(Guid userId);
    }

}
