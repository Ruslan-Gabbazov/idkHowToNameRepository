// See https://aka.ms/new-console-template for more information

var filePath = "..\\..\\..\\LogTextFile.txt";

var matrixLogger = new LocalFileLogger<Matrix>(filePath);
var vectorLogger = new LocalFileLogger<Vector>(filePath);

matrixLogger.LogInfo("LogInfo massage from Matrix class");
matrixLogger.LogWarning("LogWarning massage from Matrix class");
matrixLogger.LogError("LogError massage from Matrix class", new ArgumentException());

vectorLogger.LogInfo("LogInfo massage from Vector class");
vectorLogger.LogWarning("LogWarning massage from Vector class");
vectorLogger.LogError("LogError massage from Vector class", new ArgumentException());

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
        File.WriteAllText(filePath, String.Empty);
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

internal class Matrix
{
    private readonly double[,] _matrix;
    private int Rows { get; }
    private int Cols { get; }

    public Matrix(int rows, int cols, int initialValue = 0)
    {
        Rows = rows;
        Cols = cols;
        var matrix = new double[rows, cols];

        for (var i = 0; i < rows; i++)
            for (var j = 0; j < cols; j++)
                matrix[i, j] = initialValue;

        _matrix = matrix;
    }
}

public class Vector
{
    private double X;
    private double Y;
    private double Z;

    public Vector(double x = 0.0, double y = 0.0, double z = 0.0)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vector(double[] xyz)
    {
        if (xyz.Length != 3)
        {
            string msg = "Vector: cannot make a vector of non-3 numbers";
            Console.WriteLine(msg);
            throw new ArgumentException(msg);
        }
        else
        {
            X = xyz[0];
            Y = xyz[1];
            Z = xyz[2];
        }
    }
}