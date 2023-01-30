using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor;

namespace TXSTBXRD_UI.Utils
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navManager;
        public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager)
        {
            _interceptor = interceptor;
            _navManager = navManager;
        }
        public void RegisterEvent() => _interceptor.AfterSend += InterceptResponse;
        private void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            _navManager.NavigateTo("/404");
        }
        public void DisposeEvent() => _interceptor.AfterSend -= InterceptResponse;
    }
}

