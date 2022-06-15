using Microsoft.AspNetCore.Mvc;

using Thoughts.Domain.Base.Entities;
using Thoughts.Interfaces;
using Thoughts.Services.Models;

namespace Thoughts.WebAPI.Controllers
{
    [ApiController]
    [Route(WebAPIAddresses.BlogPosts)]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostManager _manager;
        private readonly ILogger<BlogPostController> _logger;

        public BlogPostController(IBlogPostManager manager, ILogger<BlogPostController> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        /// <summary>Назначение тэга посту</summary>
        /// <param name="dto">DTO ключевого слова поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если тэг был назначен успешно</returns>
        [HttpPut("posts/{PostId}/tag/{Tag}")]
        public async Task<IActionResult> AssignTagAsync([FromRoute] PostTagDTO dto, CancellationToken Cancel = default)
        {
            var result = await _manager.AssignTagAsync(dto.PostId, dto.Tag, Cancel);
            return Ok(result);
        }

        /// <summary>Изменение тела поста</summary>
        /// <param name="dto">DTO тела поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если тело было изменено успешно</returns>
        [HttpPut("posts/{PostId}/body/{Body}")]
        public async Task<IActionResult> ChangePostBodyAsync([FromRoute] PostBodyDTO dto, CancellationToken Cancel = default)
        {
            var result = await _manager.ChangePostBodyAsync(dto.PostId, dto.Body, Cancel);
            return Ok(result);
        }

        /// <summary>Изменение категории поста</summary>
        /// <param name="dto">DTO категории поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Изменённая категория</returns>
        [HttpPut("posts/{PostId}/category/{CategoryName}")]
        public async Task<IActionResult> ChangePostCategoryAsync([FromRoute] PostCategoryDTO dto, CancellationToken Cancel = default)
        {
            var result = await _manager.ChangePostCategoryAsync(dto.PostId, dto.CategoryName, Cancel);
            return Ok(result);
        }

        /// <summary>Изменение статуса поста</summary>
        /// <param name="dto">DTO статуса поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Изменённый статус</returns>
        [HttpPut("posts/{PostId}/status/{Status}")]
        public async Task<IActionResult> ChangePostStatusAsync([FromRoute] PostStatusDTO dto, CancellationToken Cancel = default)
        {
            var result = await _manager.ChangePostStatusAsync(dto.PostId, dto.Status, Cancel);
            return Ok(result);
        }

        /// <summary>Изменение заголовка поста</summary>
        /// <param name="dto">DTO заголовка поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если заголовок был изменен успешно</returns>
        [HttpPut("posts/{PostId}/title/{Title}")]
        public async Task<IActionResult> ChangePostTitleAsync([FromRoute] PostTitleDTO dto, CancellationToken Cancel = default)
        {
            var result = await _manager.ChangePostTitleAsync(dto.PostId, dto.Title, Cancel);
            return Ok(result);
        }

        /// <summary>Создание нового поста</summary>
        /// <param name="dto">DTO создания нового поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Вновь созданный пост</returns>
        [HttpPost("posts/users/{UserId}/{Title}-{Body}-{Category}")]
        public async Task<IActionResult> CreatePostAsync([FromRoute] CreatePostDTO dto, CancellationToken Cancel = default)
        {
            var result = await _manager.CreatePostAsync(dto.Title, dto.Body, dto.UserId, dto.Category, Cancel);
            return Ok(result);
        }


        /// <summary>Удаление поста</summary>
        /// <param name="Id">Идентификатор поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если пост был удалён успешно</returns>
        [HttpDelete("posts/{Id}")]
        public async Task<IActionResult> DeletePostAsync(int Id, CancellationToken Cancel = default)
        {
            var result = await _manager.DeletePostAsync(Id, Cancel);
            return Ok(result);
        }


        /// <summary>Получить все посты</summary>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Перечисление всех постов</returns>
        [HttpGet("posts")]
        public async Task<IActionResult> GetAllPostsAsync(CancellationToken Cancel = default)
        {
            var result = await _manager.GetAllPostsAsync(Cancel);
            return Ok(result);
        }

        /// <summary>Получить все посты пользователя по его идентификатору</summary>
        /// <param name="UserId">Идентификатор пользователя</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Перечисление всех постов пользователя</returns>
        [HttpGet("posts/users/{UserId}")]
        public async Task<IActionResult> GetAllPostsByUserIdAsync(string UserId, CancellationToken Cancel = default)
        {
            var result = await _manager.GetAllPostsByUserIdAsync(UserId, Cancel);
            return Ok(result);
        }

        /// <summary>Получить все страницы с постами пользователя по его идентификатору (есть TODO блок)</summary>
        /// <param name="dto">DTO просмотра страниц постов пользователя</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Страница с перечислением всех постов пользователя</returns>
        [HttpPost("posts/users/{UserId}/pages/{PageIndex}-{PageSize}")]
        public async Task<IActionResult> GetAllPostsByUserIdPageAsync([FromRoute] UserPostsPageDTO dto, CancellationToken Cancel = default)
        {
            var result = await _manager.GetAllPostsByUserIdPageAsync(dto.UserId, dto.PageIndex, dto.PageSize, Cancel);
            return Ok(result);
        }

        /// <summary>Получить определённое количество постов пользователя</summary>
        /// <param name="dto">DTO просмотра определённого количества постов пользователя</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Перечисление постов пользователя</returns>
        [HttpPost("posts/users/{UserId}/{Skip}-{Take}")]
        public async Task<IActionResult> GetAllPostsByUserIdSkipTakeAsync([FromRoute] UserPostsSkipTakeDTO dto, CancellationToken Cancel = default)
        {
            var result = await _manager.GetAllPostsByUserIdSkipTakeAsync(dto.UserId, dto.Skip, dto.Take, Cancel);
            return Ok(result);
        }

        /// <summary>Получить число постов</summary>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Число постов</returns>
        [HttpGet("posts/count")]
        public async Task<IActionResult> GetAllPostsCountAsync(CancellationToken Cancel = default)
        {
            var result = await _manager.GetAllPostsCountAsync(Cancel);
            return Ok(result);
        }

        /// <summary>Получить страницу со всеми постами</summary>
        /// <param name="dto">DTO страницы постов</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Страница с постами</returns>
        [HttpGet("posts/pages/{PageIndex}/{PageSize}")]
        public async Task<IActionResult> GetAllPostsPageAsync([FromRoute] PostsPageDTO dto, CancellationToken Cancel = default)
        {
            var result = await _manager.GetAllPostsPageAsync(dto.PageIndex, dto.PageSize, Cancel);
            return Ok(result);
        }

        /// <summary>Получить определённое количество постов из всех</summary>
        /// <param name="dto">Количество пропускаемых и получаемых элементов</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Перечисление постов</returns>
        [HttpPost("posts/{Skip}-{Take}")]
        public async Task<IActionResult> GetAllPostsSkipTakeAsync([FromRoute] PostsSkipTakeDTO dto, CancellationToken Cancel = default)
        {
            var result = await _manager.GetAllPostsSkipTakeAsync(dto.Skip, dto.Take, Cancel);
            return Ok(result);
        }

        /// <summary>Получить тэги к посту по его идентификатору</summary>
        /// <param name="Id">Идентификатор поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Перечисление тэгов</returns>
        [HttpGet("posts/{Id}/tags")]
        public async Task<IActionResult> GetBlogTagsAsync(int Id, CancellationToken Cancel = default)
        {
            var result = await _manager.GetBlogTagsAsync(Id, Cancel);
            return Ok(result);
        }

        /// <summary>Получение поста по его идентификатору</summary>
        /// <param name="Id">Идентификатор поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Найденный пост или <b>null</b></returns>
        [HttpGet("posts/{Id}")]
        public async Task<IActionResult> GetPostAsync(int Id, CancellationToken Cancel = default)
        {
            var result = await _manager.GetPostAsync(Id,Cancel);
            return Ok(result);
        }

        /// <summary>Получить посты по тэгу</summary>
        /// <param name="Tag">Тэг</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Перечисление постов</returns>
        [HttpGet("posts/tags/{Tag}")]
        public async Task<IActionResult> GetPostsByTag(string Tag, CancellationToken Cancel = default)
        {
            var result = await _manager.GetPostsByTag(Tag, Cancel);
            return Ok(result);
        }

        /// <summary>Получить количество постов пользователя</summary>
        /// <param name="UserId">Идентификатор пользователя</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Количество постов</returns>
        [HttpGet("posts/users/{UserId}/count")]
        public async Task<IActionResult> GetUserPostsCountAsync(string UserId, CancellationToken Cancel = default)
        {
            var result = await _manager.GetUserPostsCountAsync(UserId, Cancel);
            return Ok(result);
        }

        /// <summary>Удалить тэг с поста</summary>
        /// <param name="dto">DTO ключевого слова поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если тэг был удалён успешно</returns>
        [HttpPost("posts/{PostId}/tag/{Tag}")]
        public async Task<IActionResult> RemoveTagAsync([FromRoute] PostTagDTO dto, CancellationToken Cancel = default)
        {
            var result = await _manager.RemoveTagAsync(dto.PostId, dto.Tag, Cancel);
            return Ok(result);
        }
    }
}
