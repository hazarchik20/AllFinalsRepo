using FinalProjectASP_Net.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectASP_Net.Controllers
{
    [ApiController]
    [Route("api/AWSControllers")]
    public class AWSControllers : ControllerBase
    {
        private readonly S3Services _s3Service;

        public AWSControllers(S3Services s3Service)
        {
            _s3Service = s3Service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            await _s3Service.UploadFileAsync(file);

            return Ok("File uploaded to S3");
        }
    }
}
