using Minio;
using System;

namespace MinIOSharp.Core.Config
{
    public class MinIOSetting
    {
        public string Endpoint { get; set; }
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string SessionToken { get; set; } = string.Empty;
        public string BucketName { get; set; } = string.Empty;
        public Action<MinioClient>? Configure { get; private set; }
        public void ConfigureClient(Action<MinioClient> configure)
        {
            Configure = configure;
        }
    }
}