﻿namespace AoC2024.Tests;
public class Day17Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";

        //When
        var (computer, program) = Day17.ParseInput(filename);

        //Then
        Assert.Equal((ulong)729, computer.A);
        Assert.Equal((ulong)0, computer.B);
        Assert.Equal((ulong)0, computer.C);
        Assert.Equal(6, program.Count());
        Assert.Equal(0, program.First());
        Assert.Equal(5, program[2]);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";

        //When
        var result = Day17.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("4,6,3,5,6,3,5,2,1,0", result.Result);

        //Given
        filename = $"{Helpers.DirectoryPathTests}Day17-test3.txt";

        //When
        result = Day17.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("6,0,4,5,4,5,2,0", result.Result);
    }

    [Fact]
    public void Can_solve_part1_for_test2()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day17-test4.txt";

        //When
        var result = Day17.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("6,0,4,5,4,5,2,0", result.Result);

        //Given
        filename = $"{Helpers.DirectoryPathTests}Day17-test5.txt";

        //When
        result = Day17.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("3,4,4,1,7,0,2,2", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        ////Given
        var filename = $"{Helpers.DirectoryPathTests}Day17-test2.txt";

        //When
        var result = Day17.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("117440", result.Result);

    }


    [Fact]
    public void Can_solve_part2_for_test2()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day17-test5.txt";

        //When
        var result = Day17.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("266926175730705", result.Result);

        ////Given
        filename = $"{Helpers.DirectoryPathTests}Day17-test4.txt";

        ////When
        result = Day17.Part2(filename, new TestPrinter(output));

        ////Then
        Assert.Equal("202797954918051", result.Result);
    }

    [Fact]
    public void Can_run_programs()
    {
        var c = new Computer(0, 0, 9);
        var program = new List<int> { 2, 6 };
        var result = Day17.RunProgram(c, program);
        Assert.Equal((ulong)1, c.B);

        c = new Computer(10, 0, 0);
        program = new List<int> { 5, 0, 5, 1, 5, 4 };
        result = Day17.RunProgram(c, program);
        Assert.Equal([0, 1, 2], result);

        c = new Computer(2024, 0, 0);
        program = new List<int> { 0, 1, 5, 4, 3, 0 };
        result = Day17.RunProgram(c, program);
        Assert.Equal([4, 2, 5, 6, 7, 7, 7, 7, 3, 1, 0], result);
        Assert.Equal((ulong)0, c.A);

        c = new Computer(0, 29, 0);
        program = new List<int> { 1, 7 };
        result = Day17.RunProgram(c, program);
        Assert.Equal((ulong)26, c.B);

        var x = 2024 ^ 43690;
        Assert.Equal(44354, x);
        c = new Computer(0, 2024, 43690);
        program = new List<int> { 4, 0 };
        result = Day17.RunProgram(c, program);
        Assert.Equal((ulong)44354, c.B);

        c = new Computer(41644071, 0, 0);
        program = new List<int> { 2, 4, 1, 2, 7, 5, 1, 7, 4, 4, 0, 3, 5, 5, 3, 0 };
        result = Day17.RunProgram(c, program);
    }
}