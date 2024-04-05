using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Xstorage.Api.ApiViewModels;
using Xstorage.Data;
using Xstorage.Entities.Models;
using Xstorage.Repositories;

namespace Xstorage.Api.ApiControllers
{
    [Route("api/storages")]
    [ApiController]
    public class StoragesApiController : ControllerBase
    {
        readonly ILogger<StoragesApiController> logger;
        readonly XstorageDbContext _context;
        readonly UserRepository userRepository;

        public StoragesApiController(ILogger<StoragesApiController> logger, XstorageDbContext context,
            UserRepository userRepository) 
        {
            this.logger = logger;
            this._context = context;
            this.userRepository = userRepository;
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
        public IActionResult Details(string storageId, string path)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetFile(string storageId, string path)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult UploadFile([FromBody] UploadFileApiViewModel uploadFile)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetApiKey(string userNameOrEmail, string userPassword)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult RefreshApiKey(string oldKey)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult DeleteApiKey(string apiKey)
        {
            return Ok();
        }
    }
}
