
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
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

#endregion

namespace Mindplex.Commons.Mail.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public class EmailGatewayTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private static string FROM = "internetbrands.core.mail.test@carsdirect.com";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static string TO = "abel.perez@carsdirect.com"; // TODO: create distro list for unit testing.

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static string HOST = "mx3.internetbrands.com";

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestCreateMailGateway()
        {
                        
            IEmailGateway gateway = EmailGateway.Create();
            Assert.IsNotNull(gateway);

            gateway.Send("Create Config based Mail Service Test", "Success");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestCreateHtmlMailGateway()
        {
            IEmailGateway service = EmailGateway.CreateHtmlMailService(FROM, TO);
            Assert.IsNotNull(service);

            service.Send("Create Html Mail Service Test", "Success");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestCreateHtmlMailGatewayWithHost()
        {
            IEmailGateway service = EmailGateway.CreateHtmlMailService(FROM, TO, HOST);
            Assert.IsNotNull(service);

            service.Send("Create Html Mail Service With Host Test", "Success");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestCreateTextMailGateway()
        {
            IEmailGateway service = EmailGateway.CreateTextMailService(FROM, TO);
            Assert.IsNotNull(service);

            service.Send("Create Text Mail Service Test", "Success");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestCreateTextMailGatewayWithHost()
        {
            IEmailGateway service = EmailGateway.CreateTextMailService(FROM, TO, HOST);
            Assert.IsNotNull(service);

            service.Send("Create Text Mail Service With Host Test", "Success");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSend()
        {
            EmailGateway service = new EmailGateway(HOST);
            service.From = FROM;
            service.To = TO;
            service.IsHtmlBody = true;
            service.Send("Send Test (Statefull)", "Success");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendHtml()
        {
            EmailGateway.SendHtml(HOST, FROM, TO, "Send HTML Test (Stateless)", "Success");
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestSendText()
        {
            EmailGateway.SendText(HOST, FROM, TO, "Send Text Test (Stateless)", "Success");
        }

        [Test]
        public void TestEmailException()
        {
            EmailGatewayException e = new EmailGatewayException();
        }

        [Test]
        public void TestEmailExceptionWithMessage()
        {
            EmailGatewayException e = new EmailGatewayException("Failed to do something.");
        }

        [Test]
        public void TestEmailExceptionWithMessageAndException()
        {
            EmailGatewayException e = new EmailGatewayException("Failed to do something else.", new ArgumentException());
        }

        [Test]
        public void TestTo()
        {
            IEmailGateway service = EmailGateway.CreateTextMailService(FROM, TO, HOST);
            Assert.IsNotNull(service);

            Assert.IsNotNull(service.To);
        }

        [Test]
        public void TestFrom()
        {
            IEmailGateway service = EmailGateway.CreateTextMailService(FROM, TO, HOST);
            Assert.IsNotNull(service);

            Assert.IsNotNull(service.From);
        }

        [Test]
        public void TestHost()
        {
            IEmailGateway service = EmailGateway.CreateTextMailService(FROM, TO, HOST);
            Assert.IsNotNull(service);

            Assert.IsNotNull(service.Host);
        }

        [Test]
        public void TestIsHTML()
        {
            IEmailGateway service = EmailGateway.CreateHtmlMailService(FROM, TO, HOST);
            Assert.IsNotNull(service);

            
            Assert.IsNotNull(service.IsHtmlBody);
        }

        [Test]
        public void TestSetHost()
        {
            IEmailGateway service = EmailGateway.CreateHtmlMailService(FROM, TO, HOST);
            Assert.IsNotNull(service);
            service.Host = "www.msn.com";

        }

        [Test]
        public void TestConstructor()
        {
            IEmailGateway service = new EmailGateway();
            Assert.IsNotNull(service);

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorWithNullMessage()
        {
            IEmailGateway service = new EmailGateway(null);
            Assert.IsNull(service);

        }
    }
}
