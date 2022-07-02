using CalendarGenerator;

ExceptionPolicy.WithinGeneralException(() =>
{
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
        Log.Error("Creating calendar has failed.");
        return;
    }

    var htmlWriter = new HtmlWriter();
    var htmlContents = htmlWriter.CreateCalendar(calendarYears);
    if (string.IsNullOrEmpty(htmlContents))
    {
        Log.Error("Generating calendar HTML has failed.");
        return;
    }

    var fileWriter = new FileWriter();
    if (!fileWriter.Write(options.Single(o => o.Is(Constants.OutputOption)).Value, htmlContents))
    {
        Log.Error("Writing calendar has failed.");
        return;
    }

    Log.Info("Writing calendar finished.");

});
