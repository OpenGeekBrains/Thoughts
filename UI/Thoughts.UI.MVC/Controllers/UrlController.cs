using System;

using Microsoft.AspNetCore.Mvc;

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
            if (!Regex.IsMatch(url, @"^https?://"))
                url = "http://" + url;

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

        // GET -> https://localhost:5010/url/CreateUrl
        [Route("url/CreateUrl")]
        public async Task<IActionResult> CreateUrl()
        {
            return View();
        }

        // GET -> https://localhost:5010/url/Statistic?Length=9
        [Route("url/Statistic")]
        public async Task<IActionResult> Statistic(int Length )
        {
            var result = await _shortUrlManager.GetStatistic(Length: Length);
            if (result is null || !result.Any())
                return BadRequest();

            foreach (var item in result)
            {
                item.Alias = Url.ActionLink(action: nameof(RedirectByAlias), controller: "url", values: new { Alias = item.Alias });
            }
            return View(result);
        }

        // GET -> https://localhost:5010/url/ResetStatistic/10
        [Route("url/ResetStatistic/{Id}")]
        public async Task<IActionResult> ResetStatistic(int Id=0)
        {
            await _shortUrlManager.ResetStatistic(Id);

            return RedirectToAction("Statistic");
        }
    }
}
