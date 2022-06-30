
using HtmlCalendarGenerator;

ExceptionPolicy.WithinGeneralException(() =>
{
    var programArgs = new ProgramArgs();
    var options = programArgs.Parse(args);
    if (programArgs.RequiredOptionsExist(options))
    {
        // TODO
        Log.Info("In progress...");
    }
    else
    {
        ProgramHelp.Show();
    }
});
