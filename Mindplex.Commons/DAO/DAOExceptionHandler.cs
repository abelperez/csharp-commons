
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

using Mindplex.Commons.Common;
using Mindplex.Commons.Instrumentation;
using Mindplex.Commons.Mail;

#endregion

namespace Mindplex.Commons.DAO
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    [Obsolete("DAOExceptionHandler is obsolete, please use Mindplex.Commons.Exceptions.ExceptionHandler.")]
    public sealed class DAOExceptionHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly string SUBJECT_TOKEN = "${subject}";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static readonly string STACKTRACE_TOKEN = "${stacktrace}";

        /// <summary>
        /// 
        /// </summary>
        /// 
        private static ILog logger = LogManager.GetLogger(typeof(DAOExceptionHandler));

        /// <summary>
        /// 
        /// </summary>
        /// 
        private DAOExceptionHandler()
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
            
            // DAOInstrumentation.FireErrorEvent(message); TODO: revisit instrumentation support
            if (exception == null)
            {
                throw new DAOException(message);
            }
            throw new DAOException(message, exception);
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
            if (parameters == null)
            {
                Process(exception, message);
            }
            else
            {
                Process(exception, string.Format(message, parameters));
            }

            // Process(exception, string.Format(message, parameters));
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
                }

                // DAOInstrumentation.FireWarningEvent(message); TODO: revisit instrumentation support
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
        /// 
        public static void Process(Exception exception, bool supress, string message, params object[] parameters)
        {
            if (parameters == null)
            {
                Process(exception, supress, message);
            }
            else
            {
                Process(exception, supress, string.Format(message, parameters));
            }
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
            if (parameters == null)
            {
                Process(exception, supress, message);
            }
            else
            {
                Process(exception, supress, string.Format(message, parameters));
            }

            //Process(exception, supress, message, parameters);

            if (notify)
            {
                if (parameters == null)
                {
                    EmailGateway.Create().Send(message, exception.Message);
                }
                else
                {
                    EmailGateway.Create().Send(string.Format(message, parameters), exception.Message);
                }
            }
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
            string body = GetBodyTemplate();

            body = body.Replace(SUBJECT_TOKEN, message).Replace(STACKTRACE_TOKEN, exception.Message);
            Process(exception, supress, message);
                        
            if (notify)
            {
                EmailGateway.Create().Send(message, body);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        /// 
        public static string GetBodyTemplate()
        {
            return @"
<html>
	<head>
		<style type='text/css'>
			body {
				color: #000000; background-color: white; font-family: Verdana; margin-left: 0px; margin-top: 0px;
			}
			p {
				color: #000000; margin-top: 0px; margin-bottom: 12px; font-family: Verdana;
			}
			pre {
				background-color: #e5e5cc; padding: 5px; font-family: Courier New; font-size: x-small; margin-top: -5px; border: 1px #f0f0e0 solid;
			}
			code {
				font-face: Courier; font-size: 12px; font-weight: bold;
			}
			td {
				color: #000000; font-family: Verdana; font-size: .7em;
			}
			h1 {
				color: #ffffff; font-family: Tahoma; font-size: 23px; font-weight: normal; background-color: #660066; margin-top: 0px; margin-bottom: 0px; margin-left: -30px; padding-top: 10px; padding-bottom: 3px; padding-left: 15px; width: 100%;
			}
			h2 {
				font-size: 1.5em; font-weight: bold; margin-top: 25px; margin-bottom: 10px; border-top: 1px solid #660066; margin-left: -15px; color: #660066;
			}
			h3 {
				font-size: 1.1em; color: #000000; margin-left: -15px; margin-top: 10px; margin-bottom: 10px;
			}
			ul {
				margin-top: 10px; margin-left: 20px;
			}
			ol {
				margin-top: 10px; margin-left: 20px;
			}
			li {
				margin-top: 10px; color: #000000;
			}
			font.value {
				color: darkblue; font: bold;
			}
			font.key {
				color: darkgreen; font: bold;
			}
			.button {
				background-color: #dcdcdc; font-family: Verdana; font-size: 1em; border-top: #cccccc 1px solid; border-bottom: #666666 1px solid; border-left: #cccccc 1px solid; border-right: #666666 1px solid;
			}
			.frmheader {
				color: #000000; background: #dcdcdc; font-family: Verdana; font-size: .7em; font-weight: normal; border-bottom: 1px solid #dcdcdc; padding-top: 2px; padding-bottom: 2px;
			}
			.frmtext {
				font-family: Verdana; font-size: .7em; margin-top: 8px; margin-bottom: 0px; margin-left: 32px;
			}
			.frmInput {
				font-family: Verdana; font-size: 1em;
			}
			.intro {
				margin-left: -15px;
			}
			#content {
				margin-left: 30px; font-size: .70em; padding-bottom: 2em;
			}
		</style>
	</head>
	<body>
		<div id='content'>
			<h1>${subject}</h1>
			<br/>
			<table>
				<tr valign='top'>
					<td>
						<b>StackTrace:</b>
					</td>
				</tr>
				<tr>
					<td>
						<pre>${stacktrace}</pre>
					</td>
				</tr>
			</table>
		</div>
	</body>
</html>";
        }
    }
}