using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Xstorage.Api.ApiViewModels;
using Xstorage.Data;
using Xstorage.Entities.Models;
using Xstorage.Repositories;
using Xstorage.Shared;

namespace Xstorage.Api.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("CorsAllowAll_NONSECURE")]
    public class StoragesApiController : ControllerBase
    {
        readonly ILogger<StoragesApiController> logger;
        readonly XstorageDbContext _context;
        readonly UserRepository userRepository;
        readonly StorageRepository storageRepository;

        public StoragesApiController(ILogger<StoragesApiController> logger, XstorageDbContext context,
            UserRepository userRepository,
            StorageRepository storageRepository) 
        {
            this.logger = logger;
            this._context = context;
            this.userRepository = userRepository;
            this.storageRepository = storageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> StorageList()
        {
            User user = await userRepository.GetUserAsync(User.Identity);
            if (user == null) return NotFound();
            var storages = _context.Storage.Where(x => x.HostId == user.Id).ToList();
            var json = JsonSerializer.Serialize(storages);
            return Ok(json);
        }

        [HttpGet]
        public IActionResult Details([FromQuery] string apiKey, [FromQuery] string storageId, [FromQuery] string path)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetFile([FromQuery] string apiKey, [FromQuery] string storageId, [FromQuery] string path)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult UploadFile([FromQuery] string apiKey, [FromBody] UploadFileApiViewModel uploadFile)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetApiKey([FromQuery] string userNameOrEmail, [FromQuery] string userPassword)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult RefreshApiKey([FromQuery] string oldKey)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult DeleteApiKey([FromQuery] string apiKey)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetFileText([FromQuery] string storageId, [FromQuery] string path)
        {
            Storage? storage = await storageRepository.GetStorage(storageId);
            if (storage == null)
            {
                return NotFound("not found");
            }

            path = Path.Combine(storage.Path, path);
            string text = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
            return Ok(text);
        }
    }
}
