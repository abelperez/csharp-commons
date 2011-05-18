
// Copyright (C) 2011 Mindplex Media, LLC.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
// file except in compliance with the License. You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, either express or implied. See the License for the
// specific language governing permissions and limitations under the License.

#region Imports

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

using log4net;

#endregion

namespace Mindplex.Commons.Web
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class HttpGateway : IHttpGateway
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private static ILog logger = LogManager.GetLogger(typeof(HttpGateway));

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly int DEFAULT_TIMEOUT = 30000;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly string DEFAULT_AUTH_METHOD = "Basic";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly string DEFAULT_APPLICATION_TYPE = "application/x-www-form-urlencoded";
                                                                                                
        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly string POST = "POST";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly string GET = "GET";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private const int bufferSize = 8192;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string url;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string verb;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private int timeout;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string authMethod;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string username;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string password;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string contentType;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string[] headers;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private HttpGateway()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="url"></param>
        /// 
        /// <returns></returns>
        /// 
        public static HttpGateway CreateGET(string url)
        {
            return Create(url, DEFAULT_TIMEOUT, GET);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// 
        /// <returns></returns>
        /// 
        public static HttpGateway CreateGET(string url, int timeout)
        {
            return Create(url, timeout, GET);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="url"></param>
        /// 
        /// <returns></returns>
        /// 
        public static HttpGateway CreatePOST(string url)
        {
            return Create(url, DEFAULT_TIMEOUT, POST);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// 
        /// <returns></returns>
        /// 
        public static HttpGateway CreatePOST(string url, int timeout)
        {
            return Create(url, timeout, POST);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <param name="verb"></param>
        /// 
        /// <returns></returns>
        /// 
        private static HttpGateway Create(string url, int timeout, string verb)
        {
            HttpGateway result = new HttpGateway();
            result.url = url;
            result.verb = verb;
            result.timeout = timeout;
            result.contentType = DEFAULT_APPLICATION_TYPE;

            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat("creating gateway url: {0}, timeout: {1}, verb = {2}.", url, timeout, verb);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Verb
        {
            get { return verb; }
            set { verb = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string AuthMethod
        {
            get { return authMethod; }
            set { authMethod = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string[] Headers
        {
            get { return headers; }
            set { headers = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="requestPayload"></param>
        /// 
        /// <returns></returns>
        /// 
        public HttpGatewayResponse Request(string requestPayload)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat("request url: {0}.", url);
                logger.DebugFormat("request payload: {0}.", requestPayload);
            }

            if (String.IsNullOrEmpty(authMethod))
            {
                authMethod = DEFAULT_AUTH_METHOD;
                if (logger.IsDebugEnabled)
                {
                    logger.DebugFormat("request auth method: {0}.", authMethod);
                }
            }

            // set credentials
            CredentialCache credentials = new CredentialCache();
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                NetworkCredential credential = new NetworkCredential(username, password);
                credentials.Add(new Uri(url), authMethod, credential);

                if (logger.IsDebugEnabled)
                {
                    logger.DebugFormat("request username: {0}.", username);
                }
            }
            else
            {
                credentials = null;
            }

            int startTime = Environment.TickCount;
            Uri uri = new Uri(url);
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

            if (credentials != null)
            {
                request.Credentials = credentials;
            }

            // SSL support
            ServicePointManager.CertificatePolicy = new CertificatePolicy();

            HttpWebResponse response = null;
            string responsePayload = String.Empty;
            string responseCode = String.Empty;
            string responseContentType = String.Empty;
            string responseError = String.Empty;

            try
            {
                if (headers != null)
                {
                    // set headers
                    foreach (string header in headers)
                    {
                        request.Headers.Add(header);
                        if (logger.IsDebugEnabled)
                        {
                            logger.DebugFormat("request header: {0}.", header);
                        }
                    }
                }

                // set GET/POST verb.
                request.Method = verb;
                request.Timeout = timeout;
                request.ContentType = contentType;
                request.ContentLength = requestPayload.Length;

                if (logger.IsDebugEnabled)
                {
                    logger.DebugFormat("request verb: {0}.", verb);
                    logger.DebugFormat("request timeout: {0}.", timeout);
                }

                // write payload to stream.                                                
                using (Stream writeStream = request.GetRequestStream())
                {
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] bytes = encoding.GetBytes(requestPayload);
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                // get response payload.
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            responsePayload = readStream.ReadToEnd();
                        }
                    }

                    responseCode = response.StatusCode.ToString();
                    responseContentType = response.ContentType;
                }

                if (logger.IsDebugEnabled)
                {
                    logger.DebugFormat("response code: {0}.", responseCode);
                    logger.DebugFormat("response contentType: {0}.", responseContentType);
                    logger.DebugFormat("response payload: {0}.", responsePayload);
                }
            }
            catch (ArgumentOutOfRangeException exception)
            {
                responseError = exception.Message;
                responsePayload = String.Empty;

                if (logger.IsDebugEnabled)
                {
                    logger.DebugFormat("response error: {0}.", responseError);
                    logger.DebugFormat("response payload: {0}.", responsePayload);
                }
            }
            catch (WebException exception)
            {
                responseCode = exception.Status.ToString();
                responseError = exception.Message;

                if (logger.IsDebugEnabled)
                {
                    logger.DebugFormat("response error: {0}.", responseError);
                    logger.DebugFormat("response code: {0}.", responseCode);
                }

                if (exception.Response != null)
                {
                    // get response exception payload.
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            responsePayload = readStream.ReadToEnd();
                            if (logger.IsDebugEnabled)
                            {
                                logger.DebugFormat("response exception payload: {0}.", responsePayload);
                            }
                        }
                    }
                }
            }
            int elapsedTime = (Environment.TickCount - startTime);
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat("request/response time elapsed: {0}.", elapsedTime);
            }
            return new HttpGatewayResponse(url, requestPayload, responsePayload, responseCode, responseError, responseContentType, elapsedTime);
        }
    }
}