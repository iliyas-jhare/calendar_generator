namespace CalendarGenerator;

public class FileWriter
{
    private string OutputDirectory { get; }
    private const string OutputFileExtension = ".html";

    public FileWriter()
    {
        OutputDirectory = Directory.GetCurrentDirectory();
    }

    public bool Write(string name, string contents)
    {
        if (string.IsNullOrEmpty(name))
        {
            Log.Error("File name could not be null or empty.");
            return false;
        }

        var path = CreateFilePath(name);
        File.WriteAllText(path, contents);
        Log.Info($"File created. {path}");
        return true;
    }

    #region Detail

    private string CreateFilePath(string name)
    {
        var path = name.EndsWith(OutputFileExtension)
            ? Path.Combine(OutputDirectory, name)
            : Path.Combine(OutputDirectory, $"{name}{OutputFileExtension}");

        if (!File.Exists(path)) return path;

        Log.Info($"File exists. {path}");
        Log.Info("Would you like to replace it? (Y/N)");
        switch (Console.ReadLine())
        {
            case "Y":
            case "y":
                File.Delete(path);
                break;
            default:
                Log.Info("Please provide different name.");
                path = CreateFilePath(Console.ReadLine());
                break;
        }

        return path;
    }

    #endregion
}