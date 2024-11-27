namespace codecrafters_git;

internal interface IGitCommand
{
    string Execute(IEnumerable<string> args);
}