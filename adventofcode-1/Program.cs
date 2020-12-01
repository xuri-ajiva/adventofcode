using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var numbers = (await File.ReadAllLinesAsync("numbers-1")).Select(int.Parse).ToArray();
const int magic = 2020;
foreach (var i in numbers)
{
    foreach (var j in numbers)
    {
        if (i + j == magic)
            Console.WriteLine($"{i} * {j} = {i * j}");

        foreach (var k in numbers)
        {
            if (i + j + k == magic)
                Console.WriteLine($"{i} * {j} * {k} = {i * j * k}");
        }
    }
}
