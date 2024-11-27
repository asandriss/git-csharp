namespace codecrafters_git;

internal class InitCommand : IGitCommand
{
    public string Execute(IEnumerable<string> args)
    {
        CreateGitDirectories();
        InitHead();
        return "Initialized git directory\n";
    }

    private static void InitHead()
    {
        File.WriteAllText(".git/HEAD", "ref: refs/heads/main\n");
    }

    private static void CreateGitDirectories()
    {
        Directory.CreateDirectory(".git");
        Directory.CreateDirectory(".git/objects");
        Directory.CreateDirectory(".git/refs");
    }
}