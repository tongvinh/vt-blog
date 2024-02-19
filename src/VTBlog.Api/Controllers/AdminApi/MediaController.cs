using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VTBlog.Core.ConfigOptions;

namespace VTBlog.Api.Controllers.AdminApi
{
    [Route("api/admin/media")]
    [ApiController]
    public class MediaController(IWebHostEnvironment hostingEnv, IOptions<MediaSettings> settings) : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnv = hostingEnv;
        private readonly MediaSettings _settings = settings.Value;

        [HttpPost]
        [AllowAnonymous]
        public IActionResult UploadImage(string type)
        {
            var allowImageTypes = _settings.AllowImageFileTypes?.Split(",");

            var now = DateTime.Now;
            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                return null;
            }

            var file = files[0];
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)?.FileName?.Trim('"');
            if (allowImageTypes?.Any(x =>fileName?.EndsWith(x,StringComparison.OrdinalIgnoreCase) == true) == false)
            {
                throw new Exception("Không cho phép tải lên file không phải ảnh.");
            }

            var imageFolder = $@"\{_settings.ImageFolder}\images\{type}\{now:MMyyyy}";

            var folder = _hostingEnv.WebRootPath + imageFolder;

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var filePath = Path.Combine(folder, fileName);
            using (var fs = global::System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            var path = Path.Combine(imageFolder, fileName).Replace(@"\",@"/");
            return Ok(new {path});
        }
    }
}
