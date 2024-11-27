using System;
using System.IO;
using System.Linq.Expressions;
using codecrafters_git;

if (args.Length < 1)
{
    Console.WriteLine("Please provide a command.");
    return;
}

string command = args[0];

IGitCommand cmd = command switch
{
     "init" => new InitCommand(),
     _ => throw new ArgumentException(),
};
