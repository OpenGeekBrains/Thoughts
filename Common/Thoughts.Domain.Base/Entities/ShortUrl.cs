using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thoughts.Domain.Base.Entities
{
    public class ShortUrl : EntityModel
    {
        /// <summary> Оригинальный URL </summary>
        [Required]
        public Uri OriginalUrl { get; set; }

        /// <summary> Псевдоним ссылки </summary>
        [Required]
        public string Alias { get; set; }

        /// <summary> Количество запросов ссылки </summary>
        [Required]
        public int Statistic { get; set; }

        /// <summary> Дата и время последего сброса статистики </summary>
        [Required]
        public DateTimeOffset LastReset { get; set; }
    }
}
