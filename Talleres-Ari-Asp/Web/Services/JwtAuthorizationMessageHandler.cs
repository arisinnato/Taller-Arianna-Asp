using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Talleres_Ari_Asp.Web.Services
{
    public class JwtAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IJSRuntime _js;

        public JwtAuthorizationMessageHandler(IJSRuntime js)
        {
            _js = js;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}