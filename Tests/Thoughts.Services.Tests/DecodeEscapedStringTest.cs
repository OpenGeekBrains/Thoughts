using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Thoughts.Services.Tests;

[TestClass]
public class DecodeEscapedStringTest
{
    [TestMethod]
    public void DecodeTest()
    {
        const string source_string = "Body6. Aenean ornare velit lacus, ac varius enim lorem "
            + "ullamcorper dolore. Proin aliquam facilisis ante interdum. Sed nulla amet lorem "
            + "feugiat tempus aliquam.&lt;br/&gt;&lt;img src=&quot;"
            + "https://catholicsar.ru/wp-content/uploads/1516116812_ryzhiy-maine-coon.jpg&quot; "
            + "alt=&quot;&#x43A;&#x430;&#x440;&#x442;&#x438;&#x43D;&#x43A;&#x430;&quot;/&gt;";

        var regex = new Regex(@"&[^;]+;");

        var result = regex.Replace(source_string, match =>
        {
            return match.Value.TrimStart('&').TrimEnd(';') switch
            {
                "lt" => "<",
                "gt" => ">",
                "quot" => "\"",
                { } s when s.StartsWith("#x") => DecodeSymbol(short.Parse(match.Value[2..], NumberStyles.HexNumber)),
                _ => ""
            };
        });

        static string DecodeSymbol(short Code)
        {
            var c = (char)((Code - 0x43A) + 'а');
            return c.ToString();
        }

        const string expected_string = "Body6. Aenean ornare velit lacus, ac varius enim lorem "
            + "ullamcorper dolore. Proin aliquam facilisis ante interdum. Sed nulla amet lorem "
            + "feugiat tempus aliquam.<br/><img src=\""
            + "https://catholicsar.ru/wp-content/uploads/1516116812_ryzhiy-maine-coon.jpg\" "
            + "alt=\"картинка\"/>";

        Assert.Equals(expected_string, result);
    }
}