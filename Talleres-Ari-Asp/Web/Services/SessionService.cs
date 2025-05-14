using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Talleres_Ari_Asp.Web.Services
{
    public class SessionService
    {
        private readonly IJSRuntime _js;

        public SessionService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SaveToken(string token)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", "jwt", token);
        }

        public async Task<string?> GetToken()
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", "jwt");
        }

        public async Task RemoveToken()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "jwt");
        }
    }
}