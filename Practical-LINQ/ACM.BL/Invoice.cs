using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.BL
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool? IsPaid { get; set; }

        public decimal Amount { get; set; }
        public int NumberOfUnits { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal TotalAmount
        {
            get
            {
                return this.Amount - (this.Amount * (this.DiscountPercent / 100));
            }
        }
    }
}
