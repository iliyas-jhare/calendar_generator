namespace HtmlCalendarGenerator;

public record CalendarMonth(string Name, IReadOnlyList<CalendarWeek> Weeks);