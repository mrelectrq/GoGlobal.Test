namespace GoGlobal.Test.Domain;


public abstract class RepositoryProvider<T>
{
    protected IRepositoryJsonParser Parser;
    public RepositoryProvider(IRepositoryJsonParser parser)
    {
        Parser = parser;
    }
    public abstract Task<IEnumerable<T>> GetRepositories(string repository);
}