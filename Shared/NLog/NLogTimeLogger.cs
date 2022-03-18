using NLog;
using Shared.NLog.Interfaces;

namespace Shared.NLog;

public class NLogTimeLogger : INLogTimeLogger
{
    private static ILogger _logger = LogManager.GetCurrentClassLogger();
    private const string _loggerName = "NLogTimeLogger";
    private static Type nLogLoggerManagerType = typeof(INLogTimeLogger);
    public NLogTimeLogger()
    {
    }

    public void LogWarn(string message)
    {
        var logEventInfo = LogEventInfo.Create(LogLevel.Warn, _loggerName, message);
        _logger.Log(nLogLoggerManagerType, logEventInfo);
    }
}

