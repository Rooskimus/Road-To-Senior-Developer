using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.BL
{
    public class InvoiceRepository
    {
        public List<Invoice> Retrieve(int customerId)
        {
            List<Invoice> invoiceList = new List<Invoice>
            {new Invoice()
                    { InvoiceId = 1,
                    CustomerId = 1,
                    InvoiceDate=new DateTime(2013, 6, 20),
                    DueDate=new DateTime(2013, 8,29),
                    IsPaid=null},
            new Invoice()
                    { InvoiceId = 2,
                    CustomerId = 1,
                    InvoiceDate=new DateTime(2013, 7, 20),
                    DueDate=new DateTime(2013, 8,20),
                    IsPaid=null},
            new Invoice()
                    { InvoiceId = 3,
                    CustomerId = 2,
                    InvoiceDate=new DateTime(2013, 7, 25),
                    DueDate=new DateTime(2013, 8,25),
                    IsPaid=null},
            new Invoice()
                    { InvoiceId = 4,
                    CustomerId = 3,
                    InvoiceDate=new DateTime(2013, 7, 1),
                    DueDate=new DateTime(2013, 9,1),
                    IsPaid=true}};

            List<Invoice> filteredList = invoiceList.Where(i => i.CustomerId == customerId).ToList();

            return filteredList;
        }
    }
}
