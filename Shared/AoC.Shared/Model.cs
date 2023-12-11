﻿namespace AoC.Shared;
public record SolutionResult(string Result) { }

public interface IPrinter
{
    void Flush();
    void Print(string s);

    void PrintMatrix<T>(T[,] matrix);
    void PrintMatrixXY<T>(T[,] matrix);
    void PrintMatrix(List<(int x, int y)> coords);
}

public class DebugPrinter : IPrinter
{
    public void Flush() { }

    public void Print(string s)
    {
        Debug.WriteLine(s);
    }

    public void PrintMatrix<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Debug.Write(matrix[row, col]?.ToString() ?? ".");
            }
            Debug.WriteLine("");
        }
    }

    public void PrintMatrix(List<(int x, int y)> coords)
    {
        var maxX = coords.Max(c => c.x);
        var maxY = coords.Max(c => c.y);
        var minX = coords.Min(c => c.x);
        var minY = coords.Min(c => c.y);
        for (int i = 0; i <= maxY - minY; i++)
        {
            for (int j = 0; j <= maxX - minX; j++)
            {
                if (coords.Contains((j + minX, i + minY)))
                {
                    Debug.Write("#");
                }
                else
                {
                    Debug.Write(".");
                }
            }
            Debug.WriteLine("");
        }
    }

    public void PrintMatrixXY<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(1); row++)
        {
            for (int col = 0; col < matrix.GetLength(0); col++)
            {
                Debug.Write(matrix[col, row]?.ToString() ?? ".");
            }
            Debug.WriteLine("");
        }
    }

}

public class Printer : IPrinter
{
    public void Flush() { }

    public void Print(string s)
    {
        Console.WriteLine(s);
    }

    public void PrintMatrix<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Console.Write(matrix[row, col]);
            }
            Console.WriteLine();
        }
    }

    public void PrintMatrix(List<(int x, int y)> coords)
    {
        var maxX = coords.Max(c => c.x);
        var maxY = coords.Max(c => c.y);
        var minX = coords.Min(c => c.x);
        var minY = coords.Min(c => c.y);
        for (int i = 0; i <= maxY - minY; i++)
        {
            for (int j = 0; j <= maxX - minX; j++)
            {
                if (coords.Contains((j + minX, i + minY)))
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }
    }

    public void PrintMatrixXY<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(1); row++)
        {
            for (int col = 0; col < matrix.GetLength(0); col++)
            {
                Console.Write(matrix[col, row]);
            }
            Console.WriteLine();
        }
    }

}

public class TestPrinter(ITestOutputHelper output) : IPrinter
{
    private readonly ITestOutputHelper _output = output;
    private readonly StringBuilder _sb = new();

    public void Print(string s)
    {
        _sb.Append(s);
    }

    public void Flush()
    {
        Debug.WriteLine(_sb.ToString());
        _output.WriteLine(_sb.ToString());
        _sb.Clear();
    }

    public void PrintMatrix<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                _sb.Append(matrix[row, col]?.ToString() ?? ".");
            }
            _sb.AppendLine();
        }
    }

    public void PrintMatrixXY<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(1); row++)
        {
            for (int col = 0; col < matrix.GetLength(0); col++)
            {
                _sb.Append(matrix[col, row]?.ToString() ?? ".");
            }
            _sb.AppendLine();
        }
    }

    public void PrintMatrix(List<(int x, int y)> coords)
    {
        var maxX = coords.Max(c => c.x);
        var maxY = coords.Max(c => c.y);
        var minX = coords.Min(c => c.x);
        var minY = coords.Min(c => c.y);
        for(int i = 0;i <= maxY - minY; i++)
        {
            for(int j = 0; j <= maxX - minX; j++)
            {
                if (coords.Contains((j + minX, i + minY)))
                {
                    _sb.Append('#');
                }
                else
                {
                    _sb.Append('.');
                }
            }
            _sb.AppendLine();
        }
    }
}

public class Solveable(string filename, string name, int day = 0) : Attribute
{
    public string Filename { get; private set; } = filename;
    public string Name { get; private set; } = name;
    public int Day { get; private set; } = day;
}

public class PriorityQueue<T> where T : IComparable<T>
{
    private readonly List<T> _data = [];

    public void Enqueue(T item)
    {
        _data.Add(item);
        var currentIndex = _data.Count - 1;

        while (currentIndex > 0)
        {
            var parentIndex = (currentIndex - 1) / 2;

            if (_data[currentIndex].CompareTo(_data[parentIndex]) >= 0)
            {
                break;
            }
            var tmp = _data[currentIndex];
            _data[currentIndex] = _data[parentIndex];
            _data[parentIndex] = tmp;
            currentIndex = parentIndex;
        }
    }

    public T Dequeue()
    {
        int lastIndex = _data.Count - 1;
        var frontItem = _data[0];
        _data[0] = _data[lastIndex];
        _data.RemoveAt(lastIndex);

        --lastIndex;
        var parentIndex = 0;

        while (true)
        {
            var leftChildIndex = parentIndex * 2 + 1;
            if (leftChildIndex > lastIndex)
            {
                break;
            }

            var rightChildIndex = leftChildIndex + 1;
            if (rightChildIndex <= lastIndex && _data[rightChildIndex].CompareTo(_data[leftChildIndex]) < 0)
            {
                leftChildIndex = rightChildIndex;
            }

            if (_data[parentIndex].CompareTo(_data[leftChildIndex]) <= 0)
            {
                break; 
            }

            var tmp = _data[parentIndex];
            _data[parentIndex] = _data[leftChildIndex];
            _data[leftChildIndex] = tmp;
            parentIndex = leftChildIndex;
        }

        return frontItem;
    }

    public int Count()
    {
        return _data.Count;
    }
}
