using System.Text;

namespace CalendarGenerator;

public class HtmlWriter
{
    private const string BeigeColor = "beige";
    private const string WhiteSmokeColor = "whitesmoke";
    private const string PaddingStyle = "style=\"padding:6\"";
    private const string MarginStyle = "style=\"margin:10\"";

    private IReadOnlyList<string> CalendarColumnHeaders { get; }

    public HtmlWriter()
    {
        CalendarColumnHeaders = new[] { "Week", "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su" };
    }

    public string CreateCalendar<T>(T t) =>
        new StringBuilder()
            .AppendLine("<html>")
            .AppendLine("<body>")
            .AppendLine(t switch
            {
                CalendarMonth month => CreateTable(month),
                CalendarYear year => CreateTables(year),
                IReadOnlyList<CalendarMonth> months => CreateTables(months),
                IReadOnlyList<CalendarYear> years => CreateTables(years),
                _ => throw new InvalidOperationException("")
            })
            .AppendLine("</body>")
            .AppendLine("</html>")
            .ToString();

    #region Detail

    private string CreateTables(IEnumerable<CalendarYear> years) =>
        new StringBuilder()
            .AppendJoin(Environment.NewLine, years.Select(CreateTables))
            .ToString();

    private string CreateTables(CalendarYear year) =>
        new StringBuilder()
            .AppendLine($"<h1 {MarginStyle}>{year.Name}</h1>")
            .AppendJoin(Environment.NewLine, year.Months.Select(CreateTable))
            .ToString();

    private string CreateTables(IEnumerable<CalendarMonth> months) =>
        new StringBuilder()
            .AppendJoin(Environment.NewLine, months.Select(CreateTable))
            .ToString();

    private string CreateTable(CalendarMonth month) =>
        new StringBuilder()
            .AppendLine($"<table {MarginStyle} border=\"1\">")
            .AppendLine(CreateTableHeaderRow(month.Name, BeigeColor, 8))
            .AppendLine(CreateTableHeadersRow(CalendarColumnHeaders, WhiteSmokeColor))
            .AppendJoin(Environment.NewLine, month.Weeks.Select(w => CreateTableRow(w, WhiteSmokeColor)))
            .AppendLine("</table>")
            .ToString();

    private static string CreateTableRow(CalendarWeek week, string color, int colspan = 0) =>
        week.Days.Any()
            ? new StringBuilder()
                .AppendLine("<tr>")
                .AppendLine(CreateTableHeader(week.Number.ToString(), color))
                .AppendJoin(Environment.NewLine, week.Days.Select(CreateTableData))
                .AppendLine("</tr>")
                .ToString()
            : CreateTableHeader(week.Number.ToString(), color, colspan);

    private static string CreateTableHeaderRow(string name, string color, int colspan) =>
        new StringBuilder()
            .AppendLine("<tr>")
            .AppendLine(CreateTableHeader(name, color, colspan))
            .AppendLine("</tr>")
            .ToString();

    private static string CreateTableHeadersRow(IEnumerable<string> names, string color) =>
        new StringBuilder()
            .AppendLine("<tr>")
            .AppendJoin(Environment.NewLine, names.Select(n => CreateTableHeader(n, color)))
            .AppendLine("</tr>")
            .ToString();

    private static string CreateTableHeader(string name, string color, int colspan = 0) =>
        colspan > 0
            ? new StringBuilder()
                .AppendJoin(" ", "<th", PaddingStyle, $"bgcolor=\"{color}\"", $"colspan=\"{colspan}\">", name, "</th>")
                .ToString()
            : new StringBuilder()
                .AppendJoin(" ", "<th", PaddingStyle, $"bgcolor=\"{color}\">", name, "</th>")
                .ToString();

    private static string CreateTableData(CalendarDay day) =>
        $"<td {PaddingStyle} align=\"center\">{day.Name}</td>";

    #endregion
}