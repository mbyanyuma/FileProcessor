using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using MyClasses.PersonClasses;

namespace MyClassesTests
{

    [TestClass]
    public class AssertTestClass
    {
        //testing for object types
        [TestMethod]
        [Owner("mosesb")]
        public void AreSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = x;

            Assert.AreSame(x, y);
        }

        [TestMethod]
        [Owner("mosesb")]
        public void AreNotSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = new FileProcess();

            Assert.AreNotSame(x, y);
        }

        #region IsInstanceOfType Test

        [TestMethod]
        [Owner("mosesb")]
        public void IsInstanceOfTypeTest()
        {
            PersonManager manager = new PersonManager();
            Person person;

            person = manager.CreatePerson("Russell", "Westbrook", true);

            Assert.IsInstanceOfType(person, typeof(Supervisor));
        }

        #endregion

        #region IsNull Test

        [TestMethod]
        [Owner("mosesb")]
        public void IsNullTest()
        {
            PersonManager manager = new PersonManager();
            Person person;

            person = manager.CreatePerson("", "Ball", true);

            Assert.IsNull(person);
        }

        #endregion
    }
}
