using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

//using Assert = Xunit.Assert;

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

        var result0 = HttpUtility.HtmlDecode(source_string);
        var result = regex.Replace(source_string, match =>
        {
            switch (match.Value.TrimStart('&').TrimEnd(';'))
            {
                case "lt": return "<";
                case "gt": return ">";
                case "quot": return "\"";
                case { } s when s.StartsWith("#x"):
                    var v = match.Value[3..^1];
                    return DecodeSymbol(short.Parse(v, NumberStyles.HexNumber));
                default: return "";
            }
        });

        static string DecodeSymbol(short Code)
        {
            var c = (char)(Code - 0x430 + 'а');
            return c.ToString();
        }

        const string expected_string = "Body6. Aenean ornare velit lacus, ac varius enim lorem "
            + "ullamcorper dolore. Proin aliquam facilisis ante interdum. Sed nulla amet lorem "
            + "feugiat tempus aliquam.<br/><img src=\""
            + "https://catholicsar.ru/wp-content/uploads/1516116812_ryzhiy-maine-coon.jpg\" "
            + "alt=\"картинка\"/>";

        Assert.AreEqual(expected_string, result);
    }
}