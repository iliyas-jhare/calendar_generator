namespace HtmlCalendarGenerator;

public record CalendarYear(string Name, IReadOnlyList<CalendarMonth> Months);