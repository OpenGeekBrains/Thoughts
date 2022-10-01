using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thoughts.Interfaces.Base
{
    static public class WebApiControllersPath
    {
        public const string ShortUrl = "api/v{version:apiVersion}/url";

        public const string ShortUrlV1 = "api/v1/url";
    }
}
