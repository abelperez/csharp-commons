
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
using System.Xml;

#endregion

namespace Mindplex.Commons.Web
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class HttpGatewayResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private readonly string url;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private readonly string requestPayload;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private readonly string responsePayload;
        
        /// <summary>
        /// 
        /// </summary>
        /// 
        private readonly string code;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private readonly string error;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private readonly string contentType;
        
        /// <summary>
        /// 
        /// </summary>
        /// 
        private readonly long elapsedTime;
                                
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="url"></param>
        /// <param name="requestPayload"></param>
        /// <param name="responsePayload"></param>
        /// <param name="code"></param>
        /// <param name="error"></param>
        /// <param name="contentType"></param>
        /// <param name="elapsedTime"></param>
        /// 
        public HttpGatewayResponse(string url, string requestPayload, string responsePayload, string code, string error, string contentType, int elapsedTime)
        {
            this.url = url;
            this.requestPayload = requestPayload;
            this.responsePayload = responsePayload;
            this.code = code;
            this.error = error;
            this.contentType = contentType;
            this.elapsedTime = elapsedTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public string ToXml()
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml("<postresult/>");
            XmlElement result = document.DocumentElement;

            // handle error
            if (!String.IsNullOrEmpty(error))
            {
                XmlElement errorXml = document.CreateElement("error");
                errorXml.AppendChild(document.CreateTextNode(error));
                result.AppendChild(errorXml);
            }

            // document structure
            XmlElement codeXml = document.CreateElement("code");
            XmlElement resultType = document.CreateElement("type");
            XmlElement time = document.CreateElement("time");
            XmlElement data = document.CreateElement("data");
            XmlElement urlXml = document.CreateElement("url");
            XmlElement sentXml = document.CreateElement("sent");
            XmlElement sentText = document.CreateElement("sent");
            XmlElement receiveXml = document.CreateElement("received");
            XmlElement receiveText = document.CreateElement("received");

            codeXml.AppendChild(document.CreateTextNode(code));

            if (!String.IsNullOrEmpty(contentType))
            {
                resultType.AppendChild(document.CreateTextNode(contentType));
            }

            urlXml.AppendChild(document.CreateTextNode(url));
            data.AppendChild(urlXml);
            result.AppendChild(codeXml);
            result.AppendChild(resultType);
            result.AppendChild(data);
            result.AppendChild(time);

            XmlDocument TestDocument = new XmlDocument();

            try
            {
                // receive xml
                TestDocument.LoadXml(responsePayload.Trim());
                receiveXml.AppendChild(document.ImportNode((XmlElement)TestDocument.DocumentElement, true));
                receiveXml.SetAttribute("type", "xml");
                data.AppendChild(receiveXml);
            }
            catch
            {
            }

            // receive text
            receiveText.AppendChild(document.CreateTextNode(responsePayload));
            receiveText.SetAttribute("type", "text");
            data.AppendChild(receiveText);

            try
            {
                TestDocument.LoadXml(responsePayload);
                sentXml.AppendChild(document.ImportNode((XmlElement)TestDocument.DocumentElement, true));
                sentXml.SetAttribute("type", "xml");
                data.AppendChild(sentXml);
            }
            catch
            {
            }

            // sent text
            sentText.AppendChild(document.CreateTextNode(requestPayload));
            sentText.SetAttribute("type", "text");

            data.AppendChild(sentText);
            time.AppendChild(document.CreateTextNode(Convert.ToString(elapsedTime)));
            time.SetAttribute("units", "ms");
            return result.OuterXml;
        }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Code
        {
            get { return code; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Error
        {
            get { return error; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Url
        {
            get { return url; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string RequestPayload
        {
            get { return requestPayload; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string ResponsePayload
        {
            get { return responsePayload; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string ContentType
        {
            get { return contentType; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public long ElapsedTime
        {
            get { return elapsedTime; }
        }
    }
}
