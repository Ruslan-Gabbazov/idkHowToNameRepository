// See https://aka.ms/new-console-template for more information

var filePath = "..\\..\\..\\LogTextFile.txt";

var matrixLogger = new LocalFileLogger<Matrix>(filePath);
var vectorLogger = new LocalFileLogger<Vector>(filePath);

matrixLogger.LogInfo("LogInfo massage from Matrix type");
matrixLogger.LogWarning("LogWarning massage from Matrix type");
matrixLogger.LogError("LogError massage from Matrix type", new ArgumentException());

vectorLogger.LogInfo("LogInfo massage from Vector type");
vectorLogger.LogWarning("LogWarning massage from Vector type");
vectorLogger.LogError("LogError massage from Vector type", new ArgumentException());

internal interface ILogger
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message, Exception ex);
}

internal class LocalFileLogger<T> : ILogger
{
    private string FilePath { get; }
    private string GenericTypeName { get; } = typeof(T).Name;

    public LocalFileLogger(string filePath)
    {
        FilePath = filePath;
        File.WriteAllText(filePath, string.Empty);
    }

    public void LogInfo(string message)
    {
        File.AppendAllText(FilePath, $"[Info] : [{GenericTypeName}] : {message}\n");
    }

    public void LogWarning(string message)
    {
        File.AppendAllText(FilePath, $"[Warning] : [{GenericTypeName}] : {message}\n");
    }

    public void LogError(string message, Exception ex)
    {
        File.AppendAllText(FilePath, $"[Error] : [{GenericTypeName}] : {message}. {ex.Message}\n");
    }
}

public class Matrix
{
    public double[,]? Data;
    public int Rows;
    public int Cols;
}

public class Vector
{
    public double X;
    public double Y;
    public double Z;
    public double Length;
}