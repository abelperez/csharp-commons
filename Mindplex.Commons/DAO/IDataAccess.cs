
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.Transactions;
using System.Xml;

using InternetBrands.Core.Model;
using InternetBrands.Core.DAO;
using InternetBrands.Core.Common;
using InternetBrands.Core.Instrumentation;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

#endregion

namespace InternetBrands.Core.DAO
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>Amin Bandeali</author>
    /// 
    public interface IDataAccess
    {
        IDbConnection CreateConnection();
    }
}
