using MinIOSharpSharp.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MinIOSharpSharp.Core.Services
{
    public interface IMinIOSharpService
    {
        MinIOSharpVerificationResult VerifyMinIOSharpResponse(string response, CancellationToken cancellationToken);
        Task<MinIOSharpVerificationResult> VerifyMinIOSharpResponseAsync(string response, CancellationToken cancellationToken);
    }
}
