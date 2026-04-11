namespace AniVault.Services
{
    public enum ToastType
    {
        Success,
        Error,
        Info
    }

    public class ToastMessage
    {
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = "info";
    }

    public class ToastService
    {
        public List<ToastMessage> Toasts { get; private set; } = new();
        public event Action? OnChange;

        public void Show(string message, ToastType type = ToastType.Info)
        {
            var toast = new ToastMessage
            {
                Message = message,
                Type = type.ToString().ToLower()
            };

            Toasts.Add(toast);
            OnChange?.Invoke();

            _ = RemoveAfterDelay(toast);
        }

        private async Task RemoveAfterDelay(ToastMessage toast)
        {
            await Task.Delay(3500);
            Toasts.Remove(toast);
            OnChange?.Invoke();
        }
    }
}
