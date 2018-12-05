using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;


namespace MyClassesTests
{
    [TestClass]
    public class FileProcessTest
    {
        //constants
        private readonly string badFileName = @"c:/marketwatch.stocks";

        [TestMethod]
        public void FileNameDoesExist()
        {
            //Arrange
            FileProcess fp = new FileProcess();

            //Act
            var actual = fp.FileExists(@"c:/users/mbyan/documents/sampleTextFiles/testFile.txt");

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void FileNameDoesNotExist()
        {
            //Arrange
            FileProcess fp = new FileProcess();

            //Act
            var actual = fp.FileExists(badFileName);

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
    }
}
