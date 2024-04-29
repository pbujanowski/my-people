using Serilog;

namespace MyPeople.Common.Logging;

public static class LoggingInitializer
{
    public static void Initialize(Action applicationStartup)
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        try
        {
            applicationStartup.Invoke();
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Error during application startup.");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
