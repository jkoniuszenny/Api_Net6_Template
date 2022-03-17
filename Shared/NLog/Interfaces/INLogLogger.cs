namespace Shared.NLog.Interfaces;

public interface INLogLogger
{
    void LogInfo(string message);
    void LogWarn(string message);
    void LogDebug(string message);
    void LogError(string message);
    void LogError(string message, Exception ex);
    void LogFatal(string message);
}

