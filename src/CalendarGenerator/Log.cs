namespace CalendarGenerator;

// Simple logger to output information/errors to the Console
public static class Log
{
    private const string InfoText = "INFO";
    private const string ErrorText = "ERROR";

    public static void Info(string message)
        => Write(InfoText, message);

    public static void Error(string message)
        => Write(ErrorText, message);

    public static void Exception(Exception e)
        => Write(ErrorText, CollectInfo(e));

    #region Detail

    private static void Write(string category, string message)
        => Console.WriteLine(Build(category, message));

    private static void Write(string category, IEnumerable<string> messages)
        => messages.ForEach(m => Write(category, m));

    private static string Build(string category, string message)
        => $"[{DateTime.Now:s}] [{category}] {message}";

    private static IEnumerable<string> CollectInfo(Exception e)
    {
        yield return $"Source:{e.Source}";
        yield return $"Message: {e.Message}";
        yield return $"Stack: {e.StackTrace}";
    }

    #endregion
}