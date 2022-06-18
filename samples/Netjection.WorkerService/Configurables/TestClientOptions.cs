namespace Netjection.WorkerService.Configurables;

#pragma warning disable CS8618
[Configure("TestClient")]
public class TestClientOptions
{
    public string Url { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}