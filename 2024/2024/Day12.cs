﻿namespace AoC2024;
public class Day12
{
    public static char[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new char[lines.Length, lines.First().Length];
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                result[row, col] = lines[col][row];
            }
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day12.txt", "Day 12 part 1", 12)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var plots = ParseInput(filename);
        var result = 0L;
        var regions = FindAllRegions(plots);
        foreach (var region in regions)
        {
            result += region.region.Count * region.perimeter;
        }

        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day12.txt", "Day 12 part 2", 12)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var plots = ParseInput(filename);
        var result = 0L;
        var regions = FindAllRegions(plots);
        return new SolutionResult(result.ToString());
    }

    private static void FloodFill(char[,] matrix, bool[,] visited, int x, int y, ref char target, List<(int x, int y)> region, ref int perimeter)
    {
        var cols = matrix.GetLength(0);
        var rows = matrix.GetLength(1);
        if (x < 0 || x >= cols || y < 0 || y >= rows || visited[x, y] || matrix[x, y] != target)
        {
            return;
        }

        visited[x, y] = true;
        region.Add((x, y));

        if (x + 1 >= cols || matrix[x + 1, y] != target)
        {
            perimeter++;
        }
        if (x - 1 < 0 || matrix[x - 1, y] != target)
        {
            perimeter++;
        }
        if (y + 1 >= rows || matrix[x, y + 1] != target)
        {
            perimeter++;
        }
        if (y - 1 < 0 || matrix[x, y - 1] != target)
        {
            perimeter++;
        }

        FloodFill(matrix, visited, x + 1, y, ref target, region, ref perimeter);
        FloodFill(matrix, visited, x - 1, y, ref target, region, ref perimeter);
        FloodFill(matrix, visited, x, y + 1, ref target, region, ref perimeter);
        FloodFill(matrix, visited, x, y - 1, ref target, region, ref perimeter);
    }

    private static List<(List<(int x, int y)> region, int perimeter, char target)> FindAllRegions(char[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        bool[,] visited = new bool[rows, cols];
        var regions = new List<(List<(int x, int y)> region, int perimeter, char target)>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (!visited[i, j])
                {
                    var region = new List<(int x, int y)>();
                    var perimeter = 0;
                    var target = matrix[i, j];

                    FloodFill(matrix, visited, i, j, ref target, region, ref perimeter);
                    if (region.Count > 0)
                    {
                        regions.Add((region, perimeter, target));
                    }
                }
            }
        }

        return regions;
    }

}
