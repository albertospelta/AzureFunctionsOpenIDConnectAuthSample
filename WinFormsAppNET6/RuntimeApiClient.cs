namespace WinFormsAppNET6
{
    using System;
    using System.Globalization;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class RuntimeApiClient
    {
        const string BaseUrl = "http://localhost:7071/api";

        private string _accessToken;
        private HttpClient _httpClient;
        private System.Text.Json.JsonSerializerOptions JsonSerializerSettings = new System.Text.Json.JsonSerializerOptions();

        public RuntimeApiClient(string accessToken)
        {
            _accessToken = accessToken;
            _httpClient = new HttpClient();
        }

        private void PrepareRequest(HttpClient client, HttpRequestMessage request /*, StringBuilder urlBuilder*/)
        {
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(string.Format(CultureInfo.InvariantCulture, "Bearer {0}", _accessToken));
        }

        public async Task HelloFunctionAsync(CancellationToken cancellationToken = default)
        {
            var client_ = _httpClient;

            using var request_ = new HttpRequestMessage();
            request_.Method = new HttpMethod("GET");
            request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            request_.RequestUri = new Uri("http://localhost:7071/api/HelloFunction", UriKind.RelativeOrAbsolute);

            PrepareRequest(client_, request_ /*, urlBuilder_*/);

            using var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            var status_ = response_.StatusCode;
        }

    //    public async System.Threading.Tasks.Task<Account> Account_GetAccountAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
    //    {
    //        var urlBuilder_ = new System.Text.StringBuilder();
    //        urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/v1/account");

    //        var client_ = _httpClient;
    //        var disposeClient_ = false;
    //        try
    //        {
    //            using (var request_ = new System.Net.Http.HttpRequestMessage())
    //            {
    //                request_.Method = new System.Net.Http.HttpMethod("GET");
    //                request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

    //                PrepareRequest(client_, request_ /*, urlBuilder_*/);

    //                var url_ = urlBuilder_.ToString();
    //                request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

    //                //PrepareRequest(client_, request_, url_);

    //                var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
    //                var disposeResponse_ = true;
    //                try
    //                {
    //                    var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
    //                    if (response_.Content != null && response_.Content.Headers != null)
    //                    {
    //                        foreach (var item_ in response_.Content.Headers)
    //                            headers_[item_.Key] = item_.Value;
    //                    }

    //                    //ProcessResponse(client_, response_);

    //                    var status_ = (int)response_.StatusCode;
    //                    if (status_ == 200)
    //                    {
    //                        var objectResponse_ = await ReadObjectResponseAsync<Account>(response_, headers_, cancellationToken).ConfigureAwait(false);
    //                        if (objectResponse_.Object == null)
    //                        {
    //                            throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
    //                        }
    //                        return objectResponse_.Object;
    //                    }
    //                    else
    //                    if (status_ == 400)
    //                    {
    //                        string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
    //                        throw new ApiException("Failed operation", status_, responseText_, headers_, null);
    //                    }
    //                    else
    //                    if (status_ == 401)
    //                    {
    //                        string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
    //                        throw new ApiException("Unauthorized operation", status_, responseText_, headers_, null);
    //                    }
    //                    else
    //                    {
    //                        var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
    //                        throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
    //                    }
    //                }
    //                finally
    //                {
    //                    if (disposeResponse_)
    //                        response_.Dispose();
    //                }
    //            }
    //        }
    //        finally
    //        {
    //            if (disposeClient_)
    //                client_.Dispose();
    //        }
    //    }

    //    protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Threading.CancellationToken cancellationToken)
    //    {
    //        if (response == null || response.Content == null)
    //        {
    //            return new ObjectResponseResult<T>(default(T)!, string.Empty);
    //        }

    //        if (ReadResponseAsString)
    //        {
    //            var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    //            try
    //            {
    //                var typedBody = System.Text.Json.JsonSerializer.Deserialize<T>(responseText, JsonSerializerSettings);
    //                return new ObjectResponseResult<T>(typedBody!, responseText);
    //            }
    //            catch (System.Text.Json.JsonException exception)
    //            {
    //                var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
    //                throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
    //            }
    //        }
    //        else
    //        {
    //            try
    //            {
    //                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
    //                {
    //                    var typedBody = await System.Text.Json.JsonSerializer.DeserializeAsync<T>(responseStream, JsonSerializerSettings, cancellationToken).ConfigureAwait(false);
    //                    return new ObjectResponseResult<T>(typedBody!, string.Empty);
    //                }
    //            }
    //            catch (System.Text.Json.JsonException exception)
    //            {
    //                var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
    //                throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
    //            }
    //        }
    //    }

    //    protected struct ObjectResponseResult<T>
    //    {
    //        public ObjectResponseResult(T responseObject, string responseText)
    //        {
    //            this.Object = responseObject;
    //            this.Text = responseText;
    //        }

    //        public T Object { get; }

    //        public string Text { get; }
    //    }

    //    public bool ReadResponseAsString { get; set; }
    }

    //[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v13.0.0.0))")]
    //public partial class Account
    //{
    //    /// <summary>
    //    /// Tenant identifier
    //    /// </summary>

    //    [System.Text.Json.Serialization.JsonPropertyName("tenantId")]

    //    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    //    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    //    public string TenantId { get; set; } = default!;

    //    /// <summary>
    //    /// Account identifier
    //    /// </summary>

    //    [System.Text.Json.Serialization.JsonPropertyName("id")]

    //    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    //    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    //    public string Id { get; set; } = default!;

    //    /// <summary>
    //    /// Account personal workspace identifier
    //    /// </summary>

    //    [System.Text.Json.Serialization.JsonPropertyName("personalWorkspaceId")]

    //    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    //    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    //    public string PersonalWorkspaceId { get; set; } = default!;

    //    /// <summary>
    //    /// Account email
    //    /// </summary>

    //    [System.Text.Json.Serialization.JsonPropertyName("email")]

    //    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    //    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    //    public string Email { get; set; } = default!;

    //    /// <summary>
    //    /// Account type
    //    /// </summary>

    //    [System.Text.Json.Serialization.JsonPropertyName("type")]

    //    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    //    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    //    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    //    public AccountType Type { get; set; } = default!;

    //    /// <summary>
    //    /// Account claims identity
    //    /// </summary>

    //    [System.Text.Json.Serialization.JsonPropertyName("claimsIdentity")]

    //    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
    //    public System.Collections.Generic.ICollection<string>? ClaimsIdentity { get; set; } = default!;

    //}

    //[System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v13.0.0.0))")]
    //public enum AccountType
    //{

    //    [System.Runtime.Serialization.EnumMember(Value = @"Login")]
    //    Login = 0,

    //    [System.Runtime.Serialization.EnumMember(Value = @"Virtual")]
    //    Virtual = 1,

    //}

    //[System.CodeDom.Compiler.GeneratedCode("NSwag", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v13.0.0.0))")]
    //public partial class ApiException : System.Exception
    //{
    //    public int StatusCode { get; private set; }

    //    public string? Response { get; private set; }

    //    public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

    //    public ApiException(string message, int statusCode, string? response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception? innerException)
    //        : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
    //    {
    //        StatusCode = statusCode;
    //        Response = response;
    //        Headers = headers;
    //    }

    //    public override string ToString()
    //    {
    //        return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
    //    }
    //}
}
