using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyClassesTests
{
    /// <summary>
    /// Assembly Initialize and cleanup methods
    /// </summary>
    [TestClass]
    public class MyClassesTestInitialization
    {

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext tc)
        {
            tc.WriteLine("In the Assembly Initialize method. ");
            // TODO: Create resources needed for the tests.
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // TODO: Clean up any resources used by the tests. 
        }
    }
}
