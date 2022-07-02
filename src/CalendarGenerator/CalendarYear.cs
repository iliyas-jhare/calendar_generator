namespace CalendarGenerator;

public record CalendarYear(int Number, IReadOnlyList<CalendarMonth> Months)
{
    public string Name => Number > 0 ? $"Year {Number}" : string.Empty;
}