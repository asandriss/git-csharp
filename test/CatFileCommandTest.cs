using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace codecrafters_git.Tests;

public class CatFileCommandTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public CatFileCommandTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private const string DirectoryRoot = "/";
    
    [Theory]
    [InlineData("08fsa253d31f", "/.git/objects/08/fsa253d31f")]
    public void GetFileFromHash_ShouldReturnCorrectFilePath(string hash, string expect)
    {
        CatFileCommand cmd = new();
        
        var actual = cmd.GetFileLocationFromHash(DirectoryRoot, hash);
        actual.Should().Be(expect);
    }

    [Fact]
    public void ReadBlob_ItExists()
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test/data", "blob");
        
        File.Exists(filePath).Should().BeTrue();
    }

    [Fact]
    public void ReadBlob_HasCorrectContent()
    {
        var blobContent = "blob 13\0hello, world!";
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test/data", "blob");
        var actualFirstLine = CatFileCommand.ProcessDecompressedFile(filePath).FirstOrDefault();

        actualFirstLine.Should().Be(blobContent);
    }
}