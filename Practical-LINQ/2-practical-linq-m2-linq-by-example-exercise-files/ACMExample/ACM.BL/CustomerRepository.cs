using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ACM.BL
{
    public class CustomerRepository
    {
        public Customer Find(List<Customer> customerList, int customerId)
        {
            Customer foundCustomer = null;

            //foreach (var c in customerList)
            //{
            //    if (c.CustomerId == customerId)
            //    {
            //        foundCustomer = c;
            //        break;
            //    }
            //}

            //var query = from c in customerList
            //            where c.CustomerId == customerId
            //            select c;

            //foundCustomer = query.First();

            //foundCustomer = customerList.FirstOrDefault(c =>
            //                   c.CustomerId == customerId);

            //foundCustomer = customerList.FirstOrDefault(c =>
            //                {
            //                    Debug.WriteLine(c.LastName);
            //                    return c.CustomerId == customerId;
            //                });

            foundCustomer = customerList.Where(c =>
                            c.CustomerId == customerId)
                            .Skip(1)
                            .FirstOrDefault();

            return foundCustomer;

        }

        public IEnumerable<string> GetNames(List<Customer> customerList)
        {
            var query = customerList.Select(c => c.LastName + ", " + c.FirstName);
            return query;
        }

        // The following is not a recommended way to use anonymous types, but is encapsulated
        // this way to make for an easy code snippet to reuse in the future.  Anonymous
        // types can't really be passed back and forth among methods, so it's only recommended
        // to use them at the point where they're needed, such as in the UI layer.
        // In other words, don't try to pass anonymous types out from a function!
        public dynamic GetNamesAndEmail(List<Customer> customerList)
        {
            var query = customerList.Select(c => new
            {
                Name = c.LastName + ", " + c.FirstName,
                c.EmailAddress
            });

            foreach (var item in query)
            {
                Console.WriteLine(item.Name + ":" + item.EmailAddress);
            }
            return query;
        }

        public dynamic GetNamesAndType(List<Customer> customerList,
                                        List<CustomerType> customerTypeList)
        {
            var query = customerList.Join(customerTypeList,
                            c => c.CustomerTypeId,   // outer selector  - these select the index to join on.
                            ct => ct.CustomerTypeId, // inner selector
                            (c, ct) => new           // result selector, takes outer and inner selector parameters as parameters
                            {                        // "new" keyword for making an anonymous type.
                                Name = $"{c.LastName}, {c.FirstName}", // Display LastName, FirstName from customer object
                                CustomerTypeName = ct.TypeName         // with customer Type name from customer type object
                            });

            foreach (var item in query)
            {
                Console.WriteLine(item.Name + " : " + item.CustomerTypeName);
            }
            return query;
        }


        public List<Customer> Retrieve()
        {
            List<Customer> custList = new List<Customer>
                    {new Customer() 
                          { CustomerId = 1, 
                            FirstName="Frodo",
                            LastName = "Baggins",
                            EmailAddress = "fb@hob.me",
                            CustomerTypeId=1},
                    new Customer() 
                          { CustomerId = 2, 
                            FirstName="Bilbo",
                            LastName = "Baggins",
                            EmailAddress = "bb@hob.me",
                            CustomerTypeId=null},
                    new Customer() 
                          { CustomerId = 3, 
                            FirstName="Samwise",
                            LastName = "Gamgee",
                            EmailAddress = "sg@hob.me",
                            CustomerTypeId=1},
                    new Customer() 
                          { CustomerId = 4, 
                            FirstName="Rosie",
                            LastName = "Cotton",
                            EmailAddress = "rc@hob.me",
                            CustomerTypeId=2}};
            return custList;
        }

        public IEnumerable<Customer> RetrieveEmptyList()
        {
            return Enumerable.Repeat(new Customer(), 5);
        }

        public IEnumerable<Customer> SortByName(List<Customer> customerList)
        {
            return customerList.OrderBy(c => c.LastName)
                            .ThenBy(c=> c.FirstName);
        }

        public IEnumerable<Customer> SortByNameInReverse(List<Customer> customerList)
        {
            //return customerList.OrderByDescending(c => c.LastName)
            //                    .ThenByDescending(c => c.FirstName);
            return SortByName(customerList).Reverse();
        }

        public IEnumerable<Customer> SortByType(List<Customer> customerList)
        {
            return customerList.OrderByDescending(c => c.CustomerTypeId.HasValue)
                                .ThenBy(c=>c.CustomerTypeId);
        }
    }
}
