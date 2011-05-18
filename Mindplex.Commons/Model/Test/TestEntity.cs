
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

namespace Mindplex.Commons.Model.Test
{
    /// <summary>
    ///
    /// </summary>
    ///
    [Serializable]
    public partial class TestEntity
    {
        /// <summary>
        ///
        /// </summary>
        ///
        public TestEntity()
        {
        }

        /// <summary>
        ///
        /// </summary>
        ///
        private long id;

        /// <summary>
        ///
        /// </summary>
        ///
        private string name;

        /// <summary>
        ///
        /// </summary>
        ///
        private string description;

        private DateTime date = DateTime.Now;

        private string[] stringArray;

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// 
        public TestEntity(long id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        /// <summary>
        ///
        /// </summary>
        ///
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private string PrivateDescription
        {
            get { return description; }
        }

        /// <summary>
        ///
        /// </summary>
        ///
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        ///
        /// </summary>
        ///
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string[] StringArray
        {
            get { return stringArray; }
            set { stringArray = value; }
        }

        /// <summary>
        ///
        /// </summary>
        ///
        /// <returns></returns>
        ///
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("Id = {0}, ", Id);
            result.AppendFormat("Name = {0}, ", Name);
            result.AppendFormat("Description = {0}, ", Description);
            return result.ToString();
        }
    }
}

#endif