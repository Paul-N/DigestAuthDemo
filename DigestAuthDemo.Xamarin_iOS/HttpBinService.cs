using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
#if __IOS__
using Foundation;
#endif

namespace DigestAuthDemo.Common
{
    public interface IHttpBinService
    {
        Task<string> GetData();
    }

    internal class Consts
    {
        public const string Url = "https://httpbin.org/digest-auth/undefined/usr/pwd";

        public const string User = "usr";

        public const string Password = "pwd";
    }

    public class ClientBasedHttpBinService : IHttpBinService
    {
        private readonly HttpClient _httpClient;

        public ClientBasedHttpBinService()
        {
            var handler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential(Consts.User, Consts.Password),
            };
            _httpClient = new HttpClient(handler);

        }

        public async Task<string> GetData()
            => await _httpClient.GetStringAsync(Consts.Url);
    }

#if __IOS__

    public class NSUrlSessionBasedHttpBinService : NSObject, IHttpBinService, INSUrlSessionTaskDelegate
    {
        public async Task<string> GetData()
        {
            var tcs = new TaskCompletionSource<string>();

            var request = new NSMutableUrlRequest(new NSUrl(Consts.Url))
            {
                HttpMethod = "GET"
            };

            var task = NSUrlSession.SharedSession.CreateDataTask(request, (data, response, error) =>
            {
                var str = new NSString(data, NSStringEncoding.UTF8);
                var code = (response as NSHttpUrlResponse).StatusCode;
                if (code >= 200 || code < 300)
                    tcs.SetResult(new NSString(data, NSStringEncoding.UTF8));
                else
                {
                    var exc = new Exception("Request failed");
                    exc.Data.Add("StatusCode", response);
                    tcs.SetException(exc);
                }
            });
            task.Delegate = this;

            task.Resume();

            return await tcs.Task;
        }

        [Export("URLSession:task:didReceiveChallenge:completionHandler:")]
        public void DidReceiveChallenge(NSUrlSession session, NSUrlSessionTask task, NSUrlAuthenticationChallenge challenge,
            Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler)
        {
            var credential = new NSUrlCredential(user: Consts.User, password: Consts.Password, persistence: NSUrlCredentialPersistence.ForSession);
            completionHandler(NSUrlSessionAuthChallengeDisposition.UseCredential, credential);
        }
    }
#endif
}

