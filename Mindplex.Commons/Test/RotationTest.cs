
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

using NUnit.Core;
using NUnit.Framework;

#endregion

namespace Mindplex.Commons.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TestFixture]
    public  class RotationTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void TestRotate()
        {
            Dictionary<string, int> items = new Dictionary<string, int>();
            items.Add("IB_Light", 80);
            items.Add("IB_Green", 10);
            items.Add("IB_Aqua", 10);

            for (int i = 0; i < 100; i++)
            {
                string item = Rotation.Rotate<string>(items);
                Assert.IsNotNull(item);

                Console.WriteLine("rotating: {0}.", item);
            }
        }
    }
}
