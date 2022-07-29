using GoGlobal.Test.Backend.Filters;
using GoGlobal.Test.Backend.Services;
using GoGlobal.Test.Backend.Services.Concrete;
using GoGlobal.Test.Backend.Services.Interfaces;
using GoGlobal.Test.Data;
using GoGlobal.Test.Domain;
using GoGlobal.Test.Domain.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
builder.Services.AddScoped(typeof(IMemoryCacheService<>), typeof(MemoryCacheService<>));
builder.Services.AddScoped<IAuthMemoryCacheService, AuthCacheService>();
builder.Services.AddAuthentication(m =>
{
    m.DefaultScheme = "CustomAuth";
}).AddScheme<ValidateHashAuthenticationSchemeOptions,AuthenticationFilter>("CustomAuth",null);
builder.Services.AddLogging()
    .AddLogging(m=> m.AddConsole())
    .BuildServiceProvider();
builder.Services.AddControllers();
string provider = "github";
builder.Services.AddTransient<GitlabRepositoryParser>();
builder.Services.AddTransient<GithubRepositoryParser>();
builder.Services.AddTransient<RepositoryProvider<IRepositoryInfo>>(serviceProvider =>
{
    if (provider == "gitlab")
    {
        return new GitlabRepositoryProvider(serviceProvider.GetRequiredService<GitlabRepositoryParser>());
    }
    else if (provider == "github")
    {
        return new GithubRepositoryProvider(serviceProvider.GetRequiredService<GithubRepositoryParser>());
    }
    else
    {
        throw new Exception("InvalidProvider");
    }
});
builder.Services.AddTransient<TestDbContext>();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMailSender>(opt =>
{
    return new EmailService("smtp-mail.outlook.com", "587");
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(m =>
    {
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test GoGlobal");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();