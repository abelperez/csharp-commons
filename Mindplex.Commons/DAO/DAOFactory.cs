
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
using System.Reflection;

#endregion

namespace InternetBrands.Core.DAO
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <typeparam name="T"></typeparam>
    /// 
    /// <author>Abel Perez (java.aperez@gmail.com)</author>
    /// 
    public class DAOFactory<T> where T : GenericDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="alias"></param>
        /// 
        /// <returns></returns>
        /// 
        public static T Create(string alias)
        {
            T result = default(T);

            // gets the dao configuration based on the specified alias.
            DAOConfiguration daoConfiguration = ConfigurationManager.GetSection(DAOConfiguration.configurationSection) as DAOConfiguration;
            DAOConfigurationData daoConfigurationData = daoConfiguration.DataAccessManager.Get(alias);

            // sets dao type and singleton method return type.
            Type daoType = Type.GetType(daoConfigurationData.Type);
            Type singletonMethodReturnType = Type.GetType(daoConfigurationData.ReturnType);

            // triggers creation of the singleton dao by getting the configured dao property.
            PropertyInfo propertyInfo = daoType.GetProperty(daoConfigurationData.SingletonMethod, singletonMethodReturnType);
            result = propertyInfo.GetValue(daoType, null) as T;

            DAOInstrumentation.FireInfoEvent("Factory created DAO: {0}", alias);
            return result;
        }
    }
}