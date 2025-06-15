using gymLog.API.Services.interfaces;

namespace gymLog.API.Services
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;

        public LogService(ILogger<LogService> logger)
        {
            _logger = logger;
        }

        public void LogInfo(string message, params object[] parameters)
        {
            _logger.LogInformation(message, parameters);
        }

        public void LogWarning(string message, params object[] parameters)
        {
            _logger.LogWarning(message, parameters);
        }

        public void LogError(Exception ex, string message, params object[] parameters)
        {
            _logger.LogError(ex, message, parameters);
        }
    }
}
