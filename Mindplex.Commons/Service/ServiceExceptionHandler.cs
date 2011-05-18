
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

using log4net;

using Mindplex.Commons.Instrumentation;

#endregion

namespace Mindplex.Commons.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    [Obsolete("ServiceExceptionHandler is obsolete, please use Mindplex.Commons.Exceptions.ExceptionHandler.")]
    public sealed class ServiceExceptionHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private static ILog logger = LogManager.GetLogger(typeof(ServiceExceptionHandler));

        /// <summary>
        /// 
        /// </summary>
        /// 
        private ServiceExceptionHandler()
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
            }

            // ServiceInstrumentation.FireErrorEvent(message); TODO: revisit instrumentation support
            if (exception == null)
            {
                throw new ServiceException(message);
            }
            throw new ServiceException(message, exception);
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
        /// 
        public static void Process(Exception exception, bool supress, string message)
        {
            if (supress == true)
            {
                if (logger.IsWarnEnabled)
                {
                    logger.Warn(message, exception);
                }

                // ServiceInstrumentation.FireWarningEvent(message); TODO: revisit instrumentation support
                return;
            }

            Process(exception, message);
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
        public static void Process(Exception exception, bool supress, string message, params object[] parameters)
        {
            Process(exception, supress, string.Format(message, parameters));
        }
    }
}