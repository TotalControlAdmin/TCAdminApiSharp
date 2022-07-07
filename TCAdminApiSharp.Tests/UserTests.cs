using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using TCAdminApiSharp.Entities.User;
using TCAdminApiSharp.Querying;
using TCAdminApiSharp.Querying.Operations;

namespace TCAdminApiSharp.Tests;

public class UserTests
{
    private TcaClient _tcaClient;
        
    [SetUp]
    public void Setup()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.local.json", true)
            .Build().GetSection("TCAdmin");
        var activeProfile = config["ActiveProfile"];
        var profileSection = config.GetSection("Profiles").GetSection(activeProfile);
        var host = profileSection["Host"];
        var key = profileSection["Key"];
        _tcaClient = new TcaClient(host, key, TcaClientSettings.Debug);
    }

    [Test]
    public async Task GetUserAdvancedTest()
    {
        var users = await _tcaClient.UsersController.FindUsers(new QueryableInfo(new WhereList(nameof(User.UserName), "admin")));
        Assert.AreEqual(1, users.Count);
        var user = users.Result.First();
        Assert.AreEqual("Admin", user.UserName);
    }
    
    [Test]
    public async Task GetUserTest()
    {
        var user = await _tcaClient.UsersController.GetUser(3);
        Assert.AreEqual("Admin", user.UserName);
    }
    
    [Test]
    public async Task UpdateUserTest()
    {
        var user = await _tcaClient.UsersController.GetUser(4);
        await user.Update(x =>
        {
            x.FirstName = "Api";
            x.LastName = "Update";
        });
        user = await _tcaClient.UsersController.GetUser(4);
        Assert.AreEqual("Api", user.FirstName);
        Assert.AreEqual("Update", user.LastName);
    }
}