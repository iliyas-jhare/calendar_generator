namespace CalendarGenerator;

public static class EnumerableEx
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> act)
    {
        if (act is null) return;
        foreach (var item in source)
        {
            act(item);
        }
    }
}