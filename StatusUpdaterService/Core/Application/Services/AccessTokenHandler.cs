using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace StatusUpdaterService.Core.Application.Services
{
    public class AccessTokenHandler : DelegatingHandler
    {
        private readonly TokenService _tokenService;

        public AccessTokenHandler(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenService.GetTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }
    }

}
