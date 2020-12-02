using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
#pragma warning disable 1998

namespace adventofcode.Code
{
    public class Day2 : ISolver
    {
        public struct LayOut
        {
            public static LayOut FromString(string str)
            {
                var r = new Regex(@"(\d+)-(\d+) (\w): (\w+)", RegexOptions.IgnoreCase);

                var march = r.Match(str);


                //var splitDash = str.Split('-');
                return new()
                {
                    Min = int.Parse(march.Groups[1].Value),
                    Max = int.Parse(march.Groups[2].Value),
                    Philosophy = march.Groups[3].Value[0],
                    Password = march.Groups[4].Value,
                    //Min = int.Parse(splitDash[0]),
                    //Max = int.Parse(splitDash[1].Split(' ')[0]),
                    //Philosophy = str[str.IndexOf(' ') + 1],
                    //Password = str.Split(':')[1].Substring(1),
                };
            }

            public char Philosophy;
            public int Min;
            public int Max;

            public string Password;
        }


        public ILogger<ISolver> Logger { get; set; }
        public string InFile { get; } = "password-philosophy-2";

        public async Task Init(string[] data)
        {
            LayOuts = data.Select(LayOut.FromString).ToArray();
        }

        public LayOut[] LayOuts { get; set; }

        public async Task Run()
        {
            var valid = 0;
            foreach (var layOut in LayOuts)
            {
                var ct = layOut.Password.Count(x => x == layOut.Philosophy);
                if (ct < layOut.Min || ct > layOut.Max)
                {
                    //logger.Log(LogLevel.Debug, $"{layOut.Password} has only {ct} '{layOut.Philosophy}' expected from {layOut.Min} to {layOut.Max}!");
                }
                else
                {
                    valid++;
                }
            }

            Logger.Log(LogLevel.Information, $"Found: {valid} marches in range of min max");


            var march = 0;
            foreach (var layOut in LayOuts)
            {
                var pos1 = layOut.Password[layOut.Min - 1];
                var pos2 = layOut.Password[layOut.Max - 1];

                if ((pos1 == layOut.Philosophy && pos2 != layOut.Philosophy) ||
                    (pos2 == layOut.Philosophy && pos1 != layOut.Philosophy))
                {
                    march++;
                    // logger.Log(LogLevel.Debug, $"'{pos1}' or '{pos2}' is '{layOut.Philosophy}'");
                }
            }

            Logger.Log(LogLevel.Information, $"Found: {march} marches with min ^ max char marching");
        }
    }
}
