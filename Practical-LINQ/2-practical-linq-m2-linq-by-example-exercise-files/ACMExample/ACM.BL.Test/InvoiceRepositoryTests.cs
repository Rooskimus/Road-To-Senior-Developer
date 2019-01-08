using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACM.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.BL.Tests
{
    [TestClass()]
    public class InvoiceRepositoryTests
    {
        [TestMethod()]
        public void CalculateTotalAmountInvoicedTest()
        {
            // Arrange
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            // Act
            var actual = repository.CalculateTotalAmountInvoiced(invoiceList);

            // Assert
            Assert.AreEqual(1333.14M, actual);
        }

        [TestMethod()]
        public void CalculateMeanTest()
        {
            // Arrange
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            // Act
            var actual = repository.CalculateMean(invoiceList);

            // Assert
            Assert.AreEqual(6.875M, actual);
        }

        [TestMethod()]
        public void CalculateMedianTest()
        {
            // Arrange
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            // Act
            var actual = repository.CalculateMedian(invoiceList);

            // Assert
            Assert.AreEqual(10M, actual);
        }

        [TestMethod()]
        public void CalculateTotalUnitsSoldTest()
        {
            // Arrange
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            // Act
            var actual = repository.CalculateTotalUnitsSold(invoiceList);

            // Assert
            Assert.AreEqual(136, actual);
        }

        [TestMethod()]
        public void GetInvoiceTotalByIsPaidTest()
        {
            // Arrange
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            // Act
            var actual = repository.GetInvoiceTotalByIsPaid(invoiceList);

            // NOT REALLY A TEST - just used to view results.
        }

        [TestMethod()]
        public void GetInvoiceTotalsByIsPaidAndMonthTest()
        {
            // Arrange
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            // Act
            var actual = repository.GetInvoiceTotalsByIsPaidAndMonth(invoiceList);

            // NOT REALLY A TEST - just used to view results.
        }

        [TestMethod()]
        public void CalculateModeTest()
        {
            // Arrange
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            // Act
            var actual = repository.CalculateMode(invoiceList);

            // Assert
            Assert.AreEqual(10M, actual);
        }
    }
}