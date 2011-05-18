
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

#endregion

namespace InternetBrands.Core
{
    /// <summary>
    /// 
    /// </summary>
	///
    /// <author>Abel Perez</author>	
    /// 
    public class Rotation
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// 
        /// <param name="items"></param>
        /// 
        /// <returns></returns>
        /// 
        public static T Rotate<T>(Dictionary<T, int> items)
        {
            int itemValue = 0;

            foreach (T item in items.Keys)
            {
                if (items[item] > 0)
                {
                    itemValue += items[item];
                }
            }

            if (itemValue > 0)
            {
                int random = 0;
                while (random == 0)
                {
                    random = new Random().Next(0, itemValue + 1);
                }
                foreach (T item in items.Keys)
                {
                    int counter = items[item];
                    if (counter > 0)
                    {
                        if (counter >= random)
                        {
                            return item;
                        }
                        random -= counter;
                    }
                }
            }
            return default(T);
        }
    }
}
