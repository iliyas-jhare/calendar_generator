namespace HtmlCalendarGenerator;

public static class ExceptionPolicy
{
    public static void WithinGeneralException(Action act)
    {
        try
        {
            act();
        }
        catch (Exception e)
        {
            Log.Exception(e);
        }
    }
}