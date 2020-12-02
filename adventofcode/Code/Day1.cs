using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace adventofcode.Code
{
    public class Day1 : ISolver
    {
        private int[] numbers;
        private const int Magic = 2020;

        public ILogger<ISolver> logger { get; set; }

        public string InFile { get; } = "numbers-1";


        public async Task Init(string[] data)
        {
            numbers = (data).Select(int.Parse).ToArray();
        }

        public async Task Run()
        {
            List<int> fount = new();

            foreach (var i in numbers)
            {
                foreach (var j in numbers)
                {
                    foreach (var k in numbers)
                    {
                        if (i + j + k != Magic) continue;
                        if (fount.Contains(i * j * k)) continue;

                        fount.Add(i * j * k);
                        logger.Log(LogLevel.Information, $"{i} * {j} * {k} = {i * j * k}");
                    }

                    if (i + j != Magic) continue;
                    if (fount.Contains(i * j)) continue;

                    fount.Add(i * j);
                    logger.Log(LogLevel.Information, $"{i} * {j} = {i * j}");
                }
            }
        }
    }
}
