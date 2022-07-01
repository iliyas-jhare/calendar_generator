namespace HtmlCalendarGenerator;

public record CalendarWeek(string Name, IReadOnlyList<CalendarDay> Days);