using Newtonsoft.Json;
using RepoLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Repositories.Measurements
{
    public class GenericRepository : IGenericRepository
    {
        private HttpClient httpClient;

        private JsonSerializerSettings mysettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public GenericRepository(HttpClient client)
        {
            httpClient = client;
        }

        #region GET
        public async Task<T> GetAsync<T>(string uri, string authToken = "")
        {
            try
            {
                string myJsonResponse = string.Empty;

                HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);

                if (responseMessage.IsSuccessStatusCode)
                {
                    myJsonResponse = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var myDeserializedClass = JsonConvert.DeserializeObject<T>(myJsonResponse, mysettings);

                    return myDeserializedClass;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(myJsonResponse);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, myJsonResponse);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
        #endregion

        #region POST
        public async Task<T> PostAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                ConfigureHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await httpClient.PostAsync(uri, content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<TR> PostAsync<T, TR>(string uri, T data, string authToken = "")
        {
            try
            {
                ConfigureHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await httpClient.PostAsync(uri, content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<TR>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region PUT
        public async Task<T> PutAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                ConfigureHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await httpClient.PutAsync(uri, content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized ||
                    responseMessage.StatusCode == HttpStatusCode.MethodNotAllowed)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region DELETE
        public async Task DeleteAsync(string uri, string authToken = "")
        {
            try
            {
                ConfigureHttpClient(authToken);
                await httpClient.DeleteAsync(uri);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region HELPER
        private void ConfigureHttpClient(string authToken)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
        #endregion
    }
}
