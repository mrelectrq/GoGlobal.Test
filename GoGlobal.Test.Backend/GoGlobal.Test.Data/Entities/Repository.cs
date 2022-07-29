namespace GoGlobal.Test.Data.Entities;

public class Repository
{
    public Guid RepositoryId { get; set; }
    public string RepositoryName { get; set; } = default!;
    public string Avatar { get; set; } = default!;
    public string RepositoryDescription { get; set; } = default!;
}