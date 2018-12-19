using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System.Configuration;
using System.IO;
using System.Security.Policy;


namespace MyClassesTests
{
    [TestClass]
    public class FileProcessTest
    {
        //constants
        private const string BadFileName = @"c:/marketwatch.stocks";
        private string _goodFileName;

        private const string FILE_NAME = @"FileToDeploy.txt";

        public TestContext TestContext { get; set; }

        #region Class Initialize and Cleanup

        [ClassInitialize]
        public static void ClassInitialize(TestContext tc)
        {
            tc.WriteLine("In the Class Initialize");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            //TODO: clean up any class resources used by tests
        }
        #endregion

        #region Test Initiailize and Cleanup

        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.TestName.StartsWith("FileNameDoesExist"))
            {
                SetGoodFileName();
                if (!string.IsNullOrEmpty(_goodFileName))
                {
                    TestContext.WriteLine("Creating the file: " + _goodFileName);
                    File.AppendAllText(_goodFileName, "CR7 > Messi");
                }
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName.StartsWith("FileNameDoesExist"))
            {
                if (!string.IsNullOrEmpty(_goodFileName))
                {
                    TestContext.WriteLine("Deleting the file: " + _goodFileName);
                    File.Delete(_goodFileName);
                }
            }
            
        }
        #endregion

        [TestMethod]
        [DataSource("System.Data.SqlClient",
            "Server=LAPTOP-6ICNF0TN\\SQLEXPRESS;Database=FileProcessDB;Integrated Security=Yes",
            "tests.FileProcessTest",
            DataAccessMethod.Sequential)]
        public void FileExistsTestFromDb()
        {
            FileProcess fp = new FileProcess();
            string fileName;
            bool expectedValue;
            bool causesException;

            //get values from data row
            fileName = TestContext.DataRow["FileName"].ToString();
            expectedValue = Convert.ToBoolean(TestContext.DataRow["ExpectedValue"]);
            causesException = Convert.ToBoolean(TestContext.DataRow["CausesException"]);

            //check assertion
            try
            {
                var actual = fp.FileExists(fileName);
                Assert.AreEqual(expectedValue, actual, "File Name: " + fileName +
                                                       "doesn't seem to exist according to the code in test method: FileExistsTestFromDb()");
            }
            catch (AssertFailedException m)
            {
                //rethrow assertion
                throw m;
            }
            catch (ArgumentNullException)
            {
                // See if method was expected to throw an exception
                Assert.IsTrue(causesException);
            }



        }


        [TestMethod]
        [Description("Check to see if a file does exist")]
        [Owner("mosesb")]
        [Priority(1)]
        [TestCategory("NoException")]
        public void FileNameDoesExist()
        {
            //Arrange
            FileProcess fp = new FileProcess();

            //Act
            
            TestContext.WriteLine("Testing the file: " + _goodFileName);
            var actual = fp.FileExists(_goodFileName);
            

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        [Description("Check to see if a file does Not exist")]
        [Owner("mosesb")]
        [Priority(1)]
        [TestCategory("NoException")]
        public void FileNameDoesNotExist()
        {
            //Arrange
            FileProcess fp = new FileProcess();

            //Act
            var actual = fp.FileExists(BadFileName);

            //Assert
            Assert.IsFalse(actual);

        }

        [TestMethod]
        [Owner("mosesb")]
        [Description("Check to see if there's an empty string for file name or file name not entered at all")]
        [Priority(2)]
        [TestCategory("Exception")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            //Arrange
            FileProcess fp = new FileProcess();
            

            //Act
             fp.FileExists("");

            //Assert
            
        }

        private void SetGoodFileName()
        {
            _goodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_goodFileName.Contains("[AppPath]"))
            {
                _goodFileName = _goodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        [TestMethod]
        [Owner("mosesb")]
        [DeploymentItem(FILE_NAME)]
        public void FileNameDoesExistUsingDeploymentItem()
        {
            //arrange
            FileProcess fp = new FileProcess();
            string fileName;

            fileName = TestContext.DeploymentDirectory + @"\" + FILE_NAME;
            TestContext.WriteLine("Checking file: " + fileName);

            //act
            var actual = fp.FileExists(fileName);

            //assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        [Timeout(2000)]
        public void SetTimeout()
        {
            System.Threading.Thread.Sleep(4000);
        }

        [TestMethod]
        public void FileNameDoesExistMessageWithFormatting()
        {

            FileProcess fp = new FileProcess();

            var actual = fp.FileExists(_goodFileName);

            Assert.IsFalse(actual, "File '{0}' does not exist", _goodFileName);
        }

    }
}
