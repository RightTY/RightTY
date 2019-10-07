using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCore.BLL
{
    /// <summary>
    /// HttpClientHelper
    /// </summary>
    public class HttpClientHelper<T>
    {
        /// <summary>
        /// 回傳資料
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// IHttpClientFactory
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// httpClient
        /// </summary>
        private  HttpClient HttpClient => _httpClientFactory.CreateClient();
        /// <summary>
        /// 
        /// </summary>
        private HttpRequestMessage Request = new HttpRequestMessage();

        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public HttpClientHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// SendAsync
        /// </summary>
        /// <param name="method">HttpMethod</param>
        /// <param name="requestUri">requestUri</param>
        /// /// <param name="sendData">sendData</param>
        /// <returns></returns>
        public T SendAsync(HttpMethod method, string requestUri,IEnumerable<KeyValuePair<string, string>> sendData=null )
        {
            Request.Method = method;
            if (HttpClient.BaseAddress==null)
            {
                Request.RequestUri =new Uri(requestUri);
            }
            else
            {
                Request.RequestUri = new Uri(HttpClient.BaseAddress+ requestUri);
            }

            if (sendData!=null)
            {
                if (method == HttpMethod.Get)
                {
                    
                    string[] data = sendData.Select(x => x.Key + "=" + x.Value).ToArray();
                    string strData = string.Join("&", data);
                    Request.RequestUri= new Uri(Request.RequestUri.ToString() + "?" + strData);
                }
                else
                {
                    Request.Content = new FormUrlEncodedContent(sendData);
                }
                
            }
            HttpResponseMessage response = HttpClient.SendAsync(Request).Result;
            Data = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            return Data;
        }

        /// <summary>
        /// 設置Headers
        /// </summary>
        /// /// <param name="key">key</param>
        /// /// <param name="value">value</param>
        public void SetHeaders(string key,string value)
        {
            Request.Headers.Add(key, value);
        }

        /// <summary>
        /// 設置Headers
        /// </summary>
        /// <param name="dic">"Dictionary"</param>
        public void SetHeaders(Dictionary<string,string> dic)
        {
            foreach (KeyValuePair<string,string> item in dic)
            {
                Request.Headers.Add(item.Key,item.Value);
            }
        }
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //public void Dispose(bool disposing)
        //{
        //    HttpClient.Dispose();
        //    Request.Dispose();
        //}
    }
}
