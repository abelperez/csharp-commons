
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
using System.Configuration;
using System.Net.Mail;

using log4net;

#endregion

namespace Mindplex.Commons.Mail
{
    /// <summary>
    /// TODO: should use ExceptionHandler.
    /// </summary>
    /// 
    public class EmailGateway : IEmailGateway
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private static ILog logger = LogManager.GetLogger(typeof(EmailGateway));

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly string HOST = "EmailGatewayHost";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly string FROM = "EmailGatewayFrom";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly string TO = "EmailGatewayTo";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly string HTMLBODY = "EmailGatewayHtmlBody";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string host;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string from;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private string to;

        /// <summary>
        /// 
        /// </summary>
        /// 
        private bool htmlBody;

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// 
        public EmailGateway()
            : this(GetValue(HOST))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="host"></param>
        /// 
        public EmailGateway(string host)
        {
            Guard.CheckIsNullOrEmpty(host, "host");

            this.host = host;

            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat("Initialized Mail Gateway with host: {0}.", host);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="host"></param>
        /// <param name="isHtmlBody"></param>
        /// 
        private EmailGateway(string from, string to, string host, bool isHtmlBody)
        {
            Guard.CheckIsNullOrEmpty(from, to); // TODO: add verbose message.
            
            if (Guard.IsNullOrEmpty(host))
            {
                this.host = GetValue(HOST);
            }
            else
            {
                this.host = host;
            }

            this.from = from;
            this.to = to;
            this.htmlBody = isHtmlBody;

            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat("Initialized Mail Gateway with host: {0}, from: {1}, to: {2}, html body: {3}.", this.host, this.from, this.to, this.htmlBody);
            }
        }

        #endregion

        #region Config based Varients

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public static IEmailGateway Create()
        {
            try
            {
                string host = GetValue(HOST);
                string from = GetValue(FROM);
                string to = GetValue(TO);
                bool htmlBody = Boolean.Parse(GetValue(HTMLBODY));

                return new EmailGateway(from, to, host, htmlBody);
            }
            catch (Exception exception)
            {
                throw new EmailGatewayException("Failed to create Mail Service.", exception);
            }
        }

        #endregion

        #region Stateless Varients

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// 
        /// <returns></returns>
        /// 
        public static IEmailGateway CreateHtmlMailService(string from, string to)
        {
            return new EmailGateway(from, to, null, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="host"></param>
        /// 
        /// <returns></returns>
        /// 
        public static IEmailGateway CreateHtmlMailService(string from, string to, string host)
        {
            return new EmailGateway(from, to, host, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// 
        /// <returns></returns>
        /// 
        public static IEmailGateway CreateTextMailService(string from, string to)
        {
            return new EmailGateway(from, to, null, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="host"></param>
        /// 
        /// <returns></returns>
        /// 
        public static IEmailGateway CreateTextMailService(string from, string to, string host)
        {
            return new EmailGateway(from, to, host, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// 
        public static void SendText(string from, string to, string subject, string body)
        {
            Send(GetValue(HOST), from, to, subject, body, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="host"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// 
        public static void SendText(string host, string from, string to, string subject, string body)
        {
            Send(host, from, to, subject, body, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// 
        public static void SendHtml(string from, string to, string subject, string body)
        {
            Send(GetValue(HOST), from, to, subject, body, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="host"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// 
        public static void SendHtml(string host, string from, string to, string subject, string body)
        {
            Send(host, from, to, subject, body, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="host"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtml"></param>
        /// 
        private static void Send(string host, string from, string to, string subject, string body, bool isHtml)
        {
            Guard.CheckIsNullOrEmpty(host);
            
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(from);
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                SmtpClient client = new SmtpClient(host);
                client.Send(message);

                if (logger.IsDebugEnabled)
                {
                    logger.DebugFormat("Mail Gateway sent host: {0}, from: {1}, to: {2}, html body: {3}, subject: {4}, body: {5}.", host, from, to, isHtml, subject, body);
                }
            }
            catch (Exception exception)
            {
                throw new EmailGatewayException("Failed to send email.", exception);
            }
        }

        #endregion

        #region Stateful Varients

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// 
        public void Send(string subject, string body)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(from);
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = htmlBody;

                SmtpClient client = new SmtpClient(host);
                client.Send(message);

                if (logger.IsDebugEnabled)
                {
                    logger.DebugFormat("Mail Gateway sent subject: {0}, body: {1}.", subject, body);
                }
            }
            catch (Exception exception)
            {
                throw new EmailGatewayException("Failed to send email.", exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string From
        {
            get { return from; }
            set { from = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string To
        {
            get { return to; }
            set { to = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public bool IsHtmlBody
        {
            get { return htmlBody; }
            set { htmlBody = value; }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="key"></param>
        /// 
        /// <returns></returns>
        /// 
        private static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
