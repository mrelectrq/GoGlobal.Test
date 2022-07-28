using GoGlobal.Test.Domain.Models;

namespace GoGlobal.Test.Domain;

public class GitlabRepositoryProvider : RepositoryProvider<IRepositoryInfo>
{
    
    public GitlabRepositoryProvider(IRepositoryJsonParser parser) : base(parser)
    {
    }
    public override Task<IEnumerable<IRepositoryInfo>> GetRepositories(string repository)
    {
        throw new NotImplementedException();
    }
}