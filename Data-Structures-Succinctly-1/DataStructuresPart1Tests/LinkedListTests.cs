using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructuresPart1.Program;
using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1.Program.Tests
{
    [TestClass()]
    public class LinkedListTests
    {
        [TestMethod()]
        public void CopyToTest()
        {
            // Arrange
            LinkedList<string> linkedList = new LinkedList<string>();
            linkedList.Add("Apple");
            linkedList.Add("Banana");
            linkedList.Add("Cranberry");
            linkedList.Add("Date");

            string[] stringArray = new string[linkedList.Count];

            // Act
            linkedList.CopyTo(stringArray, 0);


            // Assert
            // Not really a test, I just created this to see how the array[arrayIndex++] worked inside the CopyTo method.

        }
    }
}