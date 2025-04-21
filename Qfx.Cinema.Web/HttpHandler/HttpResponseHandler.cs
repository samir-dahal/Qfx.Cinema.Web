
using Qfx.Cinema.Web.Extensions;
using System.Text;

namespace Qfx.Cinema.Web.HttpHandler
{
    public class HttpResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (response.Content?.Headers?.ContentType?.MediaType != "application/json")
            {
                return response;
            }
            // Parse the JSON response
            string returnString = await response.Content.ReadAsStringAsync();
            var jsonObject = returnString.DeserializeAnonymousType(new { encryptedData = string.Empty, status = false });
            // Check if the expected fields exist
            if (jsonObject?.encryptedData == null)
            {
                return response;
            }
            // Decrypt the data
            string decryptedData = jsonObject.encryptedData.DecryptPayload();
            // Create new response with decrypted data
            var newResponse = new HttpResponseMessage(response.StatusCode)
            {
                Content = new StringContent(decryptedData, Encoding.UTF8, "application/json")
            };
            // Copy original headers
            foreach (var header in response.Headers)
            {
                newResponse.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            return newResponse;
        }
    }
}
