using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LogService", menuName = "Services/LogService")]
public class LogService : Service
{
    public enum LogLevel
    {
        Info,
        Debug,
        Warning,
        Error,
        None = 9999
    }

    [SerializeField] private LogLevel _logLevel = LogLevel.Debug;
    [SerializeField] private bool _logToConsole = true;
    [SerializeField] private bool _logToPlayerLog = false;

    public override void AddToContainer(IServiceContainer container)
    {
        var logService = Instantiate(this) as LogService;
        container.Register<LogService>(logService);
        container.Register<Service>(logService);
        container.Register<IDisposable>(logService);
    }
    
    public void I(string message)
    {
        Log(LogLevel.Info, message);
    }

    public void D(string message)
    {
        Log(LogLevel.Debug, message);
    }

    public void W(string message)
    {
        Log(LogLevel.Warning, message);
    }

    public void E(string message)
    {
        Log(LogLevel.Error, message);
    }

    void Log(LogLevel logLevel, string message)
    {
        if(logLevel < _logLevel)
        {
            return;
        }

        string formatOutput = "[" + logLevel.ToString() + "] " + message;

        if (_logToConsole)
        {
            Debug.Log(formatOutput);
        }

        if (_logToPlayerLog)
        {
            System.Console.WriteLine(formatOutput);
        }

    }
}
