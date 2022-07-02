namespace CalendarGenerator;

public record CalendarDay(int Number, int DayOfWeek)
{
    public string Name => Number > 0 ? Number.ToString() : string.Empty;
}