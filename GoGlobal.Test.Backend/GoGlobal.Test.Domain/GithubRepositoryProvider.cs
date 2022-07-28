using System.Net.Http.Headers;
using System.Text;
using GoGlobal.Test.Domain.Models;

namespace GoGlobal.Test.Domain;

public class GithubRepositoryProvider : RepositoryProvider<IRepositoryInfo>
{
    private readonly HttpClient _client;

    public override async Task<IEnumerable<IRepositoryInfo>> GetRepositories(string repository)
    {
        var repoURL = "https://api.github.com/search/repositories?q=";
        _client.DefaultRequestHeaders.Add("User-Agent",
            "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(repoURL).Append(repository);
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(urlBuilder.ToString(), UriKind.RelativeOrAbsolute);

                var response = await _client
                    .SendAsync(request, System.Net.Http.HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                var status = (int)response.StatusCode;
                if (status == 404) throw new Exception("Response was null which was not expected.");
                else if (status == 200)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var fields = Parser.Parse(content);
                    return fields;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return null;
    }

    public GithubRepositoryProvider(IRepositoryJsonParser parser) : base(parser)
    {
        _client = new HttpClient();
    }
}