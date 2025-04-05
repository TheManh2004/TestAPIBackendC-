using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Data;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FileUploadController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        [RequestSizeLimit(50 * 1024 * 1024)]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsDir = Path.Combine(_env.ContentRootPath, "Uploads");
            if (!Directory.Exists(uploadsDir))
                Directory.CreateDirectory(uploadsDir);

            var filePath = Path.Combine(uploadsDir, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var uploaded = new UploadedFile
            {
                FileName = file.FileName,
                FilePath = filePath,
                FileSize = file.Length
            };

            _context.UploadedFiles.Add(uploaded);
            await _context.SaveChangesAsync();

            return Ok(uploaded);
        }
    }
}
