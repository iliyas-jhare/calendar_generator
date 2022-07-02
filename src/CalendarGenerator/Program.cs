using CalendarGenerator;

ExceptionPolicy.WithinGeneralException(() =>
{
    Log.Info("Started.");

    var optionsFactory = new OptionsFactory();
    var options = optionsFactory.CreateOptions(args).ToArray();
    if (optionsFactory.HelpRequested(options))
    {
        ProgramHelp.Show();
        return;
    }

    if (!optionsFactory.RequiredOptionsExist(options))
    {
        Log.Error("Insufficient number of arguments were passed.");
        ProgramHelp.Show();
        return;
    }

    var calendarDaysFactory = new CalendarDayFactory();
    var calendarYearFactory = new CalendarYearFactory(calendarDaysFactory);
    var calendarYears =
        calendarYearFactory.CreateCalendarYears(options.Single(o => o.Is(Constants.YearOption)));
    if (!calendarYears.Any())
    {
        Log.Error("Creating calendar years has failed.");
        return;
    }
    Log.Info("Calendar years created.");

    var htmlWriter = new HtmlWriter();
    var htmlContents = htmlWriter.CreateCalendar(calendarYears);
    if (string.IsNullOrEmpty(htmlContents))
    {
        Log.Error("Creating calendar years HTML has failed.");
        return;
    }
    Log.Info("Calendar years HTML created.");

    var fileWriter = new FileWriter();
    if (!fileWriter.Write(options.Single(o => o.Is(Constants.OutputOption)).Value, htmlContents))
    {
        Log.Error("Writing calendar years HTML has failed.");
        return;
    }
    Log.Info("Calendar years HTML written.");

    Log.Info("Finished.");
});
