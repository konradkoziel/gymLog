namespace gymLog.API.Services.interfaces
{
    public interface ILogService
    {
        void LogInfo(string message, params object[] parameters);
        void LogWarning(string message, params object[] parameters);
        void LogError(Exception ex, string message, params object[] parameters);
    }
}
