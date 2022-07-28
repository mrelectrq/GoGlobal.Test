using GoGlobal.Test.Domain.Models;

namespace GoGlobal.Test.Domain;

public interface IRepositoryJsonParser
{
    public IEnumerable<IRepositoryInfo> Parse(string data);
}