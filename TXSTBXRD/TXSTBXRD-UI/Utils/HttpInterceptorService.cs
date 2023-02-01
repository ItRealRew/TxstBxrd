using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor;

namespace TXSTBXRD_UI.Utils
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navManager;
        private readonly NotificationService _notification;
        public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager, NotificationService notification)
        {
            _interceptor = interceptor;
            _navManager = navManager;
            _notification = notification;
        }
        public void RegisterEvent() => _interceptor.AfterSend += InterceptResponse;
        private void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            _notification.ShowToast("000000", Utils.Types.NotificationLevel.Success);
        }
        public void DisposeEvent() => _interceptor.AfterSend -= InterceptResponse;
    }
}

