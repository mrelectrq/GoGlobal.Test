namespace GoGlobal.Test.Domain.Models;

public class GitlabRepositoryInfo : IRepositoryInfo
{
    public string RepositoryName { get; set; } = default!;
    public string Avatar { get; set; } = default!;
    public string RepositoryDescription { get; set; } = default!;
}