using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace adventofcode.Code
{
    public class Day6 : ISolver
    {
        private ImmutableArray<string> groups;
        public ILogger<ISolver> Logger { get; set; }
        public string InFile { get; } = "custom-6";

        public async Task Init(string data)
        {
            groups = data.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Replace(Environment.NewLine, ",")).ToImmutableArray();
        }

        public async Task Run()
        {
            int everyone = 0, anyone = 0;
            foreach (var group in groups)
            {
                var answers = group.Split(',', StringSplitOptions.RemoveEmptyEntries);
                var allPossibly = group.Replace(",", "").Distinct().ToHashSet();

                anyone += allPossibly.Count;



                allPossibly.RemoveWhere(x => !answers.All(y => y.Contains(x)));
                //foreach (var c in allPossibly.Where(c => !answers.All(y => y.Contains(c)))) {allPossibly.Remove(c);}


                everyone += allPossibly.Count;
            }


            Logger.Log(LogLevel.Information, $"Sum of Counts for anyone: {anyone}");
            Logger.Log(LogLevel.Information, $"Sum of Counts for everyone: {everyone}");
        }
    }
}
