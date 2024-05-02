using DigestAuthDemo.Common;

IHttpBinService service = new ClientBasedHttpBinService();

try
{
    var response = await service.GetData();
    Console.WriteLine(response);
}
catch (HttpRequestException exc)
{
    Console.WriteLine($"Error, status code: {exc.StatusCode}");
}



