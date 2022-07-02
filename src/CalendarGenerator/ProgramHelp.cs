using System.Reflection;
using System.Text;

namespace CalendarGenerator;

public static class ProgramHelp
{
    public static void Show() => Console.WriteLine(Content());

    private static string Content() =>
        new StringBuilder()
            .AppendLine("Program usage:")
            .AppendLine($"{Assembly.GetExecutingAssembly().GetName().Name} -o calendar.html -y 2020")
            .AppendLine()
            .AppendLine("Options:")
            .AppendLine("  -h              Show help")
            .AppendLine("  -o  [Required]  Name of the output file")
            .AppendLine("  -y  [Required]  Year of the calendar")
            .ToString();
}