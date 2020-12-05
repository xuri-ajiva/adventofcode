using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

#pragma warning disable 1998

namespace adventofcode.Code
{
    public class Day3 : ISolver
    {
        public ILogger<ISolver> Logger { get; set; }
        public string InFile { get; } = "toboggan-3";


        private bool[][] trees;

        public async Task Init(string data)
        {
            trees = data.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Select(y => y == '#').ToArray()).ToArray();
        }


        public async Task Run()
        {
            {
                var ln = trees[0].Length;

                int[] right = {1, 3, 5, 7, 1};
                int[] down = {1, 1, 1, 1, 2};

                long count_all = 1;

                for (var p = 0; p < right.Length; p++)
                {
                    var count = 0;
                    for (int i = 0, px = 0; i < trees.Length; i += down[p], px++)
                    {
                        var x = (right[p] * px) % ln;
                        if (trees[i][x])
                            count++;
                    }

                    Logger.Log(LogLevel.Debug, $"Found {count} trees for {p} ({right[p]},{down[p]})");
                    count_all *= count;
                }

                Logger.Log(LogLevel.Information, $"Found {count_all} trees by multiplying");
            }
        }
    }
}
