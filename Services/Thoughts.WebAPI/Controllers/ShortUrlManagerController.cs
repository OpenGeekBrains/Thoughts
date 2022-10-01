using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

using Thoughts.Interfaces.Base;

namespace Thoughts.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route(WebApiControllersPath.ShortUrl)]
    [ApiController]
    public class ShortUrlManagerController : ControllerBase
    {
        private readonly IShortUrlManager _shortUrlManager;

        public ShortUrlManagerController(IShortUrlManager ShortUrlManager)
        {
            _shortUrlManager = ShortUrlManager;
        }

        // GET: api/v1/url?Alias=...
        [HttpGet]
        public async Task<ActionResult<Uri>> GetUrl(string Alias)
        {
            var result = await _shortUrlManager.GetUrlAsync(Alias);
            if (result is null)
                return BadRequest();

            return result;
        }

        // GET: api/v1/url/10
        [HttpGet("{Id}")]
        public async Task<ActionResult<Uri>> GetUrlById(int Id)
        {
            var result = await _shortUrlManager.GetUrlByIdAsync(Id);
            if (result is null)
                return BadRequest();

            return result;
        }

        //GET: api/v1/url/alias/10
        [HttpGet("alias/{Id}")]
        public async Task<ActionResult<string>> GetAliasById(int Id)
        {
            var result = await _shortUrlManager.GetAliasByIdAsync(Id);

            if (String.IsNullOrEmpty(result))
                return BadRequest();

            return result;
        }

        // POST api/v1/url
        [HttpPost]
        public async Task<ActionResult<int>> AddUrl([FromBody]string Url)
        {
            var result = await _shortUrlManager.AddUrlAsync(Url);
            if (result==0)
                return BadRequest();

            //return $"{result}";
            return result;
        }

        // DELETE api/v1/url/10
        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> DeleteUrl(int Id)
        {
            var result= await _shortUrlManager.DeleteUrlAsync(Id);
            return result ? result : BadRequest();
        }

        // POST api/v1/url/10
        [HttpPost("{Id}")]
        public async Task<ActionResult<bool>> UpdateUrl(int Id, [FromBody] string Url)
        {
            var result=await _shortUrlManager.UpdateUrlAsync(Id, Url);
            return result ? result : BadRequest();
        }
    }
}
