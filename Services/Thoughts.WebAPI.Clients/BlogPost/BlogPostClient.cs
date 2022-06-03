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
            string address = Address + "posts/{PostId}/tag/{Tag}";
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
            string address = Address + "posts/{PostId}/body/{Body}";
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
            string address = Address + "posts/{PostId}/category/{CategoryName}";
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
            string address = Address + "posts/{PostId}/status/{Status}";
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
            string address = Address + "posts/{PostId}/title/{Title}";
            var model = new PostTitleDTO() { PostId = PostId, Title = Title };

            var response = await PutAsync(address, model, Cancel);
            var result = response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<bool>()
                .Result;

            return result;
        }
        public async Task<Post> CreatePostAsync(string Title, string Body, string UserId, string Category, CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<bool> DeletePostAsync(int Id, CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<IEnumerable<Post>> GetAllPostsAsync(CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<IEnumerable<Post>> GetAllPostsByUserIdAsync(string UserId, CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<IPage<Post>> GetAllPostsByUserIdPageAsync(string UserId, int PageIndex, int PageSize, CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<IEnumerable<Post>> GetAllPostsByUserIdSkipTakeAsync(string UserId, int Skip, int Take, CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<int> GetAllPostsCountAsync(CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<IPage<Post>> GetAllPostsPageAsync(int PageIndex, int PageSize, CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<IEnumerable<Post>> GetAllPostsSkipTakeAsync(int Skip, int Take, CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<IEnumerable<Tag>> GetBlogTagsAsync(int Id, CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<Post?> GetPostAsync(int Id, CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<IEnumerable<Post>> GetPostsByTag(string Tag, CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<int> GetUserPostsCountAsync(string UserId, CancellationToken Cancel = default) => throw new NotImplementedException();
        public async Task<bool> RemoveTagAsync(int PostId, string Tag, CancellationToken Cancel = default) => throw new NotImplementedException();
    }
}
