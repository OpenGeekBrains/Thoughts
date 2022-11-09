using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Thoughts.Tools.Extensions;

namespace Thoughts.Tools.Tests.Extensions;

[TestClass]
public class ExtensionsTests
{
    [TestMethod]
    public async Task GetMd5Async_HelloWorld()
    {
        // Arrange
        const string str = "Hello World";

        const string expected_hash_md5 = "b10a8db164e0754105b7a99be72e3fe5";

        // Act
        var actual_hash_md5 = await str.GetMd5Async();

        // Assert
        Assert.AreEqual(expected_hash_md5, actual_hash_md5);
    }

    [TestMethod]
    public async Task GetSha1Async_HelloWorld()
    {
        // Arrange
        const string str = "Hello World";

        const string expected_hash_md5 = "0a4d55a8d778e5022fab701977c5d840bbc486d0";

        // Act
        var actual_hash_sha1 = await str.GetSha1Async();

        // Assert
        Assert.AreEqual(expected_hash_md5, actual_hash_sha1);
    }
}