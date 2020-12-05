using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace adventofcode
{
    public interface ISolver
    {
        ILogger<ISolver> Logger { get; set; }
        
        string InFile { get; }

        Task Init(string data);

        Task Run();
    }
}
