using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GoGlobal.Test.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoGlobal.Test.Domain;

public class GithubRepositoryParser : IRepositoryJsonParser
{
    public GithubRepositoryParser()
    {
        
    }
    public IEnumerable<IRepositoryInfo> Parse(string data)
    {
        var result = JsonConvert.DeserializeObject<JToken>(data);
        var contentArray = result["items"];
        List<GithubRepositoryInfo> items = new List<GithubRepositoryInfo>();

        Parallel.ForEach(contentArray, x =>
        {
            items.Add(new GithubRepositoryInfo()
            {
                RepositoryName = (string)x["full_name"],
                Avatar = (string)x["owner"].SelectToken("avatar_url").ToString(),
                RepositoryDescription = (string)x["description"]
            });
        });
        return items;
    }
}