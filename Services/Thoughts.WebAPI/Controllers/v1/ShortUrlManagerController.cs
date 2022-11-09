using System;
using System.Collections;
using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

using Thoughts.Domain.Base.Entities;
using Thoughts.Interfaces;
using Thoughts.Interfaces.Base;

namespace Thoughts.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route(WebApiControllersPath.ShortUrl)]
    [ApiController]
    public class ShortUrlManagerController : ControllerBase
    {
        private readonly IShortUrlManager _shortUrlManager;
        private readonly IConfiguration _configuration;

        public ShortUrlManagerController(IShortUrlManager ShortUrlManager, IConfiguration Configuration)
        {
            _shortUrlManager = ShortUrlManager;
            _configuration = Configuration;
        }

        // GET: api/v1/url?Alias=...
        [HttpGet]
        public async Task<ActionResult<Uri>> GetUrl(string Alias)
        {
            var result = await _shortUrlManager.GetUrlAsync(Alias);
            if (result is null)
                return NotFound();

            return result;
        }

        // GET: api/v1/url/10
        [HttpGet("{Id}")]
        public async Task<ActionResult<Uri>> GetUrlById(int Id)
        {
            var result = await _shortUrlManager.GetUrlByIdAsync(Id);
            if (result is null)
                return NotFound();

            return result;
        }

        //GET: api/v1/url/alias/10?Length=10
        [HttpGet("alias/{Id}")]
        public async Task<ActionResult<string>> GetAliasById(int Id, int Length)
        {
            string result;
            if (Length > 0)
                result = await _shortUrlManager.GetAliasByIdAsync(Id, Length);
            else if (int.TryParse(_configuration["ShortUrlMaxLength"], out int lengthFromConfig))
                result = await _shortUrlManager.GetAliasByIdAsync(Id, lengthFromConfig);
            else
                result = await _shortUrlManager.GetAliasByIdAsync(Id);


            if (string.IsNullOrEmpty(result))
                return NotFound();

            return AcceptedAtAction(nameof(GetUrl), new { Alias = result }, result);
        }

        // POST api/v1/url
        [HttpPost]
        public async Task<ActionResult<int>> AddUrl([FromBody] string Url)
        {
            if (!Regex.IsMatch(Url, @"^https?://"))
                Url = "http://" + Url;

            var result = await _shortUrlManager.AddUrlAsync(Url);
            if (result == 0)
                return BadRequest();

            return CreatedAtAction(nameof(GetAliasById), new { Id = result }, result);
        }

        // DELETE api/v1/url/10
        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> DeleteUrl(int Id)
        {
            var result = await _shortUrlManager.DeleteUrlAsync(Id);
            return result ? result : NotFound();
        }

        // POST api/v1/url/10
        [HttpPost("{Id}")]
        public async Task<ActionResult<bool>> UpdateUrl(int Id, [FromBody] string Url)
        {
            var result = await _shortUrlManager.UpdateUrlAsync(Id, Url);
            return result ? result : NotFound();
        }

        // GET api/v1/url/resetstat/10
        [HttpGet("resetstat/{Id}")]
        public async Task<ActionResult<bool>> ResetStatistic(int Id)
        {
            var result = await _shortUrlManager.ResetStatistic(Id);
            return result ? result : NotFound();
        }

        //GET: api/v1/url/getstat/10
        [HttpGet("getstat/{Id}")]
        public async Task<ActionResult<IEnumerable<ShortUrl>>> GetStatisticById(int Id,int Length)
        {
            IEnumerable<ShortUrl> result;

            if (Length > 0)
                result = await _shortUrlManager.GetStatistic(Id,Length);
            else if (int.TryParse(_configuration["ShortUrlMaxLength"], out int lengthFromConfig))
                result = await _shortUrlManager.GetStatistic(Id, lengthFromConfig);
            else
                result = await _shortUrlManager.GetStatistic(Id);


            if (result is null || !result.Any())
                return NotFound();

            return result.ToList();
        }
    }
}
