using Microsoft.AspNetCore.Mvc;

using Thoughts.Interfaces.Base;

namespace Thoughts.UI.MVC.Controllers
{
    public class UrlController : Controller
    {
        private readonly IShortUrlManager _shortUrlManager;

        public UrlController(IShortUrlManager ShortUrlManager)
        {
            _shortUrlManager = ShortUrlManager;
        }

        // GET -> https://localhost:5010/url/17D91E667026F10A16241F0784608CF1
        [Route("url/{Alias}")]
        [HttpGet]
        public async Task<IActionResult> RedirectByAlias(string Alias)
        {
            var url = await _shortUrlManager.GetUrlAsync(Alias);
            if (url is null)
                return NotFound();
            return Redirect(url.AbsoluteUri);
        }

        // GET -> https://localhost:5010/url/GetUrlById/10
        [Route("url/GetUrlById/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetUrlById(int Id)
        {
            var url = await _shortUrlManager.GetUrlByIdAsync(Id);
            if (url is null)
                return NotFound();
            return View(url);
        }

        // GET -> https://localhost:5010/url/GetAliasById/10?Length=6
        [Route("url/GetAliasById/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetAliasById(int Id, int Length)
        {
            var alias = Length > 0
                ? await _shortUrlManager.GetAliasByIdAsync(Id, Length)
                : await _shortUrlManager.GetAliasByIdAsync(Id);
            if (String.IsNullOrEmpty(alias))
                return NotFound();
            var shortUrl = Url.ActionLink(action: nameof(RedirectByAlias), controller: "url", values: new { Alias = alias });
            return View(model: shortUrl);
        }

        // POST -> https://localhost:5010/url/
        [Route("url")]
        [HttpPost]
        public async Task<IActionResult> AddUrl(string url)
        {
            var result = await _shortUrlManager.AddUrlAsync(url);
            if (result == 0)
                return BadRequest();
            var getUrl = Url.ActionLink(action: nameof(GetUrlById), controller: "url", values: new { Id = result });
            var getAlias = Url.ActionLink(action: nameof(GetAliasById), controller: "url", values: new { Id = result });
            ShortUrlWebModel shortUrlWebModel = new()
            {
                Id = result,
                OriginalUrl = url,
                GetUrl = getUrl!,
                GetAlias = getAlias!
            };
            return View(shortUrlWebModel);
        }

        // GET -> https://localhost:5010/url/Test
        [Route("url/Test")]
        public IActionResult Test()
        {
            return View();
        }
    }
}
