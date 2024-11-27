using System;
using System.IO;
using System.Linq.Expressions;
using codecrafters_git;

if (args.Length < 1)
{
    Console.WriteLine("Please provide a command.");
    return;
}

var command = args.FirstOrDefault();

IGitCommand cmd = command switch
{
     "init" => new InitCommand(),
     "cat-file" => new CatFileCommand(),
     _ => throw new ArgumentException("Please provide a command."),
};

var result = cmd.Execute(args.Skip(1));
Console.Write(result);