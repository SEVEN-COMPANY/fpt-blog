using System.Net.Http;
using System.Threading.Tasks;

namespace FPTBlog.Src.AuthModule.Interface
{
    public interface IGoogleService
    {
        public HttpResponseMessage getDataByAccessToken(string accessToken);
    }

    public interface IGoogleServiceAsync : IGoogleService
    {
        public Task<HttpResponseMessage> getDataByAccessTokenAsync(string accessToken);
    }
}