
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
using System.IO;
using System.Reflection;
using System.Text;

using log4net;

using Mindplex.Commons.Mail;
using Mindplex.Commons.Resources;

#endregion

namespace Mindplex.Commons.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public sealed class ExceptionHandler<E> where E : IGenericExceptionFactory, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private static ILog logger = LogManager.GetLogger("InternetBrands.Core.Exceptions.ExceptionHandler");

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static string SUBJECT_TOKEN = "${subject}";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static string STACKTRACE_TOKEN = "${stacktrace}";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static string EXCEPTION_EMAIL_TEMPLATE = "InternetBrands.Core.Resources.ExceptionEmailTemplate.txt";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private ExceptionHandler()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// 
        public static void Process(Exception exception, string message)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message, exception);
                // logger.Error(ExceptionFormatter.Format(exception), exception); TODO: revisit?
            }

            E wrapper = new E();
            IGenericException target = null;

            if (exception == null)
            {
                target = wrapper.Create(message);
            }
            else
            {
                target = wrapper.Create(message, exception);
            }
            
            throw target as Exception;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        public static void Process(Exception exception, string message, params object[] parameters)
        {
            Process(exception, string.Format(message, parameters));
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="exception"></param>
        /// <param name="supress"></param>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        public static void Process(Exception exception, bool supress, string message)
        {
            if (supress == true)
            {
                if (logger.IsWarnEnabled)
                {
                    logger.Warn(message, exception);
                    // logger.Warn(ExceptionFormatter.Format(exception)); TODO: revisit?
                }
            }
            else
            {
                Process(exception, message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="exception"></param>
        /// <param name="supress"></param>
        /// <param name="message"></param>
        /// 
        public static void Process(Exception exception, bool supress, string message, params object[] parameters)
        {
            Process(exception, supress, string.Format(message, parameters));
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="exception"></param>
        /// <param name="supress"></param>
        /// <param name="notify"></param>
        /// <param name="message"></param>
        /// 
        public static void Process(Exception exception, bool supress, bool notify, string message)
        {
            if (notify)
            {
                try
                {
                    string body = Resource.GetResource(EXCEPTION_EMAIL_TEMPLATE);
                    if (Guard.IsNullOrEmpty(body))
                    {
                        if (logger.IsWarnEnabled)
                        {
                            logger.WarnFormat("Could not find exception email template.");
                        }

                        body = string.Format("Message: {0}, Encountered: {1}.", message, exception);
                    }

                    string exceptionMessage = message;

                    if (exception != null)
                    {
                        try
                        {
                            exceptionMessage = ExceptionFormatter.Format(exception);
                        }
                        catch (Exception internalException)
                        {
                            // TODO: should never happen, need to log in case!
                            exceptionMessage = internalException.Message; //exception.Message;
                        }
                    }

                    body = body.Replace(SUBJECT_TOKEN, message).Replace(STACKTRACE_TOKEN, exceptionMessage);

                    EmailGateway.Create().Send(message, body);

                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("Exception notification sent.");
                    }
                }
                catch (Exception internalException)
                {
                    if (logger.IsErrorEnabled)
                    {
                        logger.Error("Failed to send exception notification.", internalException);
                    }
                }
            }

            Process(exception, supress, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="exception"></param>
        /// <param name="supress"></param>
        /// <param name="notify"></param>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        public static void Process(Exception exception, bool supress, bool notify, string message, params object[] parameters)
        {
            Process(exception, supress, notify, string.Format(message, parameters));
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        public static void Throw(string message)
        {
            Process(null, message); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        public static void Throw(string message, params object[] parameters)
        {
            Process(null, string.Format(message, parameters)); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="notify"></param>
        /// <param name="message"></param>
        /// 
        public static void Throw(bool notify, string message)
        {
            Process(null, false, notify, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="notify"></param>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// 
        public static void Throw(bool notify, string message, params object[] parameters)
        {
            Process(null, false, notify, message, parameters);
        }   
    }
}
