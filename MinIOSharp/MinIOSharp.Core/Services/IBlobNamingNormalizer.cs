using System.IO;
using System.Threading.Tasks;

namespace MinIOSharp.Core.Services
{
    public interface IBlobNamingNormalizer
    {
        string NormalizeContainerName(string containerName);
        string NormalizeBlobName(string blobName, string folderStruct = "");
    }
}
