namespace CalendarGenerator;

public record CalendarDay(int Number)
{
    public string Name => Number > 0 ? Number.ToString() : string.Empty;
}