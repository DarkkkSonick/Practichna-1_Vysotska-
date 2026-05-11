using System.Text;

public class PortLogger
{
    private StringBuilder logBuilder = new StringBuilder();

    public void LogOperation(string operation, int portNumber, string details)
    {
        logBuilder.AppendLine(
            $"[{DateTime.Now}] Port {portNumber} | {operation} | {details}"
        );
    }

    public string GetFullLog()
    {
        return logBuilder.ToString();
    }

    public void SaveLogToFile()
    {
        File.WriteAllText("portlog.txt", logBuilder.ToString());
    }
}