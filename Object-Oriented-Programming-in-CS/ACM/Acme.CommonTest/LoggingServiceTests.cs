using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACM.BL;

namespace Acme.Common.Tests
{
    [TestClass()]
    public class LoggingServiceTests
    {
        [TestMethod()]
        public void WriteToFileTest()
        {
            // Arrange
            var changedItems = new List<ILoggable>();

            var customer = new Customer(1)
            {
                EmailAddress = "fbaggins@hobbiton.me",
                FirstName = "Frodo",
                LastName = "Baggins",
                AddressList = null
            };

            changedItems.Add(customer as ILoggable);

            var product = new Product(2)
            {
                ProductName = "Rake",
                ProductDescription = "Garden Rake with Steel Head",
                CurrentPrice = 6M
            };
            changedItems.Add(product as ILoggable);

            // Act
            LoggingService.WriteToFile(changedItems);

            //Assert
            //Nothing to assert here.
        }
    }
}