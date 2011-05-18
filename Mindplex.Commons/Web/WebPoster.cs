
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
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.Xml;

using Mindplex.Commons.Model;

using log4net;

#endregion

namespace Mindplex.Commons.Web
{
    /// <summary>
    /// Submits post data to a url.
    /// </summary>
    /// 
    [Obsolete("InternetBrands.Core.Web.WebPoster is obsolete, please use InternetBrands.Core.Web.HttpGateway.")]
    public class WebPoster : IWebPoster
    {
        /// <summary>
        /// Gets the instance of type WebPoster ILogger
        /// </summary>
        /// 
        private static ILog logger = LogManager.GetLogger(typeof(WebPoster));

        /// <summary>
        /// Post URL
        /// </summary>
        /// 
        private string postUrl = string.Empty;

        /// <summary>
        /// Post Values
        /// </summary>
        /// 
        private NameValueCollection postValues = new NameValueCollection();

        /// <summary>
        /// Method of Post (Get or Post)
        /// </summary>
        /// 
        private PostType postType;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private int timeout = 500;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private NetworkCredential credential;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// 
        public WebPoster()
        {
        }

        /// <summary>
        /// Constructor that accepts a url as a parameter
        /// </summary>
        /// 
        /// <param name="url">The url where the post will be submitted to.</param>
        /// 
        public WebPoster(string url)
            : this()
        {
            postUrl = url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="url"></param>
        /// <param name="credential"></param>
        /// 
        public WebPoster(string url, NetworkCredential credential)
            : this()
        {
            postUrl = url;
            this.credential = credential;
        }

        /// <summary>
        /// Constructor allowing the setting of the url and items to post.
        /// </summary>
        /// 
        /// <param name="url">the url for the post.</param>
        /// <param name="values">The values for the post.</param>
        /// 
        public WebPoster(string url, NameValueCollection values)
            : this(url)
        {
            postValues = new NameValueCollection(values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="url"></param>
        /// <param name="values"></param>
        /// <param name="credential"></param>
        /// 
        public WebPoster(string url, NameValueCollection values, NetworkCredential credential)
            : this(url, values)
        {
            this.credential = credential;
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
        /// Gets or sets the url to submit the post to.
        /// </summary>
        /// 
        public string Url
        {
            get
            {
                return postUrl;
            }
            set
            {
                postUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets the name value collection of items to post.
        /// </summary>
        /// 
        public NameValueCollection PostItems
        {
            get
            {
                return new NameValueCollection(postValues);
            }
            set
            {
                postValues = new NameValueCollection(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// 
        public void AddPostItem(string name, string value)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(value))
            {
                throw new Exception("Invalid argument: name or value cannot be empty or null.");
            }

            postValues.Add(name, value);
        }

        /// <summary>
        /// Posts the supplied data to specified url.
        /// </summary>
        /// 
        /// <returns>a string containing the result of the post.</returns>
        /// 
        public string Post(PostType postMethod)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat("Post method is: {0}.", postMethod);
            }
            postType = postMethod;
            string result;
            try
            {
                StringBuilder parameters = new StringBuilder();
                for (int i = 0; i < postValues.Count; i++)
                {
                    EncodeAndAddItem(ref parameters, postValues.GetKey(i), postValues[i]);
                }

                if (logger.IsDebugEnabled)
                {
                    logger.DebugFormat("key/values: {0}.", parameters.ToString());
                }
                result = PostData(postUrl, parameters.ToString());
            }
            catch (Exception exception)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Warn(string.Format("Failed to Post to URL: {0}.", Url), exception);
                }
                throw new Exception(string.Format("Failed to Post to URL: {0}.", this.Url), exception);
            }
            return result;
        }

        /// <summary>
        /// Posts data to a specified url. Note that this assumes that you have already url encoded the post data.
        /// </summary>
        /// 
        /// <param name="postData">The data to post.</param>
        /// <param name="url">the url to post to.</param>
        /// <returns>Returns the result of the post.</returns>
        /// 
        private string PostData(string url, string postData)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat("URL: {0}", Url);
            }
            HttpWebRequest request = null;
            if (postType == PostType.Post)
            {
                Uri uri = new Uri(url);
                request = (HttpWebRequest)WebRequest.Create(uri);
                if (credential != null)
                {
                    request.Credentials = new CredentialCache();
                    ((CredentialCache)request.Credentials).Add(uri, "Basic", credential);
                }
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postData.Length;
                request.Timeout = timeout; 
                using (Stream writeStream = request.GetRequestStream())
                {
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] bytes = encoding.GetBytes(postData);
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {
                Uri uri;
                if (postData.Equals(string.Empty))
                {
                    uri = new Uri(url);
                }
                else
                {
                    uri = new Uri(url + postData);
                }
                logger.Info("Post to URL: " + url.ToString());
                request = WebRequest.Create(uri) as HttpWebRequest;
                request.Timeout = timeout; 
                if (credential != null)
                {
                    request.Credentials = new CredentialCache();
                    ((CredentialCache)request.Credentials).Add(uri, "Basic", credential);
                }
                request.Method = "GET";
                request.Referer = "https://www.carsdirect.com";
                request.AllowAutoRedirect = true;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.72;Linux)";
            }
            
            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        result = readStream.ReadToEnd();
                    }
                }
            }

            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat("Post result: {0}.", result);
            }
            return result;
        }

        /// <summary>
        /// Encodes an item and ads it to the string.
        /// </summary>
        /// 
        /// <param name="baseRequest">The previously encoded data.</param>
        /// <param name="key">The key for data</param>
        /// <param name="dataItem">The data to encode.</param>
        /// <returns>A string containing the old data and the previously encoded data.</returns>
        /// 
        private void EncodeAndAddItem(ref StringBuilder baseRequest, string key, string dataItem)
        {
            if (baseRequest == null)
            {
                baseRequest = new StringBuilder();
            }
            if (baseRequest.Length != 0)
            {
                baseRequest.Append("&");
            }
            baseRequest.Append(key);
            baseRequest.Append("=");
            baseRequest.Append(System.Web.HttpUtility.UrlEncode(dataItem));
        }
    }
}