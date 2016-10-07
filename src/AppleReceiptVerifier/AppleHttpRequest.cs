using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AppleReceiptVerifier.Interfaces;

namespace AppleReceiptVerifier
{
    /// <summary>
    /// Apple Http Request
    /// </summary>
    internal class AppleHttpRequest : IAppleHttpRequest
    {
        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="postData">The data to be posted.</param>
        /// <returns>
        /// response as string
        /// </returns>
        public string GetResponse(Uri url, string postData)
        {
            string response = string.Empty;

            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.ContentType = "text/plain";
                webRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    streamWriter.Write(postData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                WebResponse webResponse = webRequest.GetResponse();
                using (var streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }
            catch
            {
            }

            return response;
        }

        public async Task<string> GetResponseAsync(Uri url, string postData)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "text/plain";
            webRequest.Method = "POST";

            Stream requestStream = await webRequest.GetRequestStreamAsync();
            using (var streamWriter = new StreamWriter(requestStream))
            {
                await streamWriter.WriteAsync(postData);
                streamWriter.Flush();
                // streamWriter.Close(); // Not required when using "using" block - automatically closed on Dispose
            }

            HttpWebResponse webResponse = (HttpWebResponse)await webRequest.GetResponseAsync();
            Stream responseStream = webResponse.GetResponseStream();

            using (var streamReader = new StreamReader(responseStream))
            {
                return await streamReader.ReadToEndAsync();
                // streamReader.Close(); // Not required when using "using" block - automatically closed on Dispose
            }
        }
    }
}
