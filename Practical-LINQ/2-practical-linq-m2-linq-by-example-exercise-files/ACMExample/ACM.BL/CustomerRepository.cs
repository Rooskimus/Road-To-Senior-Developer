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

            foundCustomer = customerList.FirstOrDefault(c =>
                               c.CustomerId == customerId);

            //foundCustomer = customerList.FirstOrDefault(c =>
            //                {
            //                    Debug.WriteLine(c.LastName);
            //                    return c.CustomerId == customerId;
            //                });

            //foundCustomer = customerList.Where(c =>
            //                c.CustomerId == customerId)
            //                .Skip(1)
            //                .FirstOrDefault();

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

        public dynamic GetNamesAndId(List<Customer> customerList)
        {
            var query = customerList.OrderBy(c => c.LastName)
                                    .ThenBy(c => c.FirstName)
                                    .Select(c => new
                                    {
                                        Name = $"{c.LastName}, {c.FirstName}",
                                        c.CustomerId
                                    });
            return query.ToList();
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
            return query.ToList();
        }

        public IEnumerable<Customer> GetOverdueCustomers(List<Customer> customerList)
        {
            // SelectMany and its overloads were hard for me to understand, so I wanted to write up
            // my take on how it worked for future reference.  First we tried with a simple Select:
            //
            // var query = customerList.Select(c => c.InvoiceList).Where(i => (i.IsPaid ?? false) == false));
            //
            // **Side Note**: The ?? operator is used for nullable value types and returns false only if the
            //                value is null, essentially letting you pick your third state.  In this case the
            //                states for IsPaid are "paid" (true), "not paid" (false), and "overdue" (null).
            //
            // However, this gives a IEnumerable<IEnumerable<Invoice>> result, which is as you can tell just
            // by looking at that type awful to work with, and also doesn't return a list of customers like we
            // want.  Basically, each customer has a list of invoices.  This list of invoices is being put into a list.
            // so we have a list of a list of invoices, hence the nested IEnumerables.  The solution to this is the
            // SelectMany function.  So we took a look at the standard version, just changing Select to SelectMany:
            //
            // var query = customerList.SelectMany(c => c.InvoiceList).Where(i => (i.IsPaid ?? false) == false));
            //
            // What this does, as the Intellisense states, is "flattens" the results.  What that means is that we had a list
            // of lists.  SelectMany projects all of the items from the sublists into a single list and returns that.  So
            // the above query is of a time of IEnumerable<Invoice>.  This is better, but still not the list of customers
            // we desire.  To get what we need we use an overload (specifically the third overload) of SelectMany:

            var query = customerList
                        .SelectMany(c => c.InvoiceList.Where(i => (i.IsPaid ?? false) == false), (c, i) => c).Distinct();

            // This is the same as before but with one extra delegate parameter (i.e. a function) which is called the 
            // return selector. It will take an associated customer and invoice as parameters and in this case simply
            // return the customer.  The list of invoices that was generated already contained only those invoices which
            // were overdue.  By returning the customers associated with those invoices, you now have a list of customers
            // which are overdue on their invoices.  We add the Distinct to the end because each invoice will return its
            // associated customer, creating repeats if the customer has more than one overdue invoice.

            return query;
        }

        public List<Customer> Retrieve()
        {
            InvoiceRepository invoiceRepository = new InvoiceRepository();

            List<Customer> custList = new List<Customer>
                    {new Customer() 
                          { CustomerId = 1, 
                            FirstName="Frodo",
                            LastName = "Baggins",
                            EmailAddress = "fb@hob.me",
                            CustomerTypeId = 1,
                            InvoiceList = invoiceRepository.Retrieve(1)},
                    new Customer() 
                          { CustomerId = 2, 
                            FirstName="Bilbo",
                            LastName = "Baggins",
                            EmailAddress = "bb@hob.me",
                            CustomerTypeId=null,
                            InvoiceList = invoiceRepository.Retrieve(2)},
                    new Customer() 
                          { CustomerId = 3, 
                            FirstName="Samwise",
                            LastName = "Gamgee",
                            EmailAddress = "sg@hob.me",
                            CustomerTypeId=4,
                            InvoiceList = invoiceRepository.Retrieve(3)},
                    new Customer() 
                          { CustomerId = 4, 
                            FirstName="Rosie",
                            LastName = "Cotton",
                            EmailAddress = "rc@hob.me",
                            CustomerTypeId=2,
                            InvoiceList = invoiceRepository.Retrieve(4)}};
            return custList;
        }

        public IEnumerable<Customer> RetrieveEmptyList()
        {
            return Enumerable.Repeat(new Customer(), 5);
        }

        public IEnumerable<Customer> SortByName(IEnumerable<Customer> customerList)
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
