using System.Timers;
using TXSTBXRD_UI.Utils.Types;

namespace TXSTBXRD_UI.Utils
{
    public class NotificationService : IDisposable
    {
        const int DefoultTime = 10000;
        public event Action<string, NotificationLevel, bool, bool>? OnShow;
        public event Action? OnHide;
        private System.Timers.Timer? Countdown;

        public void ShowToast(NotificationLevel level, string message, bool close = false)
        {
            OnShow?.Invoke(message, level, false, close);
            StartCountdown();
        }
        public void ShowToast(NotificationLevel level, string message, int time, bool close = false, bool reload = false)
        {
            OnShow?.Invoke(message, level, reload, close);
            StartCountdown(time);
        }

        private void StartCountdown(int time = DefoultTime)
        {
            SetCountdown(time);

            if (Countdown!.Enabled)
            {
                Countdown.Stop();
                Countdown.Start();
            }
            else
            {
                Countdown!.Start();
            }
        }

        private void SetCountdown(int time)
        {
            if (Countdown != null) return;

            Countdown = new System.Timers.Timer(time);
            Countdown.Elapsed += HideToast;
            Countdown.AutoReset = false;
        }

        private void HideToast(object? source, ElapsedEventArgs args)
            => OnHide?.Invoke();

        public void Dispose()
            => Countdown?.Dispose();
    }
}