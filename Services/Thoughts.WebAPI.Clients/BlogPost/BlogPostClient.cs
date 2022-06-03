using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Thoughts.Domain.Base.Entities;
using Thoughts.Interfaces;
using Thoughts.Interfaces.Base.Repositories;
using Thoughts.Services.Models;
using Thoughts.WebAPI.Clients.Base;

namespace Thoughts.WebAPI.Clients.BlogPost
{
    public class BlogPostClient : BaseClient, IBlogPostManager
    {
        private readonly ILogger<BlogPostClient> _logger;

        protected BlogPostClient(HttpClient client, ILogger<BlogPostClient> logger) : base(client, WebAPIAddresses.BlogPosts)
        {
            _logger = logger;
        }

        /// <summary>Назначение тэга посту</summary>
        /// <param name="PostId">Идентификатор поста</param>
        /// <param name="Tag">Добавляемый тэг</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если тэг был назначен успешно</returns>
        public async Task<bool> AssignTagAsync(int PostId, string Tag, CancellationToken Cancel = default)
        {
            string address = Address + $"posts/{PostId}/tag/{Tag}";
            var model = new PostTagDTO() { PostId = PostId, Tag = Tag };

            var response = await PutAsync(address, model, Cancel);
            var result = response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<bool>()
                .Result;

            return result;
        }

        /// <summary>Изменение тела поста</summary>
        /// <param name="PostId">Идентификатор поста</param>
        /// <param name="Body">Новое тело поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если тело было изменено успешно</returns>
        public async Task<bool> ChangePostBodyAsync(int PostId, string Body, CancellationToken Cancel = default)
        {
            string address = Address + $"posts/{PostId}/body/{Body}";
            var model = new PostBodyDTO() { PostId = PostId, Body = Body };

            var response = await PutAsync(address, model, Cancel);
            var result = response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<bool>()
                .Result;

            return result;
        }

        /// <summary>Изменение категории поста</summary>
        /// <param name="PostId">Идентификатор поста</param>
        /// <param name="CategoryName">Новая категория поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Изменённая категория</returns>
        public async Task<Category> ChangePostCategoryAsync(int PostId, string CategoryName, CancellationToken Cancel = default)
        {
            string address = Address + $"posts/{PostId}/category/{CategoryName}";
            var model = new PostCategoryDTO() { PostId = PostId, CategoryName = CategoryName };

            var response = await PutAsync(address, model, Cancel);
            var result = response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<Category>()
                .Result;

            return result;
        }

        /// <summary>Изменение статуса поста</summary>
        /// <param name="PostId">Идентификатор поста</param>
        /// <param name="Status">Новый статус поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Изменённый статус</returns>
        public async Task<Status> ChangePostStatusAsync(int PostId, string Status, CancellationToken Cancel = default)
        {
            string address = Address + $"posts/{PostId}/status/{Status}";
            var model = new PostStatusDTO() { PostId = PostId, Status = Status };

            var response = await PutAsync(address, model, Cancel);
            var result = response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<Status>()
                .Result;

            return result;
        }

        /// <summary>Изменение заголовка поста</summary>
        /// <param name="PostId">Идентификатор поста</param>
        /// <param name="Title">Новый заголовок поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если заголовок был изменен успешно</returns>
        public async Task<bool> ChangePostTitleAsync(int PostId, string Title, CancellationToken Cancel = default)
        {
            string address = Address + $"posts/{PostId}/title/{Title}";
            var model = new PostTitleDTO() { PostId = PostId, Title = Title };

            var response = await PutAsync(address, model, Cancel);
            var result = response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<bool>()
                .Result;

            return result;
        }

        /// <summary>Создание нового поста</summary>
        /// <param name="Title">Заголовок</param>
        /// <param name="Body">Тело</param>
        /// <param name="UserId">Идентификатор пользователя, создающего пост</param>
        /// <param name="Category">Категория</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Вновь созданный пост</returns>
        public async Task<Post> CreatePostAsync(string Title, string Body, string UserId, string Category, CancellationToken Cancel = default)
        {
            string address = Address + $"posts/users/{UserId}/{Title}-{Body}-{Category}";
            var model = new CreatePostDTO() { Title = Title, Body = Body, UserId = UserId, Category = Category};

            var response = await PostAsync(address, model, Cancel);
            var result = response.Content.ReadFromJsonAsync<Post>().Result;

            return result;

        }

        /// <summary>Удаление поста</summary>
        /// <param name="Id">Идентификатор поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если пост был удалён успешно</returns>
        public async Task<bool> DeletePostAsync(int Id, CancellationToken Cancel = default)
        {
            var address = Address + $"posts/{Id}";
            var response = await DeleteAsync(address, Cancel);

            var result = response.IsSuccessStatusCode;
            return result;
        }

        /// <summary>Получить все посты</summary>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Перечисление всех постов</returns>
        public async Task<IEnumerable<Post>> GetAllPostsAsync(CancellationToken Cancel = default)
        {
            var address = Address + "posts";
            var response = await GetAsync<IEnumerable<Post>>(address, Cancel);

            return response ?? Enumerable.Empty<Post>();
        }

        /// <summary>Получить все посты пользователя по его идентификатору</summary>
        /// <param name="UserId">Идентификатор пользователя</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Перечисление всех постов пользователя</returns>
        public async Task<IEnumerable<Post>> GetAllPostsByUserIdAsync(string UserId, CancellationToken Cancel = default)
        {
            var address = Address + $"posts/users/{UserId}";
            var response = await GetAsync<IEnumerable<Post>>(address, Cancel);

            return response ?? Enumerable.Empty<Post>();
        }

        /// <summary>Получить все страницы с постами пользователя по его идентификатору (есть TODO блок)</summary>
        /// <param name="UserId">Идентификатор пользователя</param>
        /// <param name="PageIndex">Номер страницы</param>
        /// <param name="PageSize">Размер страницы</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Страница с перечислением всех постов пользователя</returns>
        public async Task<IPage<Post>> GetAllPostsByUserIdPageAsync(string UserId, int PageIndex, int PageSize, CancellationToken Cancel = default)
        {
            var address = Address + $"posts/users/{UserId}/pages/{PageIndex}-{PageSize}";
            var model = new UserPostsPageDTO() { UserId = UserId, PageIndex = PageIndex };

            var response = await PostAsync(address, model, Cancel);
            var result = response.Content.ReadFromJsonAsync<IPage<Post>>().Result;

            return result;
        }

        /// <summary>Получить определённое количество постов пользователя</summary>
        /// <param name="UserId">Идентификатор пользователя</param>
        /// <param name="Skip">Количество пропускаемых элементов</param>
        /// <param name="Take">Количество получаемых элементов</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Перечисление постов пользователя</returns>
        public async Task<IEnumerable<Post>> GetAllPostsByUserIdSkipTakeAsync(string UserId, int Skip, int Take, CancellationToken Cancel = default)
        {
            var address = Address + $"posts/users/{UserId}/{Skip}-{Take}";
            var model = new UserPostsSkipTakeDTO() { UserId = UserId, Skip = Skip, Take = Take };

            var response = await PostAsync(address, model, Cancel);
            var result = response.Content.ReadFromJsonAsync<IEnumerable<Post>>().Result;

            return result;
        }

        /// <summary>Получить число постов</summary>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Число постов</returns>
        public async Task<int> GetAllPostsCountAsync(CancellationToken Cancel = default)
        {
            var address = Address + $"posts/count";
            var response = await GetAsync<int>(address, Cancel);

            return response;
        }

        /// <summary>Получить страницу со всеми постами</summary>
        /// <param name="PageIndex">Номер страницы</param>
        /// <param name="PageSize">Размер страницы</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Страница с постами</returns>
        public async Task<IPage<Post>> GetAllPostsPageAsync(int PageIndex, int PageSize, CancellationToken Cancel = default)
        {
            var address = Address + $"posts/pages/{PageIndex}/{PageSize}";
            var model = new PostsPageDTO() { PageIndex = PageIndex, PageSize = PageSize };

            var response = await PostAsync(address, model, Cancel);
            var result = response.Content.ReadFromJsonAsync<IPage<Post>>().Result;

            return result;
        }

        /// <summary>Получить определённое количество постов из всех</summary>
        /// <param name="Skip">Количество пропускаемых элементов</param>
        /// <param name="Take">Количество получаемых элементов</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Перечисление постов</returns>
        public async Task<IEnumerable<Post>> GetAllPostsSkipTakeAsync(int Skip, int Take, CancellationToken Cancel = default)
        {
            var address = Address + $"posts/{Skip}-{Take}";
            var model = new PostsSkipTakeDTO() { Skip = Skip, Take = Take };

            var response = await PostAsync(address, model, Cancel);
            var result = response.Content.ReadFromJsonAsync<IEnumerable<Post>>().Result;

            return result;
        }

        /// <summary>Получить тэги к посту по его идентификатору</summary>
        /// <param name="Id">Идентификатор поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Перечисление тэгов</returns>
        public async Task<IEnumerable<Tag>> GetBlogTagsAsync(int Id, CancellationToken Cancel = default)
        {
            var address = Address + $"posts/{Id}/tags";
            var response = await GetAsync<IEnumerable<Tag>>(address, Cancel);

            return response;
        }

        /// <summary>Получение поста по его идентификатору</summary>
        /// <param name="Id">Идентификатор поста</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Найденный пост или <b>null</b></returns>
        public async Task<Post?> GetPostAsync(int Id, CancellationToken Cancel = default)
        {
            var address = Address + $"posts/{Id}";
            var response = await GetAsync<Post?>(address, Cancel);

            return response;
        }

        /// <summary>Получить посты по тэгу</summary>
        /// <param name="Tag">Тэг</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Перечисление постов</returns>
        public async Task<IEnumerable<Post>> GetPostsByTag(string Tag, CancellationToken Cancel = default)
        {
            var address = Address + $"posts/tags/{Tag}";
            var response = await GetAsync<IEnumerable<Post>>(address, Cancel);

            return response;
        }

        /// <summary>Получить количество постов пользователя</summary>
        /// <param name="UserId">Идентификатор пользователя</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Количество постов</returns>
        public async Task<int> GetUserPostsCountAsync(string UserId, CancellationToken Cancel = default)
        {
            var address = Address + $"posts/users/{UserId}/count";
            var response = await GetAsync<int>(address, Cancel);

            return response;
        }

        /// <summary>Удалить тэг с поста</summary>
        /// <param name="PostId">Идентификатор поста</param>
        /// <param name="Tag">Тэг</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если тэг был удалён успешно</returns>
        public async Task<bool> RemoveTagAsync(int PostId, string Tag, CancellationToken Cancel = default)
        {
            var address = Address + $"posts/{PostId}/tag/{Tag}";
            var model = new PostTagDTO() { PostId = PostId, Tag = Tag };

            var response = await PostAsync(address, model, Cancel);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;

            return result;
        }
    }
}
