using RestSharp;
using System.Text.Json;

namespace OpenIdGateway.Core.Helper
{
    public static class LogHelper
    {
        public static string CreateLogItem(string request, string response, string clientId, string methodName, string secretId)
        {

            var result = string.Format("ClientId:{0} , Method: {1} , SecretId: {2}  , Request:{3} ,  Response : {4}", clientId, methodName, secretId, request, response);

            return result;

        }

    }
}
