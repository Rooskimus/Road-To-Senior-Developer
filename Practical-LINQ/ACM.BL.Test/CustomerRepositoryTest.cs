using ACM.BL;
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACM.BL.Test
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void FindTestExistingCustomer()
        {
            // Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            // Act
            var result = repository.Find(customerList, 2);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.CustomerId);
            Assert.AreEqual("Baggins", result.LastName);
            Assert.AreEqual("Bilbo", result.FirstName);
        }

        [TestMethod]
        public void GetNamesTest()
        {
            // Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            // Act
            var query = repository.GetNames(customerList);

            //Analyze
            foreach (var item in query)
            {
                TestContext.WriteLine(item);
            }

            // Assert
            Assert.IsNotNull(query);
        }

        [TestMethod]
        public void GetNamesAndEmailTest()
        {
            // Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            // Act
            var query = repository.GetNamesAndEmail(customerList);
            
            //NOT REALLY A TEST
        }

        [TestMethod]
        public void GetNamesAndTypeTest()
        {
            // Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            CustomerTypeRepository typeRepository = new CustomerTypeRepository();
            var customerTypeList = typeRepository.Retrieve();

            // Act
            var query = repository.GetNamesAndType(customerList, customerTypeList);

            //NOT REALLY A TEST - it just lets us see output easily.
        }

        [TestMethod]
        public void GetOverdueCustomersTest()
        {
            // Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            // Act
            var query = repository.GetOverdueCustomers(customerList);

            // Analyze
            foreach (var item in query)
            {
                TestContext.WriteLine($"{item.FirstName} {item.LastName}");
            }

            //Assert
            Assert.IsNotNull(query);
        }

        [TestMethod]
        public void SortByNameTest()
        {
            // Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            // Act
            var result = repository.SortByName(customerList);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.First().CustomerId);
            Assert.AreEqual("Baggins", result.First().LastName);
            Assert.AreEqual("Bilbo", result.First().FirstName);
        }

        [TestMethod]
        public void SortByNameInReverseTest()
        {
            // Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            // Act
            var result = repository.SortByNameInReverse(customerList);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Last().CustomerId);
            Assert.AreEqual("Baggins", result.Last().LastName);
            Assert.AreEqual("Bilbo", result.Last().FirstName);
        }

        [TestMethod]
        public void SortByType()
        {
            // Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            // Act
            var result = repository.SortByType(customerList);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(null, result.Last().CustomerTypeId);
        }

        [TestMethod]
        public void GetInvoiceTotalByCustomerTypeTest()
        {
            // Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            CustomerTypeRepository typeRepository = new CustomerTypeRepository();
            var customerTypeList = typeRepository.Retrieve();

            // Act
            var query = repository.GetInvoiceTotalByCustomerType(customerList, customerTypeList);

            //NOT REALLY A TEST
        }
    }
}
