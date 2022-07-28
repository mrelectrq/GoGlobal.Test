using GoGlobal.Test.Domain.Models;

namespace GoGlobal.Test.Domain;

public class GitlabRepositoryParser : IRepositoryJsonParser
{
    public IEnumerable<IRepositoryInfo> Parse(string data)
    {
        throw new NotImplementedException();
    }
}