﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Thoughts.DAL.Entities.Base;

namespace Thoughts.DAL.Entities
{
    public  class ShortUrl:Entity
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
