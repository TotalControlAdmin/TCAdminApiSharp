using System;
using NUnit.Framework;

namespace TCAdminApiSharp.Tests;

public class ClientTests
{
    [Test]
    public void CreateClientWithoutHost()
    {
        Assert.Catch<ArgumentException>(() =>
        {
            var _ = new TcaClient("", "-");
        });
    }
    
    [Test]
    public void CreateClientWithoutToken()
    {
        Assert.Catch<ArgumentException>(() =>
        {
            var _ = new TcaClient("https://4a815e4e-aaa8-4351-a948-5ca3f92e5c8b.mock.pstmn.io", "");
        });
    }
}