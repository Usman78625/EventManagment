using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Threading.Tasks;
using EventManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EventManagement.Domain.Entities;

namespace EventManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public UsersController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("{id}/avatar")]
        [Authorize]
        public async Task<IActionResult> UploadAvatar(int id, [FromForm] IFormFile file)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            if (file == null || file.Length == 0) return BadRequest("No file uploaded");

            var uploads = Path.Combine(_env.WebRootPath, "avatars");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
            var fileName = $"user_{id}_{Path.GetRandomFileName()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploads, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            user.AvatarUrl = $"/avatars/{fileName}";
            await _context.SaveChangesAsync();
            return Ok(new { avatarUrl = user.AvatarUrl });
        }
    }
} 