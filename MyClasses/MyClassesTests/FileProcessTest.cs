using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System.Configuration;
using System.IO;


namespace MyClassesTests
{
    [TestClass]
    public class FileProcessTest
    {
        //constants
        private const string BadFileName = @"c:/marketwatch.stocks";
        private string _goodFileName;

        [TestMethod]
        public void FileNameDoesExist()
        {
            //Arrange
            FileProcess fp = new FileProcess();

            //Act
            SetGoodFileName();
            File.AppendAllText(_goodFileName, "CR7 > Messi");
            var actual = fp.FileExists(_goodFileName);
            File.Delete(_goodFileName);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
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
    }
}
