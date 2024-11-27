namespace codecrafters_git;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

public class CatFileCommand : IGitCommand
{
    public string Execute(IEnumerable<string> args)
    {
        if (args.Count() != 2) throw new ArgumentException("Please provide action and hash");
        if (args.First() != "-p") throw new ArgumentException("Please provide path");

        return CatBlob(args.Skip(1).First()).FirstOrDefault();
    }

    private IEnumerable<string> CatBlob(string blobHash)
    {
        var fileRoot = AppDomain.CurrentDomain.BaseDirectory;
        var file = GetFileLocationFromHash(fileRoot, blobHash);

        foreach (var line in ProcessDecompressedFile(file))
        {
            yield return line;
        }
    }

    public static IEnumerable<string> ProcessDecompressedFile(string inputFilePath)
    {
        byte[] compressedData = File.ReadAllBytes(inputFilePath);
        byte[] rawDeflateData = compressedData[2..^4];

        using var rawDataStream = new MemoryStream(rawDeflateData);
        using var deflateStream = new DeflateStream(rawDataStream, CompressionMode.Decompress);
        using var reader = new StreamReader(deflateStream, Encoding.UTF8);

        while (reader.ReadLine() is { } line)
        {
            yield return line;
        }
    }

    public string GetFileLocationFromHash(string projectDir, string blobHash)
    {
        return Path.Combine(projectDir, ".git/objects", blobHash[..2], blobHash[2..]);
    }
}