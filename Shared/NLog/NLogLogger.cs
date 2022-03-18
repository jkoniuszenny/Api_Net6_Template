using NLog;
using Shared.NLog.Interfaces;

namespace Shared.NLog;

public class NLogLogger : INLogLogger
{
    private static ILogger _logger = LogManager.GetCurrentClassLogger();
    private const string _loggerName = "NLogLogger";
    private static Type nLogLoggerManagerType = typeof(INLogLogger);

    public NLogLogger()
    {
    }

    public void LogDebug(string message)
    {
        var logEventInfo = LogEventInfo.Create(LogLevel.Debug, _loggerName, message);
        _logger.Log(nLogLoggerManagerType, logEventInfo);
    }

    public void LogError(string message)
    {
        var logEventInfo = LogEventInfo.Create(LogLevel.Error, _loggerName, message);
        _logger.Log(nLogLoggerManagerType, logEventInfo);
    }

    public void LogError(string message, Exception ex)
    {
        var logEventInfo = LogEventInfo.Create(LogLevel.Error, _loggerName, ex, null, message);
        _logger.Log(nLogLoggerManagerType, logEventInfo);

    }

    public void LogInfo(string message)
    {
        var logEventInfo = LogEventInfo.Create(LogLevel.Info, _loggerName, message);
        _logger.Log(nLogLoggerManagerType, logEventInfo);
    }

    public void LogWarn(string message)
    {
        var logEventInfo = LogEventInfo.Create(LogLevel.Warn, _loggerName, message);
        _logger.Log(nLogLoggerManagerType, logEventInfo);
    }
    public void LogFatal(string message)
    {
        var logEventInfo = LogEventInfo.Create(LogLevel.Fatal, _loggerName, message);

        _logger.Log(nLogLoggerManagerType, logEventInfo);
    }

}

