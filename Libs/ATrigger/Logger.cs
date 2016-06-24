using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Logger
{
    public delegate void LogHandler(string msg);
    private static LogHandler info_handler;
    private static LogHandler warning_handler;
    private static LogHandler error_handler;

    public static void setErrorHandler(LogHandler handler)
    {
        Logger.error_handler = handler;
    }
    public static void setInfoHandler(LogHandler handler)
    {
        Logger.info_handler = handler;
    }
    public static void setWarningHandler(LogHandler handler)
    {
        Logger.warning_handler = handler;
    }
    public static void logWarning(string msg)
    {
        if (warning_handler != null)
            warning_handler(msg);
    }
    public static void logInfo(string msg)
    {
        if (info_handler != null)
            info_handler(msg);
    }
    public static void logError(string msg)
    {
        if (error_handler != null)
            error_handler(msg);
    }
    public static void warning(string msg)
    {
        if (warning_handler != null)
            warning_handler(msg);
    }
    public static void info(string msg)
    {
        if (info_handler != null)
            info_handler(msg);
    }
    public static void error(string msg)
    {
        if (error_handler != null)
            error_handler(msg);
    }
    public static void except(Exception exc)
    {
        if (error_handler != null)
            error_handler(exc.Message);
    }
    public static void debug(string msg)
    {
        if (info_handler != null)
            info_handler(msg);
    }
}
