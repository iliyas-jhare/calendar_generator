namespace CalendarGenerator;

public record Option(string Name, string Value)
{
    public bool Is(string name) =>
        !string.IsNullOrEmpty(name) && Name == name;
}