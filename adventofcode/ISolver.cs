using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace adventofcode
{
    public interface ISolver
    {
        ILogger<ISolver> logger { get; set; }
        
        string InFile { get; }

        Task Init(string[] data);

        Task Run();
    }
}
