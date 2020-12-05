using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace adventofcode.Code
{
    public class Day5 : ISolver
    {
        private IEnumerable<(int row, int colum)> instructions;
        public ILogger<ISolver> Logger { get; set; }
        public string InFile { get; } = "boarding-5";

        public async Task Init(string data)
        {
            instructions = data.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x =>
            {
                var row = new string(x.Substring(0, 7).Select(y => y switch
                {
                    'F' => '0',
                    'B' => '1',
                    _ => '-',
                }).ToArray());
                var colum = new string(x.Substring(7, 3).Select(y => y switch
                {
                    'L' => '0',
                    'R' => '1',
                    _ => '-',
                }).ToArray());

                return (Convert.ToInt32(row, 2), Convert.ToInt32(colum, 2));
                //return (row, colum);
            });

            //Console.WriteLine($"[{string.Join(",", instructions.Select(x => $"[[{string.Join(",", x.row)}], [{string.Join(",", x.colum)}]]"))}]");
        }

        public async Task Run()
        {
            const int rows = 127;
            const int colums = 8;

            var seatIds = instructions.Select(x => x.row * 8 + x.colum).OrderBy(x=> x).ToHashSet();
            var min = seatIds.Min();
            var max = seatIds.Max();

            var seatAll = Enumerable.Range(min, max - min).ToHashSet();

            var removeWhere = seatAll.RemoveWhere(x=> seatIds.Contains(x));
            
            Logger.Log(LogLevel.Information, $"Hightest seat: {max}");
            Logger.Log(LogLevel.Information, $"My seat: {string.Join(",", seatAll)}");
        }
    }
}
