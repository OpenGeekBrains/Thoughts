using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Thoughts.Interfaces;
using Thoughts.WebAPI.Clients.Base;

namespace Thoughts.WebAPI.Clients.BlogPost
{
    public class BlogPostClient : BaseClient//, IBlogPostManager
    {
        protected BlogPostClient(HttpClient client, ILogger<BlogPostClient> logger) : base(client, "")
        {
        }
    }
}
