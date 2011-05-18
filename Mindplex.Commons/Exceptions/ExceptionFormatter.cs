
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

using Mindplex.Commons.Model;

#endregion

namespace Mindplex.Commons.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    ///  
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public class ExceptionFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="exception"></param>
        /// 
        /// <returns></returns>
        /// 
        public static CapturedExceptionCollection<CapturedException> CaptureException(Exception exception)
        {
            return CaptureException(exception, null);
        }

        /// <summary>
        /// This operation recursively calls itself if the specified 
        /// exception has inner exceptions.
        /// </summary>
        /// 
        /// <param name="exception"></param>
        /// <param name="outerException"></param>
        /// 
        /// <returns></returns>
        /// 
        public static CapturedExceptionCollection<CapturedException> CaptureException(Exception exception, Exception outerException)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            CapturedExceptionCollection<CapturedException> result = new CapturedExceptionCollection<CapturedException>();
            CapturedException item = new CapturedException(exception);
            result.Add(item);

            if (outerException == null)
            {
                item.OuterException = true;
            }

            System.Exception inner = exception.InnerException;

            if (inner != null)
            {
                result.Merge(CaptureException(inner, exception).GetEntities());
            }

            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="exception"></param>
        /// 
        /// <returns></returns>
        /// 
        public static string Format(Exception exception)
        {
            CapturedExceptionCollection<CapturedException> exceptions = CaptureException(exception);
            StringBuilder result = new StringBuilder();
            result.Append(FormatExceptionHeader(exceptions));
            string heading = string.Empty;

            foreach (CapturedException capturedException in exceptions)
            {
                if (capturedException.OuterException == true)
                {
                    heading = string.Format("Outer Exception: {0}", capturedException.ExceptionType);
                }
                else
                {
                    heading = string.Format("Inner Exception: {0}", capturedException.ExceptionType);
                }

                result.AppendLine(heading);
                result.AppendLine(new string('-', heading.Length));
                result.Append(FormatException(capturedException));
            }

            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="exception"></param>
        /// 
        /// <returns></returns>
        /// 
        public static string FormatAsXml(Exception exception)
        {
            CapturedExceptionCollection<CapturedException> exceptions = CaptureException(exception);
            StringBuilder result = new StringBuilder();
            
            result.Append(FormatExceptionHeaderAsXml(exceptions));
            string heading = string.Empty;

            foreach (CapturedException capturedException in exceptions)
            {
                if (capturedException.OuterException == true)
                {
                    heading = "<exception type=\"outer\">";
                }
                else
                {
                    heading = "<exception type=\"inner\">";
                }

                result.AppendLine(heading);
                result.Append(FormatExceptionAsXml(capturedException));
                result.Append("</exception>");
            }

            result.Append("</ExceptionSummary>");
            return result.ToString();
        }

        /// <remarks />
        /// 
        private static string FormatExceptionAsXml(CapturedException exception)
        {
            return new StringBuilder()
                .Append("<header>").Append(exception.ExceptionType).Append("</header>")
                .Append("<type>").Append(exception.ExceptionType).Append("</type>")
                .Append("<message>").Append(exception.ExceptionMessage).Append("</message>")
                .Append("<source>").Append(exception.ExceptionSource).Append("</source>")
                .Append("<stacktrace>").Append(exception.StackTrace).Append("</stacktrace>").ToString();
        }

        /// <remarks />
        /// 
        private static string FormatException(CapturedException exception)
        {
            return new StringBuilder()
                .Append("Type: ").Append(exception.ExceptionType).AppendLine()
                .Append("Message: ").Append(exception.ExceptionMessage).AppendLine()
                .Append("Source: ").Append(exception.ExceptionSource).AppendLine()
                .Append("StackTrace: ").Append(exception.StackTrace).AppendLine()
                .AppendLine().AppendLine().ToString();
        }

        /// <remarks />
        /// 
        private static string FormatExceptionHeader(CapturedExceptionCollection<CapturedException> exceptions)
        {
            return new StringBuilder()
                .Append("Time: ").Append(exceptions.TimeStamp).AppendLine()
                .Append("Machine: ").Append(exceptions.MachineName).AppendLine()
                .Append("Windows Identity: ").Append(exceptions.WindowsIdentity).AppendLine()
                .Append("Assembly: ").Append(exceptions.AssemblyFullName).AppendLine()
                .Append("App Domain: ").Append(exceptions.AppDomainName).AppendLine()
                .AppendLine(new String('-', 80)).AppendLine().ToString();
        }

        /// <remarks />
        /// 
        private static string FormatExceptionHeaderAsXml(CapturedExceptionCollection<CapturedException> exceptions)
        {
            return new StringBuilder()
                .AppendFormat("<ExceptionSummary machine=\"{0}\" identity=\"{1}\" assembly=\"{2}\" appdomain=\"{3}\" timestamp=\"{4}\" >", exceptions.MachineName, exceptions.WindowsIdentity, exceptions.AssemblyFullName, exceptions.AppDomainName, exceptions.TimeStamp).ToString();
        }
    }
}