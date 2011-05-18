
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

#endregion

namespace Mindplex.Commons.Service.Test
{
    /// <summary>
    /// 
    /// </summary>
    ///
    [Service("SimpleServiceTest", typeof(SimpleService))]
    public class SimpleService : GenericService, ISimpleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        public event ServiceEventHandler<SimpleService, string> OnSaved;

        /// <summary>
        /// 
        /// </summary>
        /// 
        public static ISimpleService Instance
        {
            get
            {
                return new SimpleService();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        public void DisplayMessage(string message)
        {
            Logger.Info(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void DoSomething()
        {
            FireServiceEvent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void Save()
        {
            if (OnSaved == null)
            {
                return;
            }
            OnSaved(this, new ServiceEventArgs<string>("entity from mars"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void Execute()
        {
            this.FireInfoEvent("{0} executed transactional operation.", GetType());
        }

        #region Concurrent Loop

        public delegate void Executor<T>(T action);

        public void Execute<T>(T action)
        {
            Console.WriteLine("doing something with T...");
        }

        public void GenericForEachConcurrent<T>(T[] actions)
        {
            System.Collections.ArrayList managers = null;
            System.Collections.ArrayList Threads = null;

            try
            {
                if (actions != null)
                {
                    managers = new System.Collections.ArrayList();
                    foreach (T action in actions)
                    {
                        //managers.Add(new Executor<T>(executor));
                        managers.Add(new Executor<T>(Execute));
                    }
                    if (managers.Count > 0)
                    {
                        Threads = new System.Collections.ArrayList();
                        foreach (Manager pcm in managers)
                        {
                            System.Threading.Thread thread = null;
                            try
                            {
                                thread = new System.Threading.Thread(new System.Threading.ThreadStart(pcm.Execute));
                                thread.Priority = System.Threading.ThreadPriority.Highest;
                                thread.Start();
                                Threads.Add(thread);
                            }
                            finally
                            {
                                thread = null;
                            }
                        }

                        int Counter = 0;
                        System.Threading.WaitHandle[] EventHandles = new System.Threading.WaitHandle[Threads.Count];
                        foreach (Manager pcm in managers)
                        {
                            EventHandles[Counter] = pcm.arEvent;
                            Counter++;
                        }

                        System.Threading.WaitHandle.WaitAll(EventHandles, 30000, false);


                        System.Xml.XmlDocument WholesaleAccepted = new System.Xml.XmlDocument();
                        WholesaleAccepted.LoadXml("<wholesale-accepted-ping-results/>");
                        System.Xml.XmlDocument WholesaleRejected = new System.Xml.XmlDocument();
                        WholesaleRejected.LoadXml("<wholesale-rejected-ping-results/>");

                        Counter = 0;
                        // bool FinalResult = false;
                        foreach (Manager pcm in managers)
                        {
                            if (pcm.Accepted)
                            {
                                pcm.ToXml(WholesaleAccepted);
                                Console.WriteLine(WholesaleAccepted.OuterXml);
                            }
                            else
                            {
                                pcm.ToXml(WholesaleRejected);
                                Console.WriteLine(WholesaleRejected.OuterXml);
                            }
                        }


                        foreach (System.Threading.Thread Current in Threads)
                        {
                            Current.Abort();
                        }
                    }
                }
            }
            finally
            {
            }
        }

        public void ForEachConcurrent(string[] reader)
        {
            System.Collections.ArrayList managers = null;
            System.Collections.ArrayList Threads = null;

            try
            {
                if (reader != null)
                {
                    managers = new System.Collections.ArrayList();
                    foreach (string s in reader)
                    {
                        managers.Add(new Manager());
                    }
                    if (managers.Count > 0)
                    {
                        Threads = new System.Collections.ArrayList();
                        foreach (Manager pcm in managers)
                        {
                            System.Threading.Thread thread = null;
                            try
                            {
                                thread = new System.Threading.Thread(new System.Threading.ThreadStart(pcm.Execute));
                                thread.Priority = System.Threading.ThreadPriority.Highest;
                                thread.Start();
                                Threads.Add(thread);
                            }
                            finally
                            {
                                thread = null;
                            }
                        }

                        int Counter = 0;
                        System.Threading.WaitHandle[] EventHandles = new System.Threading.WaitHandle[Threads.Count];
                        foreach (Manager pcm in managers)
                        {
                            EventHandles[Counter] = pcm.arEvent;
                            Counter++;
                        }

                        System.Threading.WaitHandle.WaitAll(EventHandles, 30000, false);


                        System.Xml.XmlDocument WholesaleAccepted = new System.Xml.XmlDocument();
                        WholesaleAccepted.LoadXml("<wholesale-accepted-ping-results/>");
                        System.Xml.XmlDocument WholesaleRejected = new System.Xml.XmlDocument();
                        WholesaleRejected.LoadXml("<wholesale-rejected-ping-results/>");

                        Counter = 0;
                        // bool FinalResult = false;
                        foreach (Manager pcm in managers)
                        {
                            if (pcm.Accepted)
                            {
                                pcm.ToXml(WholesaleAccepted);
                                Console.WriteLine(WholesaleAccepted.OuterXml);
                            }
                            else
                            {
                                pcm.ToXml(WholesaleRejected);
                                Console.WriteLine(WholesaleRejected.OuterXml);
                            }
                        }

                        
                        foreach (System.Threading.Thread Current in Threads)
                        {
                            Current.Abort();
                        }
                    }
                }
            }
            finally
            {
            }
        }
        #endregion
    }

    public class Manager
    {
        private bool success;

        public System.Threading.AutoResetEvent arEvent;

        public Manager()
        {
            arEvent = new System.Threading.AutoResetEvent(false);
        }

        public void Execute()
        {
            //System.Threading.Thread.Sleep(new Random().Next(2000));
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine(System.Threading.Thread.CurrentThread.GetHashCode() +  " executing manager...");
            success = true;
            arEvent.Set();
        }

        public bool Accepted
        {
            get
            {
                return success;
            }
        }

        public void ToXml(System.Xml.XmlDocument doc)
        {
            System.Xml.XmlElement newElement = null;
            try
            {
                ////if (result.Affiliate != null)
                ////{
                //    newElement = (System.Xml.XmlElement)doc.DocumentElement.AppendChild(doc.CreateElement("ping-result"));
                //    newElement.SetAttribute("affiliate", PCMPartnerCode);
                //    newElement.SetAttribute("accepted", result.Accepted.ToString());
                //    newElement.SetAttribute("success", result.Success.ToString());
                //    newElement.SetAttribute("redirection", result.Redirection.ToString());
                //    newElement.SetAttribute("reservation", result.Reservation);
                //    newElement.SetAttribute("status", Status);
                //    newElement.SetAttribute("ms", Duration.ToString());
                //    newElement.AppendChild(doc.CreateElement("request-url")).InnerText = "<![CDATA[" + result.URL + "]]>";
                //    newElement.AppendChild(doc.CreateElement("request-post-data")).InnerText = "<![CDATA[" + result.PostData + "]]>";
                //    newElement.AppendChild(doc.CreateElement("redirection-url")).InnerText = "<![CDATA[" + result.RedirectURL + "]]>";
                //    newElement.AppendChild(doc.CreateElement("response")).InnerText = "<![CDATA[" + result.Response + "]]>";
                //    newElement.AppendChild(doc.CreateElement("message")).InnerText = "<![CDATA[" + result.Message + "]]>";
                //}
                //else
                //{
                    newElement = (System.Xml.XmlElement)doc.DocumentElement.AppendChild(doc.CreateElement("ping-result"));
                    newElement.SetAttribute("affiliate", "BlueSky");
                    newElement.SetAttribute("status", success.ToString());
                    newElement.SetAttribute("ms", "Duration.ToString()");
                //}
            }
            finally
            {
                newElement = null;
            }
        }
    }
}

#endif
