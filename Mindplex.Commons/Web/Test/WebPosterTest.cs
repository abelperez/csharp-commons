
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

#if UNIT_TEST

#region Imports

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

using Mindplex.Commons.Model;
using Mindplex.Commons.Test;
using NUnit.Framework;

#endregion

namespace Mindplex.Commons.Web.Test
{
    [TestFixture]
    public class WebPosterTest
    {
        [SetUp]
        public void SetupLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [Test]
        public void TestDatranPixelFireGet()
        {
            try
            {
                string url = @"http://www.live-conversion.com/conversion.gif?GUAID=0008C8EB4501590100000000019D537A&TID=none";
                WebPoster webPoster = new WebPoster(url);
                string result = webPoster.Post(PostType.Get);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.InnerException);
                Assert.Fail(ex.Message + Environment.NewLine + ex.InnerException);
            }
        }

        [Test]
        public void TestDatranPixelFirePost()
        {
            try
            {
                string url = @"http://www.live-conversion.com/conversion.gif?GUAID=0008C8EB4501590100000000019D537A&TID=none";
                WebPoster webPoster = new WebPoster(url);
                string result = webPoster.Post(PostType.Post);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.InnerException);
                Assert.Fail(ex.Message + Environment.NewLine + ex.InnerException);
            }
        }

        [Test]
        public void TestLocalIP()
        {
            string strHostName = Dns.GetHostName();
            Console.WriteLine("Local Machine's Host Name: " + strHostName);


            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostByName(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            for (int i = 0; i < addr.Length; i++)
            {
                Console.WriteLine("IP Address {0}: {1} ", i, addr[i].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestHttpGateway()
        {
            //string url = "https://secure.blueskymg.com/AFLPost/Prod/ping.aspx";
            //string payload = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><XmlRoot><Ping ZipCode=\"98102\" AppIncome=\"1500\" SSN=\"611111111\" SourceId=\"CA5140\" SourcePw=\"omasyj\"/></XmlRoot>";
            string url = "http://ping.lendergate.com/";
           // string payload = System.Web.HttpUtility.UrlEncode("ssn=611111111&zipCode=98102&grossMonthlyIncome=2000&providerID=1092");
            string payload = "pingtier=1&vendor=cdirect&zip=00301&moninc=2200&ssn=000030000";

            HttpGateway gateway = HttpGateway.CreatePOST(url, 45000);
            HttpGatewayResponse response = gateway.Request(payload);
            string xml = response.ToXml();

            Console.WriteLine(xml);
        }
    }
}

#endif
