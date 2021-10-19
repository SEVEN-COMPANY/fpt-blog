using System.Net.Http;
using System.Threading.Tasks;

using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Utils.Interface;

namespace FPTBlog.Src.AuthModule {
    public class GoogleService : IGoogleServiceAsync {
        private readonly IConfig config;
        public GoogleService(IConfig config) {
            this.config = config;
        }
        public HttpResponseMessage getDataByAccessToken(string accessToken) {
            return getDataByAccessTokenAsync(accessToken).GetAwaiter().GetResult();
        }

        public async Task<HttpResponseMessage> getDataByAccessTokenAsync(string accessToken) {
            string uri = $"https://www.googleapis.com/oauth2/v1/userinfo?access_token={accessToken}";

            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(uri);

            return responseMessage;
        }
    }
}
