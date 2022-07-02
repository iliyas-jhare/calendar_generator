using CalendarGenerator;

ExceptionPolicy.WithinGeneralException(() =>
{
    var optionsFactory = new OptionsFactory();
    var options = optionsFactory.CreateOptions(args);
    if (optionsFactory.RequiredOptionsExist(options))
    {
        Log.Info("In progress...");
    }
    else
    {
        Log.Error("Insufficient number of arguments were passed.");
        ProgramHelp.Show();
    }
});