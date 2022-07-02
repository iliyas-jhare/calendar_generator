using static CalendarGenerator.Constants;

namespace CalendarGenerator;

public class OptionsFactory
{
    private IEnumerable<string> AllOptions { get; }
    private IEnumerable<string> RequiredOptions { get; }

    public OptionsFactory()
    {
        AllOptions = new List<string>
        {
            OutputOption,
            YearOption,
            HelpOption
        };
        RequiredOptions = new List<string>
        {
            OutputOption,
            YearOption
        };
    }

    public IEnumerable<Option> CreateOptions(IReadOnlyList<string> args)
    {
        if (args is null) yield break;
        for (var i = 0; i < args.Count; i++)
        {
            var arg = args[i];
            if (string.IsNullOrEmpty(arg) || !IsAnOption(arg))
                continue;
            yield return CreateOption(arg, GetOptionValue(args, i));
        }

        Log.Info("Program options created.");
    }

    public bool RequiredOptionsExist(IEnumerable<Option> options)
        => RequiredOptions.All(target => options.Any(o => OptionExist(o, target)));

    public bool HelpRequested(IEnumerable<Option> options)
        => options.Any(o => o.Is(HelpOption));

    #region Detail

    private static bool OptionExist(Option option, string name)
        => option is not null &&
           !string.IsNullOrEmpty(option.Name) &&
           option.Is(name) &&
           !string.IsNullOrEmpty(option.Value);

    private bool IsAnOption(string name)
        => AllOptions.Any(s => s == name);

    private static Option CreateOption(string name, string value = default) =>
        new(name, value);

    private static string GetOptionValue(IReadOnlyList<string> args, int position)
        => position + 1 < args.Count ? args[position + 1] : default;

    #endregion
}