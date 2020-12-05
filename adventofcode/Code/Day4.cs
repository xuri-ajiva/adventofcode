using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace adventofcode.Code
{
    public class Day4 : ISolver
    {
        public ILogger<ISolver> Logger { get; set; }

        public string InFile { get; } = "passport-4";


        private List<List<string>> passport = new();
        private Dictionary<string, string>[] ppv;

        public async Task Init(string data)
        {
            var pp = data.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Replace("\r\n", " "))
                .ToArray();

            ppv = pp.Select(x =>
                x.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => i.Split(":", StringSplitOptions.RemoveEmptyEntries))
                    .ToDictionary(k => k[0], v => v[1])).ToArray();
        }


        public async Task Run()
        {
            var heightRex = new Regex(@"(\d+)(cm|in)");
            var colorRex = new Regex(@"#[a-f0-9]{6}");

            string[] eyeColor = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

            bool heightTest(string obj)
            {
                var march = heightRex.Match(obj);

                if (march.Groups.Count != 3) return false;

                if (!int.TryParse(march.Groups[1].ToString(), out var height))
                {
                }

                return march.Groups[2].ToString() switch
                {
                    "cm" => 150 <= height && height <= 193,
                    "in" => 59 <= height && height <= 76,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            (string value, Predicate<string> predicate)[] keys =
            {
                ("byr", x => x.Length == 4 && int.TryParse(x, out var year) && 1920 <= year && year <= 2002),
                ("iyr", x => x.Length == 4 && int.TryParse(x, out var year) && 2010 <= year && year <= 2020),
                ("eyr", x => x.Length == 4 && int.TryParse(x, out var year) && 2020 <= year && year <= 2030),
                ("hgt", heightTest),
                ("hcl", x => colorRex.IsMatch(x)),
                ("ecl", x => eyeColor.Contains(x)),
                ("pid", x => x.Length == 9 && int.TryParse(x, out _))
            };
            /*var count1 = 0;
            var count2 = 0;
            foreach (var variable in ppv)
            {
                int coun1t = 0;
                int coun2t = 0;
                foreach (var x in keys)
                {
                    if (variable.ContainsKey(x.value))
                    {
                        coun1t++;
                        var val = variable[x.value];
                        if (x.predicate.Invoke(val))
                        {
                            coun2t++;
                        }
                    }
                }

                if (coun1t == keys.Length)
                {
                    count1++;
                }

                if (coun2t  == keys.Length)
                {
                    count2++;
                }
            }*/

            Logger.Log(LogLevel.Information,
                $"Found {ppv.Count(variable => keys.Count(x => variable.ContainsKey(x.value)) == keys.Length)} passport with all {keys.Length} keys");
            Logger.Log(LogLevel.Information,
                $"Found {ppv.Count(variable => keys.Count(x => variable.ContainsKey(x.value) && x.predicate.Invoke(variable[x.value])) == keys.Length)} passport with all {keys.Length} keys and valid values");
        }
    }
}
