using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Application.Services
{
    public class S3Services
    {
        private readonly IAmazonS3 _s3;

        public S3Services(IAmazonS3 s3)
        {
            _s3 = s3;
        }

        public async Task UploadFileAsync(IFormFile file)
        {
            var fileTransferUtility = new TransferUtility(_s3);

            using var stream = file.OpenReadStream();

            await fileTransferUtility.UploadAsync(stream,
                "jobboard-hzr-files-2026",
                file.FileName);
        }
    }
}
