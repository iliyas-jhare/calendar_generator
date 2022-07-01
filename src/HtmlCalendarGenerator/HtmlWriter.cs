using System.Text;

namespace HtmlCalendarGenerator;

public class HtmlWriter
{
    private const string BeigeColor = "beige";
    private const string WhiteSmokeColor = "whitesmoke";
    private const string CellStyle = "style=\"padding:3\"";
    private const string TableStyle = "style=\"margin: 5; float: left\"";

    private CalendarWeek WeekDayNames { get; }

    public HtmlWriter()
    {
        WeekDayNames = new("Week", new CalendarDay[]
        {
            new("Mo"),
            new("Tu"),
            new("We"),
            new("Th"),
            new("Fr"),
            new("Sa"),
            new("Su")
        });
    }

    public string CreateTables(CalendarYear year) =>
        new StringBuilder()
            .AppendLine($"<h1>Year {year.Name} Calendar</h1>")
            .AppendJoin(Environment.NewLine, year.Months.Select(CreateTable))
            .ToString();

    public string CreateTable(CalendarMonth month) =>
        new StringBuilder()
            .AppendLine($"<table {TableStyle} border=\"1\">")
            .AppendLine(CreateTableRow(new(month.Name, Array.Empty<CalendarDay>()), BeigeColor, default, 8))
            .AppendLine(CreateTableRow(WeekDayNames, WhiteSmokeColor, true))
            .AppendJoin(Environment.NewLine, month.Weeks.Select(w => CreateTableRow(w, WhiteSmokeColor)))
            .AppendLine("</table>")
            .ToString();

    private static string CreateTableRow(CalendarWeek week, string color, bool headers = default, int colspan = 0) =>
        week.Days.Any()
            ? new StringBuilder()
                .AppendLine("<tr>")
                .AppendLine(CreateTableHeader(week.Name, color))
                .AppendJoin(Environment.NewLine,
                    week.Days.Select(d => headers ? CreateTableHeader(d.Name, color) : CreateTableData(d)))
                .AppendLine("</tr>")
                .ToString()
            : new StringBuilder()
                .AppendLine("<tr>")
                .AppendLine(CreateTableHeader(week.Name, color, colspan))
                .AppendLine("</tr>")
                .ToString();

    private static string CreateTableHeader(string name, string color, int colspan = 0) =>
        colspan > 0
            ? new StringBuilder()
                .AppendJoin(" ", "<th", CellStyle, $"bgcolor=\"{color}\"", $"colspan=\"{colspan}\">", name, "</th>")
                .ToString()
            : new StringBuilder()
                .AppendJoin(" ", "<th", CellStyle, $"bgcolor=\"{color}\">", name, "</th>")
                .ToString();

    private static string CreateTableData(CalendarDay day) =>
        $"<td {CellStyle} align=\"center\">{day.Name}</td>";
}