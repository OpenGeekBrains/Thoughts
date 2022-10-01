﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Thoughts.DAL;
using Thoughts.Interfaces.Base;

namespace Thoughts.Services.InSQL
{
    public class SqlShortUrlManagerService : IShortUrlManager
    {
        private readonly ThoughtsDB _db;
        private readonly ILogger<SqlBlogPostManager> _logger;

        public SqlShortUrlManagerService(ThoughtsDB Db, ILogger<SqlBlogPostManager> Logger)
        {
            _db = Db;
            _logger = Logger;
        }

        public async Task<int> AddUrlAsync(string UrlString, CancellationToken Cancel = default)
        {
            _logger.LogInformation($"Создание короткой ссылки для Url:{UrlString}");

            var url = CreateUrl(UrlString);

            if (url is null)
            {
                _logger.LogInformation($"Короткая ссылка не создана. Некоректный Url:{UrlString}");
                return 0;
            }

            var shortUrl = await _db.ShortUrls.
                FirstOrDefaultAsync(
                    u => u.OriginalUrl == url,
                    Cancel
                ).ConfigureAwait(false);

            if (shortUrl is not null)
            {
                _logger.LogInformation($"Короткая ссылка {shortUrl.Alias} уже существует для Url:{shortUrl.OriginalUrl}");
                return shortUrl.Id;
            }

            shortUrl = new()
            {
                OriginalUrl = url,
                Alias = GenerateAlias(url.OriginalString)
            };
            await _db.ShortUrls.AddAsync(shortUrl, Cancel).ConfigureAwait(false);
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            _logger.LogInformation($"Создана короткая ссылка {shortUrl.Alias} для Url:{shortUrl.OriginalUrl}");

            return shortUrl.Id;
        }

        public async Task<bool> DeleteUrlAsync(int Id, CancellationToken Cancel = default)
        {
            _logger.LogInformation($"Удаление короткой ссылки Id:{Id}");

            var url = await _db.ShortUrls.
                FirstOrDefaultAsync(
                    u => u.Id == Id,
                    Cancel).
                ConfigureAwait(false);
            if (url is null)
            {
                _logger.LogInformation($"Короткая ссылка не удалена. Не удалось найти короткую ссылку Id:{Id}");
                return false;
            }

            try
            {
                _db.ShortUrls.Remove(url);
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError($"Удаление короткой ссылки Id:{Id} вызвало исключение DbUpdateException: {e.ToString()}");
                return false;
            }
            catch (OperationCanceledException e)
            {
                _logger.LogError($"Удаление короткой ссылки Id:{Id} вызвало исключение DbUpdateConcurrencyException: {e.ToString()}");
                return false;
            }

            _logger.LogInformation($"Успешное удаление короткой ссылки Id:{Id}");
            return true;
        }

        public async Task<Uri?> GetUrlAsync(string Alias, CancellationToken Cancel = default)
        {
            var result = await _db.ShortUrls.
                FirstOrDefaultAsync(
                    u => u.Alias == Alias,
                    Cancel
                ).
                ConfigureAwait(false);
            if (result is null)
                return null;

            return result.OriginalUrl;
        }
        public async Task<Uri?> GetUrlByIdAsync(int Id, CancellationToken Cancel = default)
        {
            var result = await _db.ShortUrls.
                FirstOrDefaultAsync(
                    u => u.Id == Id,
                    Cancel
                ).
                ConfigureAwait(false);
            if (result is null)
                return null;

            return result.OriginalUrl;
        }

        public async Task<string> GetAliasByIdAsync(int Id, CancellationToken Cancel = default)
        {
            var result = await _db.ShortUrls.
                FirstOrDefaultAsync(
                    u => u.Id == Id,
                    Cancel
                ).
                ConfigureAwait(false);
            if (result is null)
                return null ;

            return result.Alias;
        }

        public async Task<bool> UpdateUrlAsync(int Id, string UrlString, CancellationToken Cancel = default)
        {
            _logger.LogInformation($"Обновление короткой ссылки Id:{Id}. Новый Url:{UrlString}");

            var url = CreateUrl(UrlString);

            if (url is null)
            {
                _logger.LogInformation($"Короткая ссылка не обновлена. Некоректный Url:{UrlString}");
                return false;
            }

            var shortUrl = await _db.ShortUrls.
                FirstOrDefaultAsync(
                    u => u.Id == Id,
                    Cancel
                ).
                ConfigureAwait(false);
            if (shortUrl is null)
            {
                _logger.LogInformation($"Короткая ссылка не обновлена. Не удалось найти короткую ссылку Id:{Id}");
                return false;
            }

            try
            {
                shortUrl.OriginalUrl = url;
                shortUrl.Alias = GenerateAlias(url.OriginalString);
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError($"Обновление короткой ссылки Id:{Id} вызвало исключение DbUpdateException: {e.ToString()}");
                return false;
            }
            catch (OperationCanceledException e)
            {
                _logger.LogError($"Обновление короткой ссылки Id:{Id} вызвало исключение OperationCanceledException: {e.ToString()}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Генерирование псевдонима ссылки (хеш MD5)
        /// </summary>
        /// <param name="Url">Исходный Url</param>
        /// <returns>Псевдоним ссылки</returns>
        private string GenerateAlias(string Url)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(Url);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }

        /// <summary>
        /// Создание объекта Uri из строки
        /// </summary>
        /// <param name="Url">Строка сождержащия адрес Uri</param>
        /// <returns>Созданный Uri</returns>
        private Uri? CreateUrl(string Url)
        {
            bool result = Uri.TryCreate(Url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result ? uriResult : null;
        }

    }
}
