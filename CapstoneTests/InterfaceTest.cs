using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.IO;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class InterfaceTest
    {
        [TestMethod]
        public void ReturnNameTest()
        {
            VendingMachine myVendingMachine = new VendingMachine();
            Interface myInterface = new Interface(myVendingMachine);
            Assert.AreEqual("B2", myInterface.ReturnSlot("B2"));
            Assert.AreEqual("B2", myInterface.ReturnSlot("b2"));
        }
        [TestMethod]
        public void ReturnPriceTest()
        {
            VendingMachine myVendingMachine = new VendingMachine();
            Interface myInterface = new Interface(myVendingMachine);
            Assert.AreEqual(150, myInterface.ReturnPrice("B2"));
            Assert.AreEqual(150, myInterface.ReturnPrice("b2"));

        }

        [TestMethod]
        public void ToDollarsTest()
        {
            VendingMachine myVendingMachine = new VendingMachine();
            Interface myInterface = new Interface(myVendingMachine);
            Assert.AreEqual(15.01M, myInterface.ToDollars(1501));
            Assert.AreEqual(0.01M, myInterface.ToDollars(1));

        }

        //HOW TO TEST VOID METHODS??
    }
    [TestClass]
    public class VendingMachineTest
    {
        [TestMethod]
        public void MakeChangeTest()
        {
            VendingMachine myVendingMachine = new VendingMachine();

            myVendingMachine.IncreaseBalance(100);
            CollectionAssert.AreEquivalent(new List <int> (new int[] { 4, 0, 0, 0 }) , myVendingMachine.MakeChange());
            myVendingMachine.IncreaseBalance(266);
            CollectionAssert.AreEquivalent(new List<int>(new int[] { 10, 1, 1, 1 }), myVendingMachine.MakeChange());

        }
    }
}
